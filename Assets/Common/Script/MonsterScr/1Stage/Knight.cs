using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight :  CommonMonster , GetDamage
{
    public GameObject BossDie;
    // Start is called before the first frame update
    [SerializeField]
    private float MoveDelay;
    void Start()
    {
        HP = MaxHP;
        StartCoroutine(BossStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTarget == null)
        { return; }
        AttackDistCh();
	}

	protected override void AttackEnd()
	{
		base.AttackEnd();

	}

    protected override void Die()
    {
        base.Die();
    }

	protected override void PartcleStart()
	{
		base.PartcleStart();
	}

	public void Damage(float A_DMP)
	{
        HP -= A_DMP;
        HitSound.Play();
        if (HP<=0)
        {

            Die();
        }
	}

    void MeshDestroy()
    {
        Instantiate(Coin, this.transform.position, this.transform.rotation);
        Instantiate(BossDie, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator BossStart()
    {
        yield return new WaitForSeconds(MoveDelay);
        StartfindPlayer();
    }

}
