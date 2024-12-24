using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject hitVFX;
    [SerializeField] private GameObject meatPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "Player")
        {
            
            collision.GetComponent<Character>().OnHit(30f);
        }
        if(collision.tag == "Animal")
        {
            Destroy(collision.gameObject);
            Instantiate(meatPrefab, transform.position, Quaternion.identity);
            Instantiate(hitVFX, transform.position, transform.rotation);
        }
    }
    
}
