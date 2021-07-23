using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterControl : MonoBehaviour
{

    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    public State state = State.IDLE;

    [SerializeField] private Transform playerTr;
    [SerializeField] private Transform monsterTr;
    [SerializeField] private NavMeshAgent agent;
    private Animator anim;

    public float attackDist = 2.0f;

    public float traceDist = 10.0f;

    private int hashIsTrace = Animator.StringToHash("IsTrace");
    private int hashsIsAttack = Animator.StringToHash("IsAttack");

    public bool isDie = false;

    public float hp = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject playerObject = GameObject.FindGameObjectWithTag("PLAYER");
        //playerTr = playerObject.GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        monsterTr = this.gameObject.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        StartCoroutine(MonsterBehaviour());
    }

    IEnumerator MonsterBehaviour()
    {
        while (isDie == false)
        {
            CheckMonsterState();
            MonsterAction();
            yield return new WaitForSeconds(0.3f);
        }
    }

    void CheckMonsterState()
    {
        float distance = Vector3.Distance(playerTr.position, monsterTr.position);

        if (distance <= attackDist)
            state = State.ATTACK;
        else if (distance < traceDist)
            state = State.TRACE;
        else
            state = State.IDLE;
    }

    void MonsterAction()
    {
        switch (state)
        {
            case State.IDLE:
                agent.isStopped = true;
                anim.SetBool(hashIsTrace, false);
                break;
            case State.ATTACK:
                agent.isStopped = true; 
                anim.SetBool(hashsIsAttack, true);
                break;
            case State.TRACE:
                agent.SetDestination(playerTr.position);
                agent.isStopped = false;
                anim.SetBool(hashIsTrace, true);
                anim.SetBool(hashsIsAttack, false);
                break;
            case State.DIE:

                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            Destroy(collision.gameObject);
            anim.SetTrigger("Hit");

            hp -= 20;
            if(hp <= 0.0f)
            {
                MonsterDie();
            }
        }
    }

    void MonsterDie()
    {
        isDie = true;
        StopAllCoroutines();
        anim.SetTrigger("Die");
        agent.isStopped = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    void YouWin()
    {
        if (isDie == true) return;

        StopAllCoroutines();
        anim.SetFloat("DanceSpeed", Random.Range(0.8f, 1.5f));
        anim.SetTrigger("Dance");
        agent.isStopped = true;
    }
}
