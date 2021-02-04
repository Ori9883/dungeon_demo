using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
	public GameObject player;
	public Animator anim;
	public Rigidbody2D rb;

	[Header("Attack")]
	public GameObject bullet;
	public bool canFire;
	private float z = 0; //子弹角度
	private float fireTime;
	private float coolTime = 2.5f;
	private Vector3 firePosition;
	private IEnumerator current;

	[Header("State")]
	public int health = 10;
	public bool dead = false;
	public float speed;



	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		anim = gameObject.GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		canFire = true;

		current = nullFire();
		StartCoroutine(current);
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 mov = new Vector3(
		0.01f,
		0.01f,
		0
		);

		if(fireTime <= coolTime)
        {
			fireTime += Time.deltaTime;
        }

		//movement   >
		if (!dead)//站立，追击的判断
		{
			if (Vector2.Distance(transform.position, player.transform.position) < 5)
			{
				speed = 0;
				anim.SetBool("stand", true);
				if(canFire == true && fireTime >= coolTime)
                {
					StopCoroutine(current);
					current = Fire();
					StartCoroutine(current);
					fireTime = 0;
				}
			}

			//  <
			else if (Vector2.Distance(transform.position, player.transform.position) > 5 && Vector2.Distance(transform.position, player.transform.position) < 15)
			{
				if(canFire == true && fireTime >= coolTime)
                {
					StopCoroutine(current);
					current = Firecircle();
					StartCoroutine(current);
					fireTime = 0;
                }
				speed = 2.0f;
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
				anim.SetBool("stand", false);

			}
			else
			{
				anim.SetBool("stand", true);
			}
		}
		else
		{
			speed = 0;
		}

		if (mov.x < 0)
		{
			if (player.transform.position.x < transform.position.x)
			{
				transform.localScale = new Vector3(-1, 1, 1);
			}
			else
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
		}
		else if (mov.x > 0)
		{
			if (player.transform.position.x > transform.position.x)
			{
				transform.localScale = new Vector3(1, 1, 1);

			}
			else
			{
				transform.localScale = new Vector3(-1, 1, 1);

			}
		}

	}

	//是否受伤和死亡判定
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			health = health - 1;
			Destroy(other.gameObject);
			if (health <= 0)
			{
				health = 0;
				dead = true;
				anim.SetBool("dead", dead);
			}
		}
	}

	public void DestroyGameObject() //game end
    {
		Destroy(gameObject);
    }

	private IEnumerator nullFire()
    {
		Debug.Log("can fire");
		yield return null;
    }

	private IEnumerator Fire()
    {
		canFire = false;
		Vector3 fireDirection;
		for (int i = 0;i<3;i++)
        {
			for(int j = 0; j < 3; j++)
            {
				fireDirection = (player.transform.position - transform.position).normalized;
				firePosition = transform.position;
				float angle = 0 - (90 - Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg);
				Debug.Log(angle);
				switch (j)
                {
					case 0:
						Instantiate(bullet, firePosition, Quaternion.Euler(0,0,angle-10));
						break;

					case 1:
						Instantiate(bullet, firePosition, Quaternion.Euler(0, 0, angle));
						break;

					case 2:
						Instantiate(bullet, firePosition, Quaternion.Euler(0, 0, angle+10));
						break;

				}
			}
			yield return new WaitForSeconds(0.5f);
		}
		canFire = true;
		yield return null;
    }

	private IEnumerator Firecircle()
    {
		canFire = false;
		Vector3 fireDirection = new Vector3(0,1,0);
		Quaternion startQuaternion = Quaternion.AngleAxis(10, Vector3.forward);

		for(int i=0; i <3; i++)
        {
			firePosition = transform.position;
			for(int j=0; j < 18; j++)
            {
				Instantiate(bullet, firePosition, Quaternion.Euler(0, 0, 0 + z));
/*				Instantiate(bullet, firePosition, Quaternion.Euler(fireDirection));
				fireDirection = startQuaternion * fireDirection;
				Debug.Log(fireDirection); */
				z = z + 20;
            }
			yield return new WaitForSeconds(0.5f);
        }

		canFire = true;
		yield return null;
    }
}
