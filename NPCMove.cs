using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{

    Rigidbody rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {

            float dir1 = Random.Range(-5f, 5f);
            float dir2 = Random.Range(-5f, 5f);

            yield return new WaitForSeconds(2);

            anim.SetBool("isWalking", true);
            rigid.velocity = new Vector3(dir1, 1, dir2);

        }
    }

    //private void Start()
    //{
    //    while (true)
    //    {

    //        float dir1 = Random.Range(-5f, 5f);
    //        float dir2 = Random.Range(-5f, 5f);

    //        rigid.velocity = new Vector3(dir1, 1, dir2);
    //    }
    //}

}