using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonPartcle : MonoBehaviour
{
    [SerializeField]
    private GameObject BossSummon;
    [SerializeField]
    private GameObject Praticle;
    [SerializeField]
    private float SummonDelay;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Praticle, this.transform.position, this.transform.rotation);
        StartCoroutine(Summon());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator Summon()
    {
        yield return new WaitForSeconds(SummonDelay);
        Instantiate(BossSummon, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

}
