using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
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
            transform.rotation = Quaternion.Euler(Vector3.zero);
            target = bPoint.position;
        }
        else if (Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
            target = aPoint.position;
        }
    }
    
}
