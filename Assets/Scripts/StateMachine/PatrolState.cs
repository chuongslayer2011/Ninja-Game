using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomtime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomtime = Random.Range(3f, 6f);
    }

    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            if (enemy.isTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }        
        }
        else
        {
            if (timer < randomtime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
       
       
    }

    public void OnExit(Enemy enemy)
    {
       
    }

   
}
