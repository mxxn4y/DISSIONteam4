using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum CurrentState { idle, trace, attack, dead };
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

        firstPosition = this.transform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());

        player = GameObject.Find("Astronaut").GetComponent<PlayerController>();
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
                case CurrentState.idle: //멈춤
                    nvAgent.destination = firstPosition;
                    //nvAgent.Stop();
                    break;
                case CurrentState.trace: //추적
                    nvAgent.destination = playerTransform.position;
                    //nvAgent.Resume();
                    break;
                case CurrentState.attack: //공격
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
        Debug.Log("player now hp : " + player.hpNow);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
