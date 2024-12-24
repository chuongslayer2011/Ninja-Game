using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject hitVFX;
    [SerializeField] private GameObject meatPrefab;
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn),1f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(30f);
            
            OnDespawn();
        }
        if (collision.CompareTag("Animal"))
        {
            Destroy(collision.gameObject);
            Instantiate(meatPrefab, transform.position, Quaternion.identity);
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }

}
