using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	public float health;
	public float speed;
	public bool push;

	//scales for the enemy
	public float scale_X;
	public float scale_Y;

	//sides
	public bool right = true;

	//anim for enemy
	public Animator anim;
	const float timeout = 4.0f;

	//the player that is following
	public GameObject player;
	private float distance;

	//life
	int destroyTime = 3;

	//dar disparo
	public bool disparo;
	public bool hitted;
	public bool dead = false;

	private float skillCD = 3.5f;
	private float nextattack = 0;

	void Start()
	{

		player = GameObject.FindGameObjectWithTag("Player");
		anim = gameObject.GetComponent<Animator>();

	}


	public virtual void Update()
	{
		distance = Vector2.Distance(transform.position, player.transform.position);
		Vector3 mov = new Vector3(
			0.01f,
			0.01f,
			0
		);

		anim.SetBool("hitted", hitted);

		//movement
		if (!dead && push == false)//站立，追击的判断
		{
			if (Vector2.Distance(transform.position, player.transform.position) < 5)
			{
				disparo = true;
				if (nextattack <= Time.time)
				{
					push = true;
					anim.SetBool("bump", push);
					nextattack = Time.time + skillCD;
				}
				anim.SetBool("stand", true);
			}

			else if (Vector2.Distance(transform.position, player.transform.position) > 5 && Vector2.Distance(transform.position, player.transform.position) < 15)
			{
				disparo = true;
				speed = 2.0f;
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
				anim.SetBool("stand", false);

			}
			else
			{
				disparo = false;
				anim.SetBool("stand", true);
			}
		}
		else
		{
			speed = 0;
		}

		if(push == true)
        {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 4.0f * Time.deltaTime);
		}


		if (mov.x < 0)
		{
			if (player.transform.position.x < transform.position.x)
			{
				transform.localScale = new Vector3(-scale_X, scale_Y, 1);
				right = false;
			}
			else
			{
				transform.localScale = new Vector3(scale_X, scale_Y, 1);
				right = true;
			}

		}
		else if (mov.x > 0)
		{
			if (player.transform.position.x > transform.position.x)
			{
				transform.localScale = new Vector3(scale_X, scale_Y, 1);
				right = true;
			}
			else
			{
				transform.localScale = new Vector3(-scale_X, scale_Y, 1);
				right = false;
			}
		}



	}

	//是否受伤和死亡判定
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			hitted = true;
			AudioManager.instance.EnemyHit();
			health = health - 1;
			Destroy(other.gameObject);
			if (health <= 0)
			{
				health = 0;
				dead = true;
				disparo = false;
				Destroy(gameObject);
			}
		}
	}
	public void ResetHitted()
	{
		hitted = false;
	}

	public bool GetSide()
	{
		return right;
	}

	public bool GetDisparo()
	{
		return disparo;
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
	}

	public void Bump()
	{
		Vector3 mov = new Vector3(0.01f,0.01f,0);
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position + mov, 6 * Time.deltaTime);
		
	}

	public void BumpEND()
	{
		push = false;
		anim.SetBool("bump", push);
	}
}
