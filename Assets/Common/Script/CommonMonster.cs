using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CommonMonster : MonoBehaviour
{
	///나중에 포폴용으론 이걸로  AI짜
	public enum State
	{
		Taunting,
		Dizzy,
		Idle,
		Move,
		Attack,
		Die,
		Victory

	};
	public float MaxHP;

	public float HP;
	
	public float AttackPower;


	public float AttackDist;

	public Animator anim;

	public Collider AttackColl;

	public Transform AttackOffset;

	public Transform PlayerTarget;

	public GameObject PartcleObj;

	public GameObject RangeAttackObj;



	public AudioSource HitSound;

	public bool DieFT;

	public float Score;
	public float DownScore;

	private NavMeshAgent agent;
	private float Dist;

	public GameObject Coin;
	public void StartfindPlayer()
	{
		DieFT = false;
		agent = GetComponent<NavMeshAgent>();
		StartCoroutine(ScoreDown());
		if (GameObject.FindWithTag("Player"))
		{
			GameObject player = GameObject.FindWithTag("Player");
			PlayerTarget = player.transform;
			agent.SetDestination(PlayerTarget.position);
		}
	}

	public void DistCul()
	{
		Dist = Vector3.Distance(PlayerTarget.position, transform.position);
	}

	
	public void AttackDistCh()
	{
		if (DieFT)
		{
			agent.ResetPath();
			return;
		}
		Dist = Vector3.Distance(PlayerTarget.position, transform.position);
        if (Dist < AttackDist)
        {
            agent.ResetPath();
        }
        else
        {
            agent.SetDestination(PlayerTarget.position);
        }
	}


	public void AttackStart()
	{
		anim.SetTrigger("Attack");
		AttackColl.enabled = true;

	}

	public void RangeAttackStrat()
	{
		anim.SetTrigger("Fire");
		agent.ResetPath();
	}
	protected virtual void RangeAttack()
	{
		Instantiate(RangeAttackObj, AttackOffset.position, AttackOffset.rotation);
	}

	protected virtual void AttackEnd()
	{
		AttackColl.enabled = false;
	}
	
	protected virtual void Die()
	{
		if (DieFT)
		{
			return;
		}
		int A = Random.Range(0, 100);
		if (A <= 5)
		{
			Instantiate(Coin, this.transform.position + new Vector3(0,5f,0), this.transform.rotation);
		}
		anim.SetTrigger("Die");
		agent.ResetPath();
		Scr_PlayerManager.instance.SumScore(Score);
		DieFT = true;
	}

	protected virtual void PartcleStart()
	{
		Instantiate(PartcleObj, AttackOffset.position, AttackOffset.rotation);
	}

	IEnumerator ScoreDown()
	{
		while (true)
		{

			if (Score > 0)
			{
				Score -=DownScore;
			}
			yield return new WaitForSeconds(10f);
		}



	}


}



