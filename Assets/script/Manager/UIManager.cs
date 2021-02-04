using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Elements")]
    public Slider PlayerHealthBar;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void SetPlayerHealth(int health)
    {
        PlayerHealthBar.maxValue = health;
    }

    public void UpdatePlayerHealth(int health)
    {
        PlayerHealthBar.value = health;
    }
}
