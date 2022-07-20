using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_H3 : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private BoxCollider2D playerCollider;

    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float jumpAmount = 5f;

    [SerializeField] private float groundCheckPerimeter=0.2f;

    //Player Input Values
    private float h_Input;
    private bool jump_input=false;
    

    private bool isWalking=false;
    private bool isOnGround=false;
    private bool isJumping=false;



    void Awake()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
        playerCollider = this.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }


    void FixedUpdate()
    {
        ApplyMovement();
        CheckGround();
    }

    void ApplyMovement()
    {
        playerRB.velocity = new Vector2( h_Input * walkingSpeed , playerRB.velocity.y);
        if(jump_input==true)
        {
            jump_input=false;
            Jump();

        }
    }

    void CheckInput()
    {
        h_Input = Input.GetAxis("Horizontal");

        //jump

        if(Input.GetButton("Jump"))
        {
            jump_input=true;
        }
    }
    void Jump()
    {
        if(isOnGround==true&&isJumping==false)
        {
              playerRB.velocity=new Vector2(playerRB.velocity.x,jumpAmount);
        }
      
    }
    void CheckGround()
    {
        RaycastHit2D groundHit=Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y+groundCheckPerimeter);
        //Debug.Log(groundHit);
        if(groundHit)
           { Debug.DrawRay(playerCollider.bounds.center,Vector2.down*(playerCollider.bounds.extents.y+groundCheckPerimeter),Color.green);
           isOnGround=true;
           }
        else
            {Debug.DrawRay(playerCollider.bounds.center,Vector2.down*(playerCollider.bounds.extents.y+groundCheckPerimeter),Color.red);
            isOnGround=false;
            }
        
    }

}