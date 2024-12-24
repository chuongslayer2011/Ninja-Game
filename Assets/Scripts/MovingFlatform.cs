using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatform : MonoBehaviour
{
    public LayerMask LayerMask;
    [SerializeField] private Transform aPoint, bPoint;
    [SerializeField] private Vector3 target;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = aPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            target = bPoint.position;
        }
        else if (Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            target = aPoint.position;
        }
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.transform.SetParent(transform);
        } 
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
