using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    Rigidbody rigid;
    Animator animator;

    public float moveSpeed=10f;
    public float jumpPower = 5f;
    private bool canJump = true; //점프 검사

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

    }

    private void Move()
    {

        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")).normalized;
        transform.position += moveVec * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + moveVec);

        bool isMove = moveVec.magnitude != 0;
        //animator.SetBool("isMove", isMove); //파라미터 bool형태~
        if (isMove)
        {
            //움직이는 경우
        }
    }

    private void Jump()
    {
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        canJump = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true; //바닥에 있어야 점프 가능
        }
    }
}
