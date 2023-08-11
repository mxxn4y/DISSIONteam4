using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum CurrentState { idle, roaming, trace, attack, dead };
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

        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());

        player = GameObject.Find("Player").GetComponent<PlayerController>(); //����
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
                case CurrentState.idle: //����(�⺻���� : ��ȸ) - ����
                    int waitTime = Random.Range(7, 10);
                    yield return new WaitForSeconds(waitTime);
                    curState = CurrentState.roaming;
                    break;

                case CurrentState.roaming: //��ȸ
                    Roaming();
                    break;

                case CurrentState.trace: //����
                    transform.LookAt(playerTransform.position);
                    nvAgent.destination = playerTransform.position;
                    break;

                case CurrentState.attack: //����
                    transform.LookAt(playerTransform.position);
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
        //�´� �ִϸ��̼� �߰� - ����
        player.animator.SetTrigger("Hit");
        Debug.Log("player now hp : " + player.hpNow);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    //���� ��ȸ �ڵ� - ����
    private void Roaming()
    {
        float dist = Vector3.Distance(playerTransform.position, _transform.position);

        Vector3 randomPosition = GetRandomRoamingPosition();

        // �ȴ� �ִϸ��̼� true
        transform.LookAt(randomPosition);
        nvAgent.destination = randomPosition;

        if (dist <= traceDist)
        {
            curState = CurrentState.trace;
            return;
        }

        // �ȴ� �ִϸ��̼� false, idle
        curState = CurrentState.idle;

    }

    Vector3 GetRandomRoamingPosition()
    {
        firstPosition = this.transform.position;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        Vector3 newPosition = firstPosition + randomDirection * 3f;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(newPosition, out navHit, 1.0f, NavMesh.AllAreas))
        {
            return navHit.position;
        }
        return newPosition;
    }
}
