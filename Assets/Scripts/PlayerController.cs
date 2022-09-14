using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    private float _moveSpeed = 300f;
    [SerializeField] 
    private float _jumpForce;
    [SerializeField] 
    private float _gameGravity = -30;
    private Vector2 _jumpDirection = Vector2.up;
    private bool _isOnSides = false;
    private bool _isStuck = false;
    
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
    private Gravity _gravity = Gravity.Down;


    void Start () {
        var collider = gameObject.GetComponent<Collider2D>() as CircleCollider2D;
        _rb = GetComponent<Rigidbody2D>();
        _characterRadius = collider.radius;
        _moveSpeed = 300f;
        _isStuck = false;
    }
	
    void Update () {
        if (_isOnSides)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _rb.velocity = new Vector2(_moveSpeed * Time.fixedDeltaTime, _rb.velocity.y);
        }
        
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
            _rb.velocity = _jumpDirection * _jumpForce;
            _jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && _rb.velocity.y != 0f)
        {
            _coyoteTimeCounter = 0f;
        }

        //Key debugger
        /*if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            LockKey.CollectedKeys++;
        }*/

        Vector2 inFront = transform.right * sideFacing;
        RaycastHit2D hit = Physics2D.Raycast (transform.position, inFront, _characterRadius + 0.01f, Ground);
        //Debug.DrawRay(transform.position, inFront);

        if (hit.collider != null)
        {
            Flip();
            _moveSpeed *= -1;
        }

        
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, Ground);
        if (_isStuck)
        {
            _moveSpeed *= 0.9f;
            if (_moveSpeed < 0.2f)
            {
                StartCoroutine(Respawn());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GravityUp") && _gravity != Gravity.Up)
        {
            if (_gravity == Gravity.Down || _gravity == Gravity.Right)
            {
                Flip();
            }
            ChangeGravity(Gravity.Up);
            _isOnSides = false;
            
        }
        
        if (other.gameObject.CompareTag("GravityDown") && _gravity != Gravity.Down)
        {
            if (_gravity == Gravity.Up || _gravity == Gravity.Left)
            {
                Flip();
            }
            ChangeGravity(Gravity.Down);
            _isOnSides = false;
            
        }
        
        if (other.gameObject.CompareTag("GravityRight") && _gravity != Gravity.Right)
        {
            if (_gravity == Gravity.Up || _gravity == Gravity.Left)
            {
                Flip();
            }
            ChangeGravity(Gravity.Right);
            _isOnSides = true;
            
        }
        
        if (other.gameObject.CompareTag("GravityLeft") && _gravity != Gravity.Left)
        {
            if (_gravity == Gravity.Down || _gravity == Gravity.Right)
            {
                Flip();
            }
            ChangeGravity(Gravity.Left);
            _isOnSides = true;
            
        }

        if (other.gameObject.CompareTag(("Key")))
        {
            Destroy(other.gameObject);
            LockKey.CollectedKeys++;
        }
        
        if (other.gameObject.CompareTag("Death"))
        {
            _isStuck = true;
        }
    }

    private void Flip(){
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        sideFacing *= -1;
    }

    enum Gravity
    {
        Up,
        Down,
        Left,
        Right
    }

    private void ChangeGravity(Gravity dir)
    {
        switch (dir)
        {
            case Gravity.Up:
                _gravity = Gravity.Up;
                Physics2D.gravity = new Vector2(0f, -_gameGravity);
                transform.eulerAngles = new Vector3(0, 0, 180f);
                _jumpDirection = Vector2.down;
                break;
            case Gravity.Down:
                _gravity = Gravity.Down;
                Physics2D.gravity = new Vector2(0f, _gameGravity);
                transform.eulerAngles = Vector3.zero;
                _jumpDirection = Vector2.up;
                break;
            case Gravity.Left:
                _gravity = Gravity.Left;
                Physics2D.gravity = new Vector2(_gameGravity, 0f);
                transform.eulerAngles = new Vector3(0, 0, -90f);
                _jumpDirection = Vector2.right;
                break;
            case Gravity.Right:
                _gravity = Gravity.Right;
                Physics2D.gravity = new Vector2(-_gameGravity, 0f);
                transform.eulerAngles = new Vector3(0, 0, 90f);
                _jumpDirection = Vector2.left;
                break;
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
