using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralLevelGenerator.Unity.Examples.Common;

public class medicine : MonoBehaviour
{
    private PlayerMovement player;
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Pick"))
            {
                Debug.Log("health + 1 !!!");
                Destroy(gameObject);
                player = collision.gameObject.GetComponent<PlayerMovement>();
                Debug.Log("start");
                player.treat();
                AudioManager.instance.Treat();
                Debug.Log("end");
            }
        }
    }
}

