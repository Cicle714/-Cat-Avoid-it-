using UnityEngine;
using static Bullet;

public class Enemy : MonoBehaviour
{

    public bool Attack;
    public bool heart;
    enum AttackType
    {
        Normal,
        gravity,
    }
    [SerializeField]
    AttackType type;
    [SerializeField]
    GameObject BulletObj;
    [SerializeField]
    GameObject HeartBullet;
    [SerializeField]
    GameObject BulletPos;

    private SpriteRenderer sr;
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
        if (Attack)
        {

            Attack = false;
            GameObject bullet = Instantiate(BulletObj, BulletPos.transform.position, Quaternion.identity);
            if (heart)
            {
                heart  = false;
                bullet.GetComponent<Bullet>().heart = true;
            }
            if (sr.flipX)
                bullet.GetComponent<Bullet>().reverse = true;

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
