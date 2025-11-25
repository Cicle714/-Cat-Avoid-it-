using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool Attack; //攻撃をするか
    public bool heart; //回復アイテムをだすか
    enum AttackType //攻撃の種類
    {
        Normal,
        gravity,
    }
    [SerializeField]
    AttackType type; //種類の設定用
    [SerializeField]
    GameObject BulletObj; //弾の設定
    [SerializeField]
    GameObject HeartBullet; //回復アイテムの設定
    [SerializeField]
    GameObject BulletPos; //弾が出る場所

    private SpriteRenderer sr; //画像
    public SpriteRenderer Sr
    {
        get { return sr; }
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃がオンになったら
        if (Attack)
        {

            Attack = false;
            GameObject bullet = Instantiate(BulletObj, BulletPos.transform.position, Quaternion.identity);
            //heartがtrurなら弾が回復アイテムになる
            if (heart)
            {
                heart = false;
                bullet.GetComponent<Bullet>().heart = true;
            }
            if (sr.flipX)
                bullet.GetComponent<Bullet>().reverse = true;

            //攻撃タイプで、弾の出方を変える
            switch (type)
            {
                case AttackType.Normal:
                    bullet.GetComponent<Bullet>().type = Bullet.BulletType.Normal;
                    break;
                case AttackType.gravity:
                    bullet.GetComponent<Bullet>().type = Bullet.BulletType.gravity;
                    break;

            }


        }
    }
}
