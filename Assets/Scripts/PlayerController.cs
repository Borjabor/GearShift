using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = .2f;
    //private CircleCollider2D collider;
    private float crawlerRadius;
    //private Vector2 crawlerCenter;
    public LayerMask obstacles;
    private Vector3 previousPosition;
    private Rigidbody2D _rb;

    void Start () {
        var collider = gameObject.GetComponent<Collider2D>() as CircleCollider2D;
        _rb = GetComponent<Rigidbody2D>();
        crawlerRadius = collider.radius;
    }
	
    void Update () {
        Vector2 up = transform.up;

        Vector2 frontPoint = transform.position;
        //frontPoint.x += (crawlerRadius * transform.lossyScale.x);

        RaycastHit2D hit = Physics2D.Raycast (frontPoint, -up, crawlerRadius+0.1f, obstacles);
        Debug.DrawRay (frontPoint, -up, Color.red);

        if (hit.collider != null) {
            //just debug info
            Debug.DrawRay (hit.point, hit.normal, Color.yellow);
            Vector3 moveDirection = Quaternion.Euler (0, 0, -90) * hit.normal;
            Debug.DrawRay (frontPoint, moveDirection, Color.white);
			
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            //stick to the obstacle
            _rb.AddForce (-2 * hit.normal);
            //move forward
            //transform.Translate(transform.right * _moveSpeed * Time.deltaTime, Space.Self);
            //_rb.AddForce(0.5f * moveDirection);
            _rb.velocity = moveDirection * _moveSpeed;


        } else {
            //no ground under object - falling.
            _rb.AddForce(Vector3.down);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Flip();
        }
    }

    private void Flip(){
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        _moveSpeed *= -1;
    }
    
    

    

}
