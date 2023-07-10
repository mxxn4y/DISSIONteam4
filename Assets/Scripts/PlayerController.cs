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

    private bool canAttack = true;
    public float attackSpeed = 5f; //공격 속도

    public float hpMax = 100;
    public float attackPower = 50;
    public float hpNow = 100;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //움직임 조작
        Move();

        //공격
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown()); //공격대기
        }

        //사망
        if (hpNow <= 0)
        {
            Dead();
        }

    }

    private void Move()
    {

        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")).normalized;
        transform.position += moveVec * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + moveVec);

        bool isMove = moveVec.magnitude != 0;
        animator.SetBool("isMove", isMove);
        if (isMove)
        {
            // 움직이는 경우
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
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

    private void Attack() //공격
    {
        Debug.Log("공격을 받아랏~ ");
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    private void Dead() //사망
    {
        Debug.Log("사망!");
    }
}
