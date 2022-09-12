using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = .2f;
    [SerializeField] 
    private float _jumpForce;
    
    private Rigidbody2D _rb;

    private bool _isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    public LayerMask Ground;
    private int sideFacing = 1;
    private float _characterRadius;

    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;
    
    private float _jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;

    private bool _top;
    private bool _bottom;
    private bool _right;
    private bool _left;
    

    void Start () {
        var collider = gameObject.GetComponent<Collider2D>() as CircleCollider2D;
        _rb = GetComponent<Rigidbody2D>();
        _characterRadius = collider.radius;
    }
	
    void Update () {
        _rb.velocity = new Vector2(_moveSpeed * Time.fixedDeltaTime, _rb.velocity.y);
        if (_isGrounded)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
        
        if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
        {
            _rb.velocity = Vector2.up * _jumpForce;
            _jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && _rb.velocity.y != 0f)
        {
            _coyoteTimeCounter = 0f;
        }

        Vector2 inFront = transform.right * sideFacing;
        RaycastHit2D hit = Physics2D.Raycast (transform.position, inFront, _characterRadius + 0.01f, Ground);
        Debug.DrawRay(transform.position, inFront);

        if (hit.collider != null)
        {
            Flip();
            _moveSpeed *= -1;
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, Ground);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GravityUp"))
        {
            _rb.gravityScale *= -1;
            _jumpForce *= -1;
            Flip();
            Rotation();
        }
        
        if (other.gameObject.CompareTag("GravityDown"))
        {
            _rb.gravityScale *= -1;
            _jumpForce *= -1;
            Flip();
            Rotation();
        }
    }

    private void Flip(){
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        sideFacing *= -1;
    }

    private void Rotation()
    {
        if (!_top)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        _top = !_top;

    }
    
    

    

}
