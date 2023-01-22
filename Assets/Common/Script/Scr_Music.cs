using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Music : MonoBehaviour
{
    public AudioClip[] bgm;
    public AudioSource Aud;

    public int BGMIndex = 999;
    // Start is called before the first frame update

	private void Start()
	{
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MusicPlay(SceneManager.GetActiveScene().name);
    }

    public void MusicPlay(string scene)
    {
        switch (scene)
        {
            case "0_Intro":
                if (BGMIndex != 0)
                {
                    BGMIndex = 0;
                    Aud.clip = bgm[0];
                    Aud.Play();
                }
                break;
            case "Stage1_1":
            case "Stage1_2":
            case "Stage1_3":
            case "Stage1_4":
            case "Stage1_5":
                if (BGMIndex != 1)
                {
                    BGMIndex = 1;
                    Aud.clip = bgm[BGMIndex];
                    Aud.Play();
                }
                break;

            case "Stage1_6":
            case "Stage2_6":
            case "Stage3_6":
                if (BGMIndex != 2)
                {
                    BGMIndex = 2;
                    Aud.clip = bgm[2];
                    Aud.Play();
                }
                break;
            case "Stage2_1":
            case "Stage2_2":
            case "Stage2_3":
            case "Stage2_4":
            case "Stage2_5":
                if (BGMIndex != 3)
                {
                    BGMIndex = 3;
                    Aud.clip = bgm[BGMIndex];
                    Aud.Play();
                }
                break;
            case "Stage3_1":
            case "Stage3_2":
            case "Stage3_3":
            case "Stage3_4":
            case "Stage3_5":
                if (BGMIndex != 4)
                {
                    BGMIndex = 4;
                    Aud.clip = bgm[BGMIndex];
                    Aud.Play();
                }
                break;

            default:
                BGMIndex = 999;
                Aud.Stop();
                break;
        }
    }
}
