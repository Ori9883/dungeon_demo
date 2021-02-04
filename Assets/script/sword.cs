using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public int damage;
    public float speed;
    public float arrawDistance;

    private Rigidbody2D rg2d;
    private Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        distory();
        /*    float distance = (transform.position - startPos).sqrMagnitude;
            if (distance > arrawDistance)
            {
                Destroy(gameObject);
            }
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    void distory()
    {
        new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}