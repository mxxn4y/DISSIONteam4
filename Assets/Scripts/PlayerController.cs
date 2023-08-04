using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    Rigidbody rigid;
    public Animator animator;
    //움직임속도, 점프속도 조정 - 수진
    public float moveSpeed;
    public float jumpPower = 10f;
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
        
        //수진 - 움직임속도 설정
        if (Input.GetKey(KeyCode.LeftShift)) moveSpeed = 8f;
        else moveSpeed = 5f;
        
        transform.position += moveVec * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + moveVec);

        animator.SetFloat("Speed", moveVec.magnitude*moveSpeed);
        

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        //점프 애니메이션 추가 - 수진
        animator.SetTrigger("Jump");
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
        //공격 애니메이션 추가 - 수진
        animator.SetTrigger("Attack1");
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
        //사망 애니메이션추가 - 수진
        animator.SetTrigger("Dead");
        Debug.Log("사망!");
    }
}
