using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BetaPortal : MonoBehaviour
{
    public GameObject Portal;
    public GameObject BossPortal;
    [SerializeField]
    private bool Clear;
    public int level;
    public int Stage;
    public bool Boss;

    public bool End;
    // Start is called before the first frame update
    void Start()
    {
        Clear = false;
    }

    // Update is called once per frame
    void Update()
    {
        StageCh();

    }

    public void StageCh()
    {
        if (!GameObject.FindWithTag("Monster"))
        {
            Clear = true;
        }
        if (Clear)
        {
            if (Boss)
            {
                BossPortal.gameObject.SetActive(true);
            }
            else if(!Boss)
            {
                Portal.gameObject.SetActive(true);
            }
            
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            if (Clear)
            {
                if (End)
                {
                    SceneManager.LoadScene("0_Intro");
                    return;
                }
               SceneManager.LoadScene("Stage"+ level +"_" + Stage);

            }
        }
	}
}
