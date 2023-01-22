using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MobMove : MonoBehaviour
{
    NavMeshAgent agent; // 에이전트의 목적지
                        //
    [SerializeField]
    Transform target;
    float Dist;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {

        MoveCh();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agent.SetDestination(target.position);
        }
        //agent.SetDestination(target.position);

    }

    private void MoveCh()
    {
        Dist = Vector3.Distance(target.position, transform.position);
        Debug.Log(Dist);
        if (Dist < 40)
        {
            agent.ResetPath();
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }
}
