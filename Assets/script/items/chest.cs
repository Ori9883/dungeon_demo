using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public Animator anim;
    private Vector3 position;
    public GameObject thing;

    private void Start()
    {
        anim = GetComponent<Animator>();
        position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Open", true);
            AudioManager.instance.OpenChest();
        }
    }

    public void CreateThing()
    {
        Instantiate(thing,position, Quaternion.Euler(0,0,0));
    }
}
