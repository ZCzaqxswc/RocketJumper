using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ScoreData
{
    public float Score;
    public ScoreData(float A_Score)
    {
        Score = A_Score;
    }

}


public class Scr_PlayerManager : MonoBehaviour
{
    public static Scr_PlayerManager instance;

    public int GunMax;
    public int GunMaxRe;
    public int MoveMax;
    public int MoveMaxRe;
    public int MaxHP;
    public int HPRe;
    public int Damage;
    public int Coin;
    public GameObject Player;

    public float HighScore;
    public float nowScore;
    public float Record;

    private List<ScoreData> DataInfo;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        if (!instance)
        {
            instance = this;

        }
        
            DontDestroyOnLoad(this);
        if (!GameObject.FindWithTag("Player"))
        {
            Instantiate(Player, this.transform.position, this.transform.rotation);
        }
            
    }
    // Start is called before the first frame update
    void Start()
    {
        DataInfo = new List<ScoreData>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        ResetStat();
        Reset();
        Load();
    }

    // Update is called once per frame
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        name = SceneManager.GetActiveScene().name;
        if (name == "0_Intro")
        {
            nowScore += Coin * 300f;
            Coin = 0;
            if (nowScore > HighScore)
            {
                HighScore = nowScore;
                if (DataInfo.Count == 0)
                {
                    ScoreData asd = new ScoreData(HighScore);
                    DataInfo.Add(asd);
                }
                else
                {
                    if (DataInfo[0].Score < HighScore)
                    {
                        DataInfo[0].Score = HighScore;
                    }
                }
                string DB = JsonConvert.SerializeObject(DataInfo, Formatting.Indented);
                File.WriteAllText(Path.Combine(Application.persistentDataPath, "Save.json"), DB);
            }
            nowScore = 0;

        }
    }

    public void SumScore(float A_Score)
    {
        nowScore += A_Score;
    }

    void ResetStat()
    {
        GunMax = 100;
        GunMaxRe = 1;
        MoveMax = 0;
        MoveMaxRe = 6;
        MaxHP = 100;
        HPRe = 10;
        Damage = 10;
    }
    public void Reset()
    {
        nowScore = 0;
        Coin = 0;
    }

	public void Load()
	{
        string text = "";
        if (File.Exists(Path.Combine(Application.persistentDataPath, "Save.json")))
        {
            text = File.ReadAllText(Path.Combine(Application.persistentDataPath, "Save.json"));
            Debug.Log(text);
            DataInfo = JsonConvert.DeserializeObject<List<ScoreData>>(text);
            HighScore = DataInfo[0].Score;
        }
    }
}
