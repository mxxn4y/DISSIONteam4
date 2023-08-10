using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraForm; // 카메라 정보
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
        // 방향정보 설정
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            //수진 - 움직임속도 설정
            if (Input.GetKey(KeyCode.LeftShift)) moveSpeed = 8f;//달리기속도
            else moveSpeed = 5f;//걷는속도

            // 움직임 설정
            Vector3 lookForward = new Vector3(cameraForm.forward.x, 0f, cameraForm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraForm.right.x, 0f, cameraForm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; //움직이는 방향

            transform.forward = moveDir; //움직이는 방향으로 보기
            transform.position += moveDir * Time.deltaTime * moveSpeed; //움직이기

            //크기 1인 벡터로 변환후 속도 곱해줌
            //5면 걷기, 8이면 달리기
            animator.SetFloat("Speed", moveInput.normalized.magnitude * moveSpeed);
        }
        else
        {
            Vector3 lookForward = new Vector3(cameraForm.forward.x, 0f, cameraForm.forward.z).normalized;
            transform.forward = lookForward;
            animator.SetFloat("Speed", moveInput.normalized.magnitude * moveSpeed);
        }
       
        //점프 검사
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
