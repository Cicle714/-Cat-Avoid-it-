using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprities; //弾の種類

    SpriteRenderer sr;
    Rigidbody2D rb;
    public float MoveSpeed; //弾のスピード
    public bool reverse; //弾が出る向き

    public bool heart; //回復アイテム

    public int Vec; //弾のベクトル

    //弾の動きの種類
    public enum BulletType
    {
        Normal,
        gravity,
    }
    public BulletType type;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        //回復アイテムか、そうでないかの処理
        if (heart)
            sr.sprite = sprities[1];
        else
            sr.sprite = sprities[0];

        //向きの処理
        if (reverse)
            Vec = -1;
        else
            Vec = 1;

        rb = GetComponent<Rigidbody2D>();

        //山なりの攻撃
        if (type == BulletType.gravity)
            rb.linearVelocity = new Vector3(-10 * Vec, 10);
        else
            rb.gravityScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //まっすぐの攻撃
        if (type == BulletType.Normal)
        {
            transform.position += Vector3.left * MoveSpeed * Vec * Time.deltaTime;
        }

    }
}
