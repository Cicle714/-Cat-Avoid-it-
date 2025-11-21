using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int Hp;
    [SerializeField]
    private int MaxHp;
    public int GetHp
    {
        get { return Hp; }
    }

    private bool walk; //歩いているか
    private bool Dash; //走っているか
    private bool Jump; //ジャンプしたか
    private bool isGround; //地面にいるか
    [SerializeField]
    private float WalkSpeed; //移動の速さ
    [SerializeField]
    private float DashSpeed;
    [SerializeField]
    private float JumpPow; //ジャンプ力
    private bool fall;

    private bool Hit; //攻撃に当たったか
    private bool knockback;
    private float knockbackCount; //ノックバックのカウント
    [SerializeField]
    private float knockbackTime; //ノックバックの時間
    [SerializeField]
    private float InvisibleTime; //点滅時間
    private float InvisibleCount; //点滅時間のカウント
    [SerializeField]
    private float InvisibleInterbal; //点滅の間隔
    private float InvisibleInterbalCount; //点滅のカウント

    [SerializeField]
    private GameObject DeathObj; //死んだときのリボン
    [SerializeField]
    private GameObject DeathEffe; //死んだときのキラキラ

    private UI ui; //UI取得
    private Camera camera; //カメラ取得
    private SpriteRenderer sr; //プレイヤーの画像
    private Animator anim; 
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private float offsetX;
    void Start()
    {
        ui = FindObjectOfType<UI>();
        camera = FindObjectOfType<Camera>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        Hp = MaxHp; //Hpを最大値に
        offsetX = col.offset.x;　//当たり判定取得
    }

    // Update is called once per frame
    void Update()
    {
        
        walk = false; //移動操作の有無
        Dash = false; //ダッシュの有無

        //攻撃に当たった後の処理
        if (Hit)
        {
            if (knockbackCount > 0)
            {   
                knockbackCount -= Time.deltaTime; //ノックバックのカウント
                return;
            }
            if (knockback)
            {
                knockback = false; //ノックバック終わり
                anim.Play("Idle"); //アニメーションをアイドル
            }
            if(InvisibleInterbalCount < 0)
            {
                InvisibleInterbalCount = InvisibleInterbal; //点滅のカウントのリセット
                //点滅処理
                if(sr.color.a != 0)
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
                else
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }else
                InvisibleInterbalCount -= Time.deltaTime; //点滅のカウント

                InvisibleCount -= Time.deltaTime; //点滅時間のカウント

            if (InvisibleCount < 0)
            {
                //点滅終わり
                Hit = false;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
        }

        float MoveSpeed = WalkSpeed;　//動くスピードを代入

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Dash = true;
            MoveSpeed += DashSpeed; //ダッシュで速度加算
        }

        //移動処理
        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A)))
        {
            sr.flipX = false;
            walk = true;
            rb.linearVelocityX = MoveSpeed;
        }
        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D)))
        {
            sr.flipX = true;
            walk = true;
            rb.linearVelocityX = -MoveSpeed;
        }

        //ジャンプの処理
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump = true;
            transform.position += Vector3.up * 0.1f;
            rb.linearVelocityY = JumpPow;
            isGround = false;
            anim.Play("Jump");
        }
        
        //ジャンプ後、落ちている時の処理
        if(Jump && rb.linearVelocityY < 0)
        {
            fall = true;
        }
        //アニメーションの処理
        anim.SetBool("Walk", walk);
        anim.SetBool("Dash",Dash);
        anim.SetBool("Down", fall);

        //向きによる当たり判定の反転
        if (sr.flipX)
            col.offset = new Vector3(offsetX * -1, col.offset.y);
        else
            col.offset = new Vector3(offsetX * -1, col.offset.y);

        //移動してなかったら滑らず止まる
        if (!walk)
        {
            rb.linearVelocityX = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敵の弾に当たった時の処理
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (!Hit && !bullet.heart)
            {
                //敵の攻撃に当たったら
                Destroy(collision.gameObject);
                anim.Play("Hit");
                Damage();
                rb.linearVelocity = new Vector2(-8 * bullet.Vec, 8);
            }
            else if(bullet.heart)
            {
                //回復アイテムに当たったら
                Destroy(collision.gameObject);
                Hp++;
                if(Hp > MaxHp)
                    Hp = MaxHp;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //敵本体に当たったら
        if (collision.gameObject.GetComponent<Enemy>() && !Hit)
        {
            int Vec;
            Damage();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //敵の向きで吹っ飛ぶ場所を変える
            if (enemy.Sr.flipX)
                Vec = -1;
            else
                Vec = 1;
                rb.linearVelocity = new Vector2(-8 * Vec, 8);
        }

    }

    /// <summary>
    /// ダメージを受けたときの処理
    /// </summary>
    void Damage()
    {
        
        Hp--;
        if(Hp <= 0)
            StartCoroutine(Death());
        anim.Play("Hit");
        Hit = true;
        knockback = true;
        knockbackCount = knockbackTime;
        InvisibleCount = InvisibleTime;
    }

    /// <summary>
    /// 着地処理
    /// </summary>
    public void checkGround()
    {
        Jump = false;
        fall = false;
        isGround = true;
    }


    /// <summary>
    /// 死んだときの処理
    /// </summary>
    /// <returns></returns>
    IEnumerator Death()
    {
        Time.timeScale = 0;
        Vector3 StartPos = camera.transform.position;　//移動前の最初の位置
        Vector3 Target = new Vector3(transform.position.x,transform.position.y,camera.transform.position.z);//移動場所のターゲット
        float currentTime = 0; //移動するときのカウント
        float step = 1; //step秒後に移動完了
        float cameraZoom = 3.5f; //カメラのZoom距離
        float inertia = 0; //移動の慣性
        float cameraSize = camera.orthographicSize; //カメラの最初のサイズ
        //移動と拡大処理
        while (currentTime < step)
        {
            inertia += Time.unscaledDeltaTime;
           
            camera.orthographicSize = cameraSize - ((inertia * inertia) * cameraZoom);
            currentTime = inertia * inertia;
            
            camera.transform.position = Vector3.Lerp(StartPos, Target, currentTime /step);
           yield return null;
        }

        //移動後に一時停止処理
        float DelayCount = 0;
        float DelayTime = 1f;　//DelayTime秒待つ
        while(DelayCount < DelayTime)
        {
            DelayCount += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
        Instantiate(DeathObj, transform.position, Quaternion.identity); //死亡オブジェクトを出す
        Instantiate(DeathEffe,transform.position, Quaternion.identity); //死亡エフェクトを出す
        gameObject.SetActive(false); //プレイヤーを非表示
        StartCoroutine(ui.BlackOut2()); //暗転処理
;        yield return null;
    }


}
