using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private Rigidbody2D rigidbd;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private bool isRightDirection;
    [SerializeField] private float speed;
    

    [SerializeField] private Health health;

    public GroundDetection groundDetection;
    [SerializeField] private CollisionDamage collisionDamage;

    private void Update()
    {
        
        if (groundDetection.isGround)
        {
            
            if (transform.position.x > rightBorder.transform.position.x||collisionDamage.Direction<0)
            {
            
                 isRightDirection = false;

            }
            else if (transform.position.x < leftBorder.transform.position.x||collisionDamage.Direction>0)
            {

                isRightDirection = true;

            }
            rigidbd.velocity = isRightDirection ? Vector2.right : Vector2.left;//���������� if ���� ��������� ����� true �� ��� ����� ������ ����� ���� false �� ��� ����� :
            rigidbd.velocity *= speed;
        }
        if (rigidbd.velocity.x > 0)
            spriteRenderer.flipX = true ;
        if (rigidbd.velocity.x < 0)
            spriteRenderer.flipX = false;
        /*if (isRightDirection&&groundDetection.isGround)
        {
            //rigidbody.velocity = new Vector2(speed, 0); ���� ����� ��� � ����
            rigidbody.velocity = Vector2.right*speed;
           
            if (transform.position.x>rightBorder.transform.position.x)
            {
                //isRightDirection = false;���� ����� ��� � ����
                isRightDirection = !isRightDirection;
                
            } 
            spriteRenderer.flipX = true;
        }
        else if(groundDetection.isGround)
        {
            //rigidbody.velocity = new Vector2(speed, 0); ���� ����� ��� � ����
            rigidbody.velocity = Vector2.left * speed;
            
            if (transform.position.x < leftBorder.transform.position.x)
            {
                //isRightDirection = true; ���� ����� ��� � ����
                isRightDirection = !isRightDirection;
                

            }
            spriteRenderer.flipX = false;
        }*/
       
       
    }
  

}
