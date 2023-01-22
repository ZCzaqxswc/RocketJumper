using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : CommonMonster, GetDamage
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
        this.transform.LookAt(PlayerTarget);
        if (Att)
        {
            RangeAttackStrat();
            Att = false;
        }
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
        Destroy(gameObject);
    }

    IEnumerator AttDelay()
    {
        yield return new WaitForSeconds(3f);
        Att = true;
    }
}
