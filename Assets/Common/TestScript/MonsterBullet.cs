using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public int Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime *Speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            return;
        }
        GetDamage damage = other.GetComponent<GetDamage>();

        if (damage != null)
        {
            damage.Damage(10);
            Destroy(gameObject);
        }
    }

}
