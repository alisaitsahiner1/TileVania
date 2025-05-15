using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidBody2D;
    PlayerMovement player;

    [SerializeField] float arrowSpeed=15f;
    float xSpeed;
    void Start()
    {
        myRigidBody2D=GetComponent<Rigidbody2D>();
        player=FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        xSpeed=player.transform.localScale.x * arrowSpeed;  //oku playerın yönünde atması için bu değişkene atayıp vectöre verdik
        myRigidBody2D.velocity=new Vector2 (xSpeed , 0f);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Enemy")
        {
            Destroy(other.gameObject);   //enemy ile trigger olursa enemyi yok et
        }
        Destroy(gameObject);   //arrowu da yok et
    }

    void OnCollisionEnter2D (Collision2D other) //herhangi bir şeye çarparsa arrow yok olsun
    {
        Destroy(gameObject);
    }
}
