using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soilball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float arrawDistance;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        startPos = transform.position;
    }

    // Update is called once per frame
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
        /*        if (collision.gameObject.CompareTag("Wall"))
                {
                    Destroy(gameObject);
                }   */
        if (collision.gameObject.name.Equals("Walls") || collision.gameObject.name.Equals("collideable"))
        {
            Destroy(gameObject);
        }
    }
}
