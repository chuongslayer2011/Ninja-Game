using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState

{
    float timer;
    float randomtime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomtime = Random.Range(2.5f, 4f);
    }

    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > randomtime)
        {
            enemy.ChangeState(new PatrolState());
        }

    }

    public void OnExit(Enemy enemy)
    {

    }


}
