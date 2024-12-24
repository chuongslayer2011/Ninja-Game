using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Phat hien muc tieu");
            enemy.SetTarget(collision.GetComponent<Character>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("muc tieu chay thoat");
            enemy.SetTarget(null);
        }
    }
}
