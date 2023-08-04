using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    Rigidbody rigid;
    public Animator animator;
    //�����Ӽӵ�, �����ӵ� ���� - ����
    public float moveSpeed;
    public float jumpPower = 10f;
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
        
        //���� - �����Ӽӵ� ����
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
        //���� �ִϸ��̼� �߰� - ����
        animator.SetTrigger("Jump");
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
        //���� �ִϸ��̼� �߰� - ����
        animator.SetTrigger("Attack1");
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
        //��� �ִϸ��̼��߰� - ����
        animator.SetTrigger("Dead");
        Debug.Log("���!");
    }
}
