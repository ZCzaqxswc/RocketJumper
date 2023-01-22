using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour, GetDamage
{
    private bool getout;
    public GameObject Part;
    // Start is called before the first frame update
    void Start()
    {
        getout = false;
    }

    // Update is called once per frame
    public void Damage(float A_DMP)
    {
        if (getout)
        {
            return;
        }
        StartCoroutine(Exit());
        getout = true;
        Instantiate(Part, this.transform.position, this.transform.rotation);
    }


    IEnumerator Exit()
    {
        
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
