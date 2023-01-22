using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public float DPM;
    private void OnTriggerEnter(Collider other)
    {
        GetDamage damage = other.GetComponent<GetDamage>();

        if (damage != null)
        {
            damage.Damage(DPM);
        }
    }
}
