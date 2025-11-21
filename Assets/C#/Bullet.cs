using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprities;

    SpriteRenderer sr;
    Rigidbody2D rb;
    public float MoveSpeed;
    public bool reverse;

    public bool heart;

    public int Vec;
    public enum BulletType
    {
        Normal,
        gravity,
    }
    public BulletType type;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if(heart)
            sr.sprite = sprities[1];
        else
            sr.sprite = sprities[0];
        
        if (reverse)
            Vec = -1;
        else
            Vec = 1;

        rb = GetComponent<Rigidbody2D>();
        if (type == BulletType.gravity)
            rb.linearVelocity = new Vector3(-10 * Vec, 10);
        else
            rb.gravityScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (type == BulletType.Normal)
        {
            transform.position += Vector3.left * MoveSpeed * Vec * Time.deltaTime;
        }
    }
}
