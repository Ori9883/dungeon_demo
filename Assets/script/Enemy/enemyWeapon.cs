using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeapon : MonoBehaviour
{

	public GameObject player;
	public GameObject enemies;

    // Use this for initialization
    private void Awake()
    {
		player = GameObject.FindGameObjectWithTag("Player");
	}

    void Start()
	{

	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
	{
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;

	}

	// Update is called once per frame
	void Update()   //控制敌人武器指向
	{
		Enemy enemy = enemies.gameObject.GetComponent<Enemy>();
		Vector2 positionOnScreen = transform.position;
		bool right = enemy.GetSide();
		Vector2 playerOnScreen = player.transform.position;


		float angle = AngleBetweenTwoPoints(playerOnScreen, positionOnScreen);

		if (right)
		{
			if (angle < 90 && angle > -90)
			{
				transform.localScale = new Vector3(1f, 1);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

			}
			else
			{
				transform.localScale = new Vector3(-1f, 1f);
				angle = AngleBetweenTwoPoints(positionOnScreen, playerOnScreen);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
			}
		}
		else
		{
			if (angle < 90 && angle > -90)
			{
				transform.localScale = new Vector3(1f, -1f);
				AngleBetweenTwoPoints(positionOnScreen, playerOnScreen);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

			}
			else
			{
				transform.localScale = new Vector3(-1f, -1f);
				angle = AngleBetweenTwoPoints(playerOnScreen, positionOnScreen);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
			}
		}
	}
}
