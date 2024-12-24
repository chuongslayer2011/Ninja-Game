using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private float hp;
    private const float maxHp = 100;
    private bool IsDead => hp <= 0;
    private string currentAnimName;
    [SerializeField] public Animator animator;
    [SerializeField] protected Heathbar heathBar;
    [SerializeField] protected CombatText combatTextPrefab;
    [SerializeField] protected GameObject hitVFX;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = 100;
        heathBar.OnInit(100, transform);
    }

    public virtual void OnDespawn()
    {

    }
    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            hp -= damage;
        }
        else
        {
            hp = 0;
            OnDeath();
        }
        if (damage < 0)
        {
            hp = hp - damage < maxHp ? hp - damage : maxHp;
            
        }
        heathBar.SetNewHp(hp);
        Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).Oninit(damage);
        if (damage > 0)
        {
            Instantiate(hitVFX, transform.position  + Vector3.down, transform.rotation);
            
        }

    }

   
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }
    
    

}
