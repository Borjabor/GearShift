using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = .2f;
    public LayerMask Ground;
    private Rigidbody2D _rb;

    void Start () {
        _rb = GetComponent<Rigidbody2D>();
    }
	
    void Update () {
        _rb.velocity = new Vector2(_moveSpeed * Time.fixedDeltaTime, _rb.velocity.y);
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
