using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scr_Shop : MonoBehaviour, GetDamage
{
    public enum Shop
    {
        DPM,
        Bullet,
        Move,
        BulletRe,
        MoveRe,
        HP,
        HPRe
    }

    public Shop buy;
    private int LV;
    public GameObject Canvas;
    public bool Show;
    public float Time;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Show)
        {
            ShowLevel();
        }
        if (Time > 0)
        {
            Time -= 0.9f;
        }
        else if (Time < -1)
        {
            return;
        }
    }

    public void Damage(float A_DMP)
    {
        var user = GameObject.FindGameObjectWithTag("Player");
        switch (buy)
        {
            case Shop.DPM:
                user.GetComponent<Player>().DPMUp();
                break;
            case Shop.Bullet:
                user.GetComponent<Player>().BulletUp();
                break;
            case Shop.Move:
                user.GetComponent<Player>().MoveUp();
                break;
            case Shop.BulletRe:
                user.GetComponent<Player>().BulletReUp();
                break;
            case Shop.MoveRe:
                user.GetComponent<Player>().MoveReUp();
                break;
            case Shop.HP:
                user.GetComponent<Player>().HPUp();
                break;
            case Shop.HPRe:
                user.GetComponent<Player>().HPReUp();
                break;
            default:
                break;
        }
    }

    public void ShowLevel()
    {
        var user = GameObject.FindGameObjectWithTag("Player");
        Canvas.SetActive(true);
        switch (buy)
        {
            case Shop.DPM:
                LV = user.GetComponent<Player>().DpmLV;
                Name.text = "Damage Up";
                break;
            case Shop.Bullet:
                LV = user.GetComponent<Player>().BulletLV;
                Name.text = "Max Bullet Up";
                break;
            case Shop.Move:
                LV = user.GetComponent<Player>().MoveLV;
                Name.text = "Max Move Up";
                break;
            case Shop.BulletRe:
                LV = user.GetComponent<Player>().BulletReLV;
                Name.text = "Bullet Restore Up";
                break;
            case Shop.MoveRe:
                LV = user.GetComponent<Player>().MoveReLV;
                Name.text = "Move Restore Up";
                break;
            case Shop.HP:
                LV = user.GetComponent<Player>().HPLV;
                Name.text = "Max Hp Up";
                break;
            case Shop.HPRe:
                LV = user.GetComponent<Player>().HPReLV;
                Name.text = "HP Restore Up";
                break;
            default:
                break;
        }
        Level.text = LV.ToString();
        if (Time < 0)
        {
            Show = false;
            Canvas.SetActive(false);
        }
    }

    public void Setting()
    {
        Show = true;
        Time = 1;
    }


}
