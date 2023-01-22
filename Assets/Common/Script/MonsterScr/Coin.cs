using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player"))
        {
            var player = GameObject.FindWithTag("Player");
            Target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag ==("Player"))
        {
            Scr_PlayerManager.instance.Coin++;
            Destroy(gameObject);
        }
    }

}
