using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce =  350;
    [SerializeField] private Kunai kunai;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource claimItem;
    [SerializeField] private GameObject Heal_VFX;
    private bool isGrounded;
    private bool isJumping;
    private bool isAttack;
    private bool isThrow;
    private bool isMoving;
    private bool isDead = false;
    private float horizontal;
    private int coin = 0;
    private Vector3 savePoint;
    private bool isSleep;
    // Start is called before the first frame update
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (isSleep)
        {
            return;
        }
        if (isAttack)
        {
            return;
        }
        if (isThrow)
        {
            return;
        }
        if (isDead == true) return;
        isGrounded = CheckGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");
        if (isGrounded)
        {   
            if(isJumping)
            {
                return;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                isJumping = true;
                ChangeAnim("jump");
                rb.AddForce(jumpForce * Vector2.up);
                jumpingSound.Play();
            }
            else
            {
                if (Input.GetKey(KeyCode.X) && !isJumping)
                {   
                    Attack();
               
                }
                else if (Input.GetKey(KeyCode.Z) && !isJumping)
                {
                    Throw();
                }
    
                if (Mathf.Abs(horizontal) > 0.1f && isJumping == false && !isAttack && !isThrow)
                    {  
                        ChangeAnim("run");
                        rb.velocity = new Vector2(horizontal  * speed, rb.velocity.y);
                        transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
                    }
                else if (isGrounded && !isAttack && !isThrow)
                    {
                        ChangeAnim("idle");
                        rb.velocity = Vector2.zero;
                    }
            }

        }
        if (!isGrounded && rb.velocity.y < 0f)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
    }
    public override void OnInit()
    {   
        base.OnInit();
        isDead = false;
        isJumping = false;
        transform.position = savePoint;
        ChangeAnim("idle");
        DeactiveAttack();
        SavePoint();
        UIManager.instance.SetCoin(coin);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
       
    }
    private bool CheckGrounded()
    { 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        else { return false; }
    }
    private void Attack()
    {
        rb.velocity = Vector2.zero;
        isAttack = true;
        ChangeAnim("attack");
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeactiveAttack), 0.5f);
        
    }
    private void Throw()
    {
        rb.velocity = Vector2.zero;
        isThrow = true;
        ChangeAnim("throw");
        Instantiate(kunai, throwPoint.position, throwPoint.rotation);
        Invoke(nameof(ResetThrow), 0.5f);
    }
    private void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }
    private void ResetThrow()
    {
        ChangeAnim("idle");
        isThrow = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {   
            claimItem.Play();
            Destroy(collision.gameObject);
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.SetCoin(coin);
        }
        if(collision.tag == "Deathzone")
        {   
            isDead = true;
            ChangeAnim("die");
            Invoke(nameof(OnInit),1f);
        }
        if(collision.tag == "HealItem")
        {
            claimItem.Play();
            Instantiate(Heal_VFX, transform.position + Vector3.left , transform.rotation);
            Debug.Log("hoi mau");
            this.OnHit(-30f);
            Destroy(collision.gameObject) ;
        }
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeactiveAttack()
    {
        attackArea.SetActive(false);
    }
    public void SetMove(float horizontal)
    {
        
        this.horizontal = horizontal;
    }
    public void SetSleep()
    {
        rb.velocity = Vector2.zero;
        isSleep = true;
        ChangeAnim("sleep");
    }
    public void SetAwake()
    {
        isSleep = false;
        ChangeAnim("idle");
    }
}
