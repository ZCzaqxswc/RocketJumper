using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public float AttackPower;

	public float AttackDelay;

	public float AttackDist;

	public Animator anim;

	public Collider AttackColl;

	public Transform AttackOffset;

	public Transform PlayerTarget;

	public GameObject PartcleObj;

	public GameObject RangeAttackObj;

	private UnityEngine.AI.NavMeshAgent agent;
	private Transform target;

    public void StartfindPlayer()
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		if(GameObject.FindWithTag("Player"))
		{
			GameObject player = GameObject.FindWithTag("Player");
			target = player.transform;
			agent.SetDestination(target.position);
		}
	}

    public void AttackStart()
	{
		anim.SetTrigger("Attack");
		AttackColl.enabled = true;
	}


}
