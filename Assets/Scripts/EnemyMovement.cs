using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb2D;

    [SerializeField] float enemySpeed=1f;
    void Start()
    {
        rb2D=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb2D.velocity=new Vector2(enemySpeed,0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {//move speedi - olsun
        // triggerdan çıktığında scalei -1 olsun
        enemySpeed=-enemySpeed;
        FlipEnemyFacing();



    }

    void FlipEnemyFacing()
    {
        transform.localScale=new Vector2(-transform.localScale.x,transform.localScale.y);
    }
}
