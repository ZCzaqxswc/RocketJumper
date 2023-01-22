using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : CommonMonster, GetDamage
{
    public bool Att;
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        StartfindPlayer();
    }

    // Update is called once per frame
    void Update()
    {

        if (Att)
        {
            this.transform.LookAt(PlayerTarget);
            RangeAttackStrat();
            Att = false;
        }
        else 
        {
            AttackDistCh();
        }
    }

    protected override void AttackEnd()
    {
        base.AttackEnd();

    }

    protected override void RangeAttack()
    {
        base.RangeAttack();
        StartCoroutine(AttDelay());
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
        if (HP <= 0)
        {
            Die();
        }
    }

    void MeshDestroy()
    {
        Instantiate(PartcleObj, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator AttDelay()
    {
        yield return new WaitForSeconds(15f);
        Att = true;
    }

}
