using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
	//shooting
	private float speed = 300;
	public GameObject enemy;

	//time 4 each shoot
	private float forshots;
	public float startime;

	//
	public Rigidbody2D bullet;
	public GameObject player;
	bool shoot;


	// Use this for initialization
	void Start()
	{
		forshots = startime;
		player = GameObject.FindGameObjectWithTag("Player");


	}

	// Update is called once per frame
	void Update()
	{
		if (Vector2.Distance(transform.position, player.transform.position) > 3)
		{

			Enemy enemies = enemy.gameObject.GetComponent<Enemy>();
			bool right = enemies.GetSide();

			shoot = enemies.GetDisparo();

			if (shoot)
			{
				if (forshots <= 0)
				{
					Rigidbody2D bala = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)));
					AudioManager.instance.EnemyShoot();
					if (!right)
					{
						bala.AddRelativeForce(new Vector2(1.5f, 0f) * speed);
					}
					else
					{
						bala.AddRelativeForce(new Vector2(1.5f, 0f) * speed);
					}
					forshots = startime;
				}
				else
				{
					forshots -= Time.deltaTime;
				}
			}
		}


	}


}