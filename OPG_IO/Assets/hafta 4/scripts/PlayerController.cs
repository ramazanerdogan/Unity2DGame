using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private BoxCollider2D playerCollider;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;

    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float jumpAmount = 5f;
    [SerializeField] private float groundCheckPerimeter = 0.2f;

    //Ground Check
    public GameObject GroundChecker;
    private PlayerGroundCheckScript groundCheckScript;

    //Player Input Values
    private float h_Input;
    private bool jump_input=false;
    
    //Player State
    [SerializeField] private bool isIdle = true;
    [SerializeField] private bool isWalking=false;
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isJumping=false;

    //Slope Properties
    private float slopDownAngle;
    private float slopePreviousAngle=0f;
    private Vector2 slopNormalPerp;

    //Direction Control
    private bool playerDir=true; //true :1 right direction false: -1 left direction



    void Awake()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
        playerCollider = this.GetComponent<BoxCollider2D>();
        spriteRenderer=this.GetComponent<SpriteRenderer>();
        playerAnimator = this.GetComponent<Animator>();

        groundCheckScript = GroundChecker.GetComponent<PlayerGroundCheckScript>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovoment();
        UpdateAnimations();
    }


    void FixedUpdate()
    {
        ApplyMovement();
        CheckGround();
        CheckSlope();
    }

    private void UpdateAnimations()
    {
        playerAnimator.SetBool( "isWalking", isWalking);
        playerAnimator.SetBool( "isIdle", isIdle);
        playerAnimator.SetBool( "isJumping", isJumping);
        playerAnimator.SetBool( "isOnGround", isOnGround);
    }

    void ApplyMovement()
    {
        //this.transform.position+=new Vector3( h_Input*walkingSpeed*Time.deltaTime ,0f,0f);
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

        //Jump

        if(Input.GetButton("Jump"))
        {
            jump_input=true;
        }
    }


    private void CheckMovoment()
    {
        if(playerDir&& h_Input<-0.15f)
        {
            Flip();
        }
        else if(!playerDir&& h_Input>0.15f)
        {
            Flip();
        }
       
        if( Mathf.Abs(playerRB.velocity.x) > 0.01f)
        {
            isIdle =false;
            if(isJumping == false && isOnGround == true )
            {
               isWalking = true;
            }
        }
        else
        {   
            if(isOnGround == true)
            {
                isIdle = true;
            }
             isWalking = false;
        }
    }


    private void Flip()
    {
        playerDir=!playerDir;

        spriteRenderer.flipX=!spriteRenderer.flipX;

       /* Quaternion target=Quaternion.Euler( this.transform.rotation.x, this.transform.rotation.y, slopDownAngle);
        this.transform.rotation=target;*/

        //this.transform.Rotate(0f, 180, 0f);

    }

    void Jump()
    {
        if(isOnGround==true&&isJumping==false)
        {
              playerRB.velocity=new Vector2(playerRB.velocity.x,jumpAmount);
              isJumping=true;
              isWalking = false;
              isIdle = false;
        }
        
    }
    
    public void  CheckSlope()
    {

        RaycastHit2D groundHit=Physics2D.Raycast(playerCollider.bounds.center - new Vector3(0f,playerCollider.bounds.extents.y+0.05f,0f), Vector2.down,0.5f);
        Debug.DrawRay(playerCollider.bounds.center - new Vector3(0f,playerCollider.bounds.extents.y+0.05f,0f),Vector2.down*0.5f, Color.red);

        if(groundHit.collider!=null)
        {
            slopDownAngle=Vector2.Angle(groundHit.normal, Vector2.up);
            slopNormalPerp=Vector2.Perpendicular(groundHit.normal).normalized;

            Debug.DrawRay( groundHit.point, slopNormalPerp ,Color.blue);
            Debug.DrawRay( groundHit.point , groundHit.normal,Color.green);

            Quaternion target=Quaternion.Euler( this.transform.rotation.x, this.transform.rotation.y, slopDownAngle);
            this.transform.rotation=target;
        }
        else{
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        }
        
            
        
       

    }
    void CheckGround()
    {
        isOnGround = groundCheckScript.getIsOnGroundFlag();
        if(isOnGround == true) isJumping=false;
        

        /*
        RaycastHit2D groundHit=Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y+groundCheckPerimeter);
        //Debug.Log(groundHit);
        if(groundHit.collider != null)
           { Debug.DrawRay(playerCollider.bounds.center,Vector2.down*(playerCollider.bounds.extents.y+groundCheckPerimeter),Color.green);
           isOnGround=true;
           }
        else
            {Debug.DrawRay(playerCollider.bounds.center,Vector2.down*(playerCollider.bounds.extents.y+groundCheckPerimeter),Color.red);
            isOnGround=false;
            }
        */
    }

} 