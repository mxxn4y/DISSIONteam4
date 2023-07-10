using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    Rigidbody rigid;
    Animator animator;

    public float moveSpeed=10f;
    public float jumpPower = 5f;
    private bool canJump = true; //���� �˻�

    private bool canAttack = true;
    public float attackSpeed = 5f; //���� �ӵ�

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
        //������ ����
        Move();

        //����
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown()); //���ݴ��
        }

        //���
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
            // �����̴� ���
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
            canJump = true; //�ٴڿ� �־�� ���� ����
        }
    }

    private void Attack() //����
    {
        Debug.Log("������ �޾ƶ�~ ");
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    private void Dead() //���
    {
        Debug.Log("���!");
    }
}
