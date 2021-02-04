using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
//	int destroyTime = 3;

	//dar disparo
	public bool disparo;
	private bool hitted; 
	public bool dead = false;

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

		//movement   >
		if (!dead)//站立，追击的判断
		{
			if (Vector2.Distance(transform.position, player.transform.position) < 5)
			{
				disparo = true;
				push = true;
				speed = 0;
				anim.SetBool("stand", false);
			}

			//  <
			else if (Vector2.Distance(transform.position, player.transform.position) > 5 && Vector2.Distance(transform.position, player.transform.position) < 15)
			{
				disparo = true;
				speed = 2.0f;
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position + mov, speed * Time.deltaTime);
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
//		anim.SetFloat("Axis_X", mov.x);
//		anim.SetFloat("Axis_Y", mov.y);

		//animations .

		//vertical
		if (mov.y < 0)
		{
			right = false;
//			anim.SetBool("right", right);

		}
		else if (mov.y > 0)
		{
			right = false;
//			anim.SetBool("right", right);
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
//			anim.SetBool("right", right);

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
//			anim.SetBool("right", right);
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
}
