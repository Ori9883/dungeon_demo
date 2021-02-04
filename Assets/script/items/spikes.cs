using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralLevelGenerator.Unity.Examples.Common;

public class spikes : MonoBehaviour
{
    private PlayerMovement player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerMovement>();
            player.harm();
        }
    }
}
