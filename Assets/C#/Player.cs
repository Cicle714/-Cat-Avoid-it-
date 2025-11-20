using UnityEngine;

public class Player : MonoBehaviour
{

    private bool walk;
    private bool Dash;
    private bool Jump;
    private bool isGround;
    [SerializeField]
    private float MoveSpeed;

    private SpriteRenderer sr;
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
        if (Input.GetKey(KeyCode.D))
        {
            sr.flipX = false;
            walk = true;
            rb.linearVelocityX = MoveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = true;
            walk = true;
            rb.linearVelocityX = -MoveSpeed;
        }
        anim.SetBool("Walk", walk);
        if (!walk)
        {
            rb.linearVelocityX = 0;
        }
    }
}
