using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource playerAudiosource,enemyAudio,itemsAudio;

    [Header("playerMusic")]
    [SerializeField]
    private AudioClip shoot, playerHit;

    [Header("EnemyMusic")]
    [SerializeField]
    private AudioClip enemyShoot, enemyHit;


    [Header("ItemsMusic")]
    [SerializeField]
    private AudioClip open, treat,hitWall;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerShoot()
    {
        playerAudiosource.clip = shoot;
        playerAudiosource.Play();
    }

    public void PlayerHit()
    {
        playerAudiosource.clip = playerHit;
        playerAudiosource.Play();
    }

    public void EnemyShoot()
    {
        if(enemyAudio.isPlaying == false)
        {
            enemyAudio.clip = enemyShoot;
            enemyAudio.Play();
        }
    }

    public void EnemyHit()
    {
        if(enemyAudio.isPlaying == false)
        {
            enemyAudio.clip = enemyHit;
            enemyAudio.Play();
        }
    }

    public void OpenChest()
    {
        itemsAudio.clip = open;
        itemsAudio.Play();
    }

    public void Treat()
    {
        itemsAudio.clip = treat;
        itemsAudio.Play();
    }

    public void HitWall()
    {
        if(itemsAudio.isPlaying == false)
        {
            itemsAudio.clip = hitWall;
            itemsAudio.Play();
        }
    }
}
