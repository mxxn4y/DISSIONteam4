using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraForm; // ī�޶� ����
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
        // �������� ����
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            //���� - �����Ӽӵ� ����
            if (Input.GetKey(KeyCode.LeftShift)) moveSpeed = 8f;//�޸���ӵ�
            else moveSpeed = 5f;//�ȴ¼ӵ�

            // ������ ����
            Vector3 lookForward = new Vector3(cameraForm.forward.x, 0f, cameraForm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraForm.right.x, 0f, cameraForm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; //�����̴� ����

            transform.forward = moveDir; //�����̴� �������� ����
            transform.position += moveDir * Time.deltaTime * moveSpeed; //�����̱�

            //ũ�� 1�� ���ͷ� ��ȯ�� �ӵ� ������
            //5�� �ȱ�, 8�̸� �޸���
            animator.SetFloat("Speed", moveInput.normalized.magnitude * moveSpeed);
        }
        else
        {
            Vector3 lookForward = new Vector3(cameraForm.forward.x, 0f, cameraForm.forward.z).normalized;
            transform.forward = lookForward;
            animator.SetFloat("Speed", moveInput.normalized.magnitude * moveSpeed);
        }
       
        //���� �˻�
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
