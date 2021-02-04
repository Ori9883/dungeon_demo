using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float arrawDistance;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > arrawDistance)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //            collision.GetComponent<>().TakeDamage(damage);
        }
        /*       if (collision.gameObject.CompareTag("Wall"))
               {
                   Destroy(gameObject);
               }  */
        if (collision.gameObject.name.Equals("Walls") || collision.gameObject.name.Equals("collideable"))
        {
            Destroy(gameObject);
        }
    }
}
