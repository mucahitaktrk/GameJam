using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityScript : MonoBehaviour
{

    private NavMeshAgent securityNavMeshAgent;
    [SerializeField] private Transform[] securityPostion;
    private int securityPostionCount = 0;
    float time = 0;

    [SerializeField] private Transform playerTransfor;

    private Animator securityAnimator;

    void Start()
    {
        securityNavMeshAgent = GetComponent<NavMeshAgent>();
        securityAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        time += Time.deltaTime;
        if ( time >= 5f)
        {
            securityNavMeshAgent.SetDestination(new Vector3(
                securityPostion[securityPostionCount].position.x,
               securityPostion[securityPostionCount].position.y,
                securityPostion[securityPostionCount].position.z
                ));
            securityPostionCount = Random.Range(0,securityPostion.Length);
            time = 0;

        }
        if (securityNavMeshAgent.velocity.z == 0 && securityNavMeshAgent.velocity.x == 0)
        {
            securityAnimator.SetBool("Run", true);
        }
        else
        {
            securityAnimator.SetBool("Run", false);
        }
        if (Vector3.Distance(transform.position , playerTransfor.position) <= 5f && !PlayerScript.isHide)
        {
            Debug.Log("Yakalandın !!");
        }
    }
}
