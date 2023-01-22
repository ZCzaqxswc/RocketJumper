using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_StartMesh : MonoBehaviour, GetDamage
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float A_DMP)
    {
        SceneManager.LoadScene("Stage1_1");
    }
}
