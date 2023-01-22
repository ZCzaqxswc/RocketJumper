using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player"))
        {
            var User = GameObject.FindWithTag("Player");
            User.transform.position = this.transform.position;
            User.transform.rotation = this.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
