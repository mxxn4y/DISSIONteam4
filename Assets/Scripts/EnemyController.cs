using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum CurrentState { idle, roaming, trace, attack, dead };
    public CurrentState curState = CurrentState.idle; //현재상태

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Vector3 firstPosition; //처음위치

    public float traceDist = 15.0f; //추적거리
    public float attackDist = 3.0f; //공격거리
    private bool isDead = false;
    private bool canAttack = true;
    public float attackSpeed = 5f; //공격 속도

    public float hpMax = 100;
    public float attackPower = 10;

    PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        //위치 설정 -> 추격

        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());

        player = GameObject.Find("Player").GetComponent<PlayerController>(); //수진
    }

    IEnumerator CheckState() //상태 변경
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(playerTransform.position, _transform.position);

            if (dist <= attackDist) //공격범위, 공격딜레이0
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
    
    IEnumerator CheckStateForAction() //상태에 따른 행동
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle: //멈춤(기본상태 : 배회) - 수진
                    int waitTime = Random.Range(7, 10);
                    yield return new WaitForSeconds(waitTime);
                    curState = CurrentState.roaming;
                    break;

                case CurrentState.roaming: //배회
                    Roaming();
                    break;

                case CurrentState.trace: //추적
                    transform.LookAt(playerTransform.position);
                    nvAgent.destination = playerTransform.position;
                    break;

                case CurrentState.attack: //공격
                    transform.LookAt(playerTransform.position);
                    if (canAttack)
                    {
                        Attack();
                        StartCoroutine(AttackCooldown()); //공격대기
                    }
                    break;
            }

            yield return null;
        }
    }
    private void Attack()
    {
        Debug.Log("공격!  " + this.gameObject.name);
        // 공격
        player.hpNow -= attackPower;
        //맞는 애니메이션 추가 - 수진
        player.animator.SetTrigger("Hit");
        Debug.Log("player now hp : " + player.hpNow);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    //몬스터 배회 코드 - 수진
    private void Roaming()
    {
        float dist = Vector3.Distance(playerTransform.position, _transform.position);

        Vector3 randomPosition = GetRandomRoamingPosition();

        // 걷는 애니메이션 true
        transform.LookAt(randomPosition);
        nvAgent.destination = randomPosition;

        if (dist <= traceDist)
        {
            curState = CurrentState.trace;
            return;
        }

        // 걷는 애니메이션 false, idle
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
