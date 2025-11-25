using UnityEngine;

public class CheckGround : MonoBehaviour
{

    Rigidbody2D rb;
    Player player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //‘«‚Ì•”•ª‚ª’n–Ê‚ÉG‚ê‚½‚ç
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            player.checkGround();
        }
    }
        
    



}
