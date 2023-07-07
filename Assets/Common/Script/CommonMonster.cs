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

	//플레이어가 방에 입장했을때
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

	//공격
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

	//공격때 애니메이션 재생,콜라이더 활성화
	public void AttackStart()
	{
		anim.SetTrigger("Attack");
		AttackColl.enabled = true;

	}
	//원거리 몬스터 공격
	public void RangeAttackStrat()
	{
		anim.SetTrigger("Fire");
		agent.ResetPath();
	}
	//원거리 몬스터 공격오브젝트 생성
	protected virtual void RangeAttack()
	{
		Instantiate(RangeAttackObj, AttackOffset.position, AttackOffset.rotation);
	}
	//공격끝나고 콜라이더 끄기
	protected virtual void AttackEnd()
	{
		AttackColl.enabled = false;
	}
	
	//죽었을때
	protected virtual void Die()
	{
		if (DieFT)
		{
			return;
		}
		//확률적으로 코인 생성
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
	//죽었을때 이펙트 생성
	protected virtual void PartcleStart()
	{
		Instantiate(PartcleObj, AttackOffset.position, AttackOffset.rotation);
	}
	//시간이 지날때마나 점수가 떨어지는 기능
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



