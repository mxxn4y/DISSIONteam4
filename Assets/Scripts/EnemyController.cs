using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum CurrentState { idle, trace, attack, dead };
    public CurrentState curState = CurrentState.idle; //�������

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Vector3 firstPosition; //ó����ġ

    public float traceDist = 15.0f; //�����Ÿ�
    public float attackDist = 3.0f; //���ݰŸ�
    private bool isDead = false;
    private bool canAttack = true;
    public float attackSpeed = 5f; //���� �ӵ�

    public float hpMax = 100;
    public float attackPower = 10;

    PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        //��ġ ���� -> �߰�

        firstPosition = this.transform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());

        player = GameObject.Find("Astronaut").GetComponent<PlayerController>();
    }

    IEnumerator CheckState() //���� ����
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(playerTransform.position, _transform.position);

            if (dist <= attackDist) //���ݹ���, ���ݵ�����0
            {
                curState = CurrentState.attack;
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
    }
    
    IEnumerator CheckStateForAction() //���¿� ���� �ൿ
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle: //����
                    nvAgent.destination = firstPosition;
                    //nvAgent.Stop();
                    break;
                case CurrentState.trace: //����
                    nvAgent.destination = playerTransform.position;
                    //nvAgent.Resume();
                    break;
                case CurrentState.attack: //����
                    if (canAttack)
                    {
                        Attack();
                        StartCoroutine(AttackCooldown()); //���ݴ��
                    }
                    break;
            }

            yield return null;
        }
    }
    private void Attack()
    {
        Debug.Log("����!  " + this.gameObject.name);
        // ����
        player.hpNow -= attackPower;
        Debug.Log("player now hp : " + player.hpNow);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
