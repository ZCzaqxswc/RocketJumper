using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : CommonMonster, GetDamage
{
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        StartfindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
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

}
