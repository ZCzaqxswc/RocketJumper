using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float DPM;
    public GameObject Boom;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

    }
     private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        GetDamage damage = other.GetComponent<GetDamage>();
        float DPM;
        DPM = Scr_PlayerManager.instance.Damage;

        if (damage != null) {
            damage.Damage(DPM);
            Instantiate(Boom, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
    }
}
