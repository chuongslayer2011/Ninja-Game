using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float movingSpeed;
    [SerializeField] public Rigidbody2D rb;
    private Character target;
    [SerializeField] private GameObject attackArea;
    private bool isDead = false;
    public Character Target => target;
    // Start is called before the first frame update
    private IState currentState;
    private bool isRight = true;
    private void Update()
    {
       if (currentState != null && !isDead)
        {
            currentState.OnExcute(this);
            
        }
    }
    public override void OnInit()
    {
        isDead = false;
        base.OnInit();
        ChangeState(new IdleState());
        
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(heathBar.gameObject);
        Destroy(gameObject);
    }
    protected override void OnDeath()
    {
        isDead = true;
        ChangeState(null);
        base.OnDeath();
    }
    
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void SetTarget(Character character)
    {
         this.target = character;
         if (isTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (Target != null) 
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * movingSpeed;
    }
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        Debug.Log("tancong");
        ChangeAnim("attack");
        attackArea.SetActive(true);
        Invoke(nameof(DeactiveAttack), 0.5f);
    }
    public bool isTargetInRange()
    {   
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

       
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    private void DeactiveAttack()
    {
        attackArea.SetActive(false);
    }
}
