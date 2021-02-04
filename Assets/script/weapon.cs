using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject sword;
    public Transform swordTransform;

    private GameObject findCamera;
    private Camera cam;
    private Vector3 mousePos;
    private Vector2 gunDirection;
    public Animator anim;
    private bool pull;

    [Header("Attack Setting")]
    public float nextAttack = 0;
    public float attackRate;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        findCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cam = findCamera.GetComponent<Camera>();
        pull = false;
    }

    // Update is called once per frame
    void Update()    //让武器指向跟随鼠标
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gunDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);
        if(Input.GetMouseButton(0) && Time.time > nextAttack)   //当按下鼠标左键发射弓箭
        {
            anim.Play("pull_bow");  
            Instantiate(sword,swordTransform.position,Quaternion.Euler(transform.eulerAngles));
            AudioManager.instance.PlayerShoot();
            nextAttack = Time.time + attackRate;

        }
    }
}