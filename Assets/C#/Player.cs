using UnityEditorInternal;
using UnityEngine;

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
    private float knockbackCount; //ノックバック
    [SerializeField]
    private float knockbackTime;
    [SerializeField]
    private float InvisibleTime;
    private float InvisibleCount;
    [SerializeField]
    private float InvisibleInterbal;
    private float InvisibleInterbalCount;

    private SpriteRenderer sr; //プレイヤーの画像
    private Animator anim; 
    private Rigidbody2D rb;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        walk = false;
        Dash = false;

        if (Hit)
        {
            if (knockbackCount > 0)
            {   
                knockbackCount -= Time.deltaTime;
                return;
            }
            if (knockback)
            {
                knockback = false;
                anim.Play("Idle");
            }
            if(InvisibleInterbalCount < 0)
            {
                InvisibleInterbalCount = InvisibleInterbal;
                if(sr.color.a != 0)
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
                else
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }else
                InvisibleInterbalCount -= Time.deltaTime;

                InvisibleCount -= Time.deltaTime;

            if (InvisibleCount < 0)
            {
                Hit = false;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
        }

        float MoveSpeed = WalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Dash = true;
            MoveSpeed += DashSpeed;
        }


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
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump = true;
            transform.position += Vector3.up * 0.1f;
            rb.linearVelocityY = JumpPow;
            isGround = false;
            anim.Play("Jump");
        }

        if(Jump && rb.linearVelocityY < 0)
        {
            fall = true;
        }
        
        anim.SetBool("Walk", walk);
        anim.SetBool("Dash",Dash);
        anim.SetBool("Down", fall);
        if (!walk)
        {
            rb.linearVelocityX = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (!Hit && !bullet.heart)
            {
                anim.Play("Hit");
                Damage();
                rb.linearVelocity = new Vector2(-8 * bullet.Vec, 8);
            }
            else
            {
                Hp++;
                if(Hp > MaxHp)
                    Hp = MaxHp;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() && !Hit)
        {
            int Vec;
            Damage();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.Sr.flipX)
                Vec = -1;
            else
                Vec = 1;
                rb.linearVelocity = new Vector2(-8 * Vec, 8);
        }

    }

    void Damage()
    {
        anim.Play("Hit");
        Hit = true;
        knockback = true;
        knockbackCount = knockbackTime;
        InvisibleCount = InvisibleTime;
    }
    public void checkGround()
    {
        Jump = false;
        fall = false;
        isGround = true;
    }

}
