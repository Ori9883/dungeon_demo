using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bull : Enemy
{
    private float skillCD = 3;
    private float nextattack = 0;

    public float bumptimes; //冲锋的速度倍数

    public override void Update()
    {
        base.Update();

        if(push == true)
        {
            if(nextattack <= Time.time)
            {
                Bump();
                anim.SetBool("bump", true);
                nextattack = Time.time + skillCD;
            }
        }
    }

    public void Bump()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 6.0f * Time.deltaTime);
    }

    public void BumpEND()
    {
        anim.SetBool("bump",false);
        push = false;
    }
}
