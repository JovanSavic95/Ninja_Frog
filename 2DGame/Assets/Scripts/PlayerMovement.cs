using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private enum PlayerState {idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpingSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(rb.bodyType != RigidbodyType2D.Static)
        {
            directionX = Input.GetAxisRaw("Horizontal");
            
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && PlayerGrounded())
            {
                jumpingSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            UpdateAnimationState();
        }
       
    }


    private void UpdateAnimationState()
    {
        PlayerState state;

        if (directionX > 0f)
        {
            state = PlayerState.running;
            sprite.flipX = false;
        }
        else if (directionX < 0f)
        {
            state = PlayerState.running;
            sprite.flipX = true;
        }
        else
        {
            state = PlayerState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = PlayerState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = PlayerState.falling;
        }
            anim.SetInteger("state",(int)state);
    }

    private bool PlayerGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
