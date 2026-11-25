using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    // 몬스터의 상태 정보
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }
    [SerializeField] State state = State.IDLE;
    [SerializeField] float traceDist = 10.0f;
    [SerializeField] float attackDist = 2.0f;
    [SerializeField] bool isDie = false;
    Transform monsterTr;
    Transform playerTr;
    NavMeshAgent agent;
    Animator anim;
    GameObject bloodEffect;
    // Animator 파라미터의 해시값 추출
    readonly int hashTrace = Animator.StringToHash("IsTrace");
    readonly int hashAttack = Animator.StringToHash("IsAttack");
    readonly int hashHit = Animator.StringToHash("Hit");
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");
        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;
                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            // 총알 삭제
            Destroy(collision.gameObject);
            // 피격 애니메이션 실행
            anim.SetTrigger(hashHit);
            // 총알의 충돌 지점
            Vector3 pos = collision.GetContact(0).point;
            // 총돌지점의 법선벡터
            Quaternion rot = Quaternion.
                LookRotation(-collision.GetContact(0).normal);
            // 혈흔효과 생성
            ShowBloodEffect(pos, rot);
        }    
    }

    void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {
        // 혈흔효과 생성
        GameObject blood = Instantiate<GameObject>(bloodEffect,
            pos, rot, monsterTr);
        Destroy(blood, 1.0f);
    }
    void OnDrawGizmos()
    {
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
}
