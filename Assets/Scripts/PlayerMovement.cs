using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  //input sistemini kullanabilmek için eklememiz gerekli

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput; //girdileri taşımak için depolamamız gerekli
    Rigidbody2D rb2D;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    SpriteRenderer mySpriteRenderer;
    
    

    [SerializeField] float runSpeed=2f;
    [SerializeField] float jumpSpeed=5f;
    [SerializeField] float climbSpeed=2f;
    [SerializeField] float gravityScaleAtStart=2f;
    [SerializeField] Vector2 deathKick=new Vector2(1f,5f);
    [SerializeField] GameObject arrow;
    [SerializeField] Transform arc;


    bool isAlive=true;
    void Start()
    {
        rb2D=GetComponent<Rigidbody2D>();
        myAnimator=GetComponent<Animator>();
        myBodyCollider2D=GetComponent<CapsuleCollider2D>();
        myFeetCollider2D=GetComponent<BoxCollider2D>();
        rb2D.gravityScale=gravityScaleAtStart;   //başlangıçta yer çekimi hep bu olsun bazı yerlerde değişse bile o koşuldan sonra buna döner
        mySpriteRenderer=GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if(!isAlive){return;}  //is alive değilse bir şey yapma
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        
    }
    void OnMove(InputValue value)
    {
        if(!isAlive){return;}
        moveInput=value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue value)
    {
        if(!isAlive){return;}
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;} //kapsül colliderırımız Getmask metoduyla ulaştığımız ground layerımıza değmiyorsa bu durumda bir şey yapma
        
            if(value.isPressed)
            {
            
                rb2D.velocity += new Vector2(0f,jumpSpeed);  //yeni vector2 değerleri kadar velocitymize ekle
            }
    }

    void OnFire(InputValue value)
    {
        if(!isAlive){return;}

        Instantiate(arrow,arc.position,transform.rotation);

    }
    
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x *runSpeed, rb2D.velocity.y);
        rb2D.velocity = playerVelocity;
        if(Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon)
        {
           myAnimator.SetBool("isRunning",true); //isRunningi true olarak ayarla
        }
        else
        {
            myAnimator.SetBool("isRunning",false);
        }
    } 

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed=Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon; //yatayda sıfır ve sıfıra çok yakon değerlerden daha büyük hızı var mı

        if (playerHasHorizontalSpeed)
        {
            transform.localScale=new Vector2(Mathf.Sign(rb2D.velocity.x),1f); //scale e sadece -1 ve 1 değerleri atansın(Sign negatif sayılara -1 , poz,t,f sayılara 1 değeri verir)
        }
    }

    void ClimbLadder()
    {
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) //climbing layerına değmiyorsa
            {
                rb2D.gravityScale=gravityScaleAtStart;

                myAnimator.SetBool("isClimbing",false);
                return;
            }
    
        Vector2 climbVelocity = new Vector2(rb2D.velocity.x , moveInput.y * climbSpeed); // x ekseninde sabit kal, y ekseninde hareket et
        rb2D.velocity=climbVelocity;

        rb2D.gravityScale=0f;  //merdivenden aşağıya kaymasın

        bool playerHasVerticalSpeed=Mathf.Abs(rb2D.velocity.y)>Mathf.Epsilon;
        myAnimator.SetBool("isClimbing",playerHasVerticalSpeed);

    }
    void Die()
        {
            if(myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards","Water")))
            {
                isAlive=false;
                myAnimator.SetTrigger("Dying");  // animatörde dying triggerını etkinleştir
                // y ekseninde biraz zıplat
                rb2D.velocity=deathKick;
                FindObjectOfType<GameSession>().ProcessPlayerDeath(); // GameSession objesinin ProcessPlayerDeath metodunu uygula
                

            }
        }    



}
