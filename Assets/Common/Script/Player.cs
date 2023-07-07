using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class Player : MonoBehaviour, GetDamage
{
    public GameObject bullet;
    public Transform firePos;
    public Transform Power;
    public Transform HitOffset;
    public Rigidbody RB;
    public float PowerPos;
    public Slider hpbar;
    public Slider BulletBar;
    public Slider MoveBar;
    public TextMeshProUGUI MoveText;
    [SerializeField]
    Vector3 Dir;

    public GameObject MoveBullet;
    public GameObject VRCam;
    public GameObject Muzzle;
    public GameObject MoveMuzzle;
    public GameObject HitEffect;
    public GameObject OverLoadEffect;
    public GameObject LVUI;
    public GameObject ScoreUI;


    [SerializeField]
    //현재 이동가능횟수
    private int nowmove;
    [SerializeField]
    private float MaxHP;
    [SerializeField]
    private float HP;
    //방이동할때 회복되는량
    [SerializeField]
    private float HPrestore;
    [SerializeField]
    //이동가능횟수 최대치
    private int maxMove;
    [SerializeField]
    //총알 최대치

    private float BulletLim;
    //현재 총알 수치
    [SerializeField]
    private float nowBulletLim;
    //총알 회복력
    [SerializeField]
    private float BulletRe;

    //이동 딜레이
    [SerializeField]
    private float MoveDealy;

    private float BulletDelay;



    private GunMode Mode;
    private bool Shot;
    public AudioSource FireSound;
    public AudioSource MoveSound;
    public AudioSource PortalSound;
    private enum GunMode 
    {
        shot,
        auto
    }
    public Image NowMode;
    public Sprite[] Spr_Mode;

    RaycastHit Ray;
    public GameObject CrossHair;
    [SerializeField]

    //못쏠때
    private bool DonShot;
    private bool MoveTF;

    /// <summary>
    /// 레벨들
    /// </summary>
    public int DpmLV;
    public int BulletLV;
    public int MoveLV;
    public int BulletReLV;
    public int MoveReLV;
    public int HPLV;
    public int HPReLV;

    public TextMeshProUGUI[] Level;
    public TextMeshProUGUI Coin;
    public TextMeshProUGUI Score;

    private void Awake()
	{
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayerStart();
    }

    public void PlayerStart()
    {
        //기본 스텟
        MaxHP = 100;
        HP = MaxHP;
        HPrestore = 10;
        BulletLim = 50;
        BulletRe = 1.5f;
        maxMove = 4;
        MoveDealy = 4f;
        nowmove = 4;
        //레벨수치
        Scr_PlayerManager.instance.Damage = 10;
        DpmLV = 1;
        BulletLV = 1;
        MoveLV = 1;
        BulletReLV = 1;
        MoveReLV = 1;
        HPLV = 1;
        HPReLV = 1;


        StartCoroutine(GunRestore());
        StartCoroutine(MoveFire());
    }

	// Update is called once per frame
	void Update()
    {
        BulletFire();
        CamRot();
        ModeChange();
        UIbarch();
        Show();
        Change();
        SocreSee();
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            SceneManager.LoadScene("0_Intro");
            HP = MaxHP;
            PlayerStart();
        }
    }
    public void UIbarch()
    {
        hpbar.value = HP / MaxHP;
        BulletBar.value = nowBulletLim / BulletLim;
        MoveBar.value = nowmove / (float)maxMove;
        MoveText.text = nowmove + "/" + maxMove;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HPHeal();
        playerone(SceneManager.GetActiveScene().name);
        PortalSound.Play();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void BulletFire()
    {
        //조준점 위치
        if (Physics.Raycast(firePos.transform.position, firePos.transform.forward, out Ray, 1000f))
        {
            CrossHair.transform.position = Ray.point;
            if (Ray.collider.tag == "Shop")
            {
                Ray.collider.gameObject.GetComponent<Scr_Shop>().Setting();
            }
        }
        //과부화
        if (DonShot)
        {
            OverLoadEffect.SetActive(true);
            return;
        }
        //과부화 해제
        else 
        {
            OverLoadEffect.SetActive(false);
        }

        //연사모드
        float bDown = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch);
        if (bDown != 0 && !Shot && Mode == GunMode.shot)
        {
            
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                Fire();
            }
        }
        else if (bDown != 0 && !Shot && Mode == GunMode.auto)
        {
            Fire();
            Shot = true;
            StartCoroutine(AutoFire());
        }
    }

    //점수 보여주는 기능
    void SocreSee()
    {
        Score.text = Scr_PlayerManager.instance.nowScore.ToString();
        Coin.text = Scr_PlayerManager.instance.Coin.ToString();
        bool Show = false;
        var Time = 0f;
        float bDown = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch);
        if (bDown != 0)
        {
            ScoreUI.SetActive(true);
        }
        else
        {
            ScoreUI.SetActive(false);
        }
    }
    //카메라 회전
    void CamRot()
    {

        Vector2 LeftStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (LeftStick.x > 0)
        {
            VRCam.transform.Rotate(new Vector3(0,1,0) * 2);
        }
        else if(LeftStick.x <0)
		{
            VRCam.transform.Rotate(new Vector3(0, 1, 0) * -1 * 2);
        }
    }

    //단발,연사모드
    public void ModeChange()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            if (Mode == GunMode.shot)
            {
                Mode = GunMode.auto;
                NowMode.sprite = Spr_Mode[1];
            }
            else
            {
                Mode = GunMode.shot;
                NowMode.sprite = Spr_Mode[0];
            }
        }
    }

    //이동
    public void Move()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !MoveTF)
        {
            if (nowmove <= 0)
            {
                return;
            }
            MoveCreate();
        }
        GameObject move;
        if(GameObject.FindWithTag("Move"))
        {
            move = GameObject.FindWithTag("Move");
            Dir = move.transform.localRotation * Vector3.forward;
            RB.AddForce(Dir * PowerPos * -1f , ForceMode.Impulse);
            Destroy(move);
            MoveSound.Play();
        }
        
    }
    
    //이동횟수 회복
    public void MoveCreate()
    {
        MoveTF = true;
        nowmove--;
        Instantiate(MoveBullet, Power.position, Power.rotation);
        Instantiate(Muzzle, Power.position, Power.rotation);
        StartCoroutine(MoveTFch());
    }


    //내가 피해입는거
    public void Damage(float A_DMP)
    {
        HP -= A_DMP;
        Instantiate(HitEffect, HitOffset.position, HitOffset.rotation);
        if (HP <= 0)
        {
            SceneManager.LoadScene("0_Intro");
            PlayerStart();
        }
    }

    //총알발사
    //인터페이스로 옮길때 참고할것
    public void Fire()
    {
        if (BulletLim > nowBulletLim)
        {
            nowBulletLim += 1.5f;
            Instantiate(bullet, firePos.position, firePos.rotation);
            Instantiate(Muzzle, firePos.position, firePos.rotation);
            FireSound.Play();
        }
        else if (BulletLim <= nowBulletLim)
        {
            DonShot = true;
            StartCoroutine(GunOverload());
        }
        
    }
    //능력치UI보여주기
    public void Show()
	{

        if (OVRInput.Get(OVRInput.Button.Four))
        {
            LVUI.SetActive(true);
        }
        else
        {
            LVUI.SetActive(false);
        }
        
	}

    //총알횟수 제거하고 점프 늘려주는기능
    public void Change()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            if (BulletLim/2 > nowBulletLim )
            {
                if (nowmove == maxMove)
                {
                    return;
                }
                nowBulletLim += 25;
                nowmove++;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (nowmove <= 0)
            {
                return;
            }
            else
            {
                nowmove--;
                var CurrBull =  Mathf.Clamp(8f, 0, nowBulletLim);
                nowBulletLim -= CurrBull;
            }
        }
    }

    //방옮길때 회복해주는 기능
    public void HPHeal()
    {
        if (MaxHP > HP)
        {
            var CurrHP = Mathf.Clamp(HP + HPrestore, 0, MaxHP);
            HP = CurrHP;
        }
    }

    //자동으로 발사하는 코루틴
    IEnumerator AutoFire()
    {
        yield return new WaitForSeconds(BulletDelay);
        Shot = false;
        switch (Mode)
        {
            case GunMode.shot:
                BulletDelay = 0.3f;
                break;
            case GunMode.auto:
                BulletDelay = 0.05f; ;
                break;
            default:
                BulletDelay = 0.5f;
                break;
        }
        
    }

    //점프횟수 회복
    IEnumerator MoveFire()
    {

        while (true)
        {
            if (maxMove > nowmove)
            {
                nowmove++;
            }
            yield return new WaitForSeconds(MoveDealy);
        }
       

    }

    IEnumerator MoveTFch()
    {
        yield return new WaitForSeconds(0.5f);
        MoveTF = false;
    }

    //총알 회복
    IEnumerator GunRestore()
    {
        while (true)
        {
            
            if (nowBulletLim > 0)
            {
                nowBulletLim -= BulletRe;
                nowBulletLim = Mathf.Lerp(0, nowBulletLim, 1f);
                nowBulletLim = Mathf.Clamp(nowBulletLim, 0, BulletLim);

            }
            yield return new WaitForSeconds(1f);
        }
            
        
        
    }

    //과부화
    IEnumerator GunOverload()
    {
        yield return new WaitForSeconds(5f);
        DonShot = false;
    }

    //공격력 업글
    public void DPMUp()
    {
        if (Scr_PlayerManager.instance.Coin > 0)
        {
            if (DpmLV == 6)
            {
                return;
            }
            DpmLV++;
            Level[0].text = DpmLV.ToString();
            Scr_PlayerManager.instance.Coin--;
            switch (DpmLV)
            {
                case 1:
                    Scr_PlayerManager.instance.Damage = 10;
                    break;
                case 2:
                    Scr_PlayerManager.instance.Damage = 20;
                    break;
                case 3:
                    Scr_PlayerManager.instance.Damage = 30;
                    break;
                case 4:
                    Scr_PlayerManager.instance.Damage = 40;
                    break;
                case 5:
                    Scr_PlayerManager.instance.Damage = 50;
                    break;
                case 6:
                    Scr_PlayerManager.instance.Damage = 70;
                    break;
                default:
                    break;

            }
        }
        
    }

    public void BulletUp()
    {
        if (Scr_PlayerManager.instance.Coin > 0)
        {
            if (BulletLV == 6)
            {
                return;
            }
            BulletLV++;
            Level[2].text = BulletLV.ToString();
            Scr_PlayerManager.instance.Coin--;
            switch (BulletLV)
            {
                case 1:
                    BulletLim = 50;
                    break;
                case 2:
                    BulletLim = 60;
                    break;
                case 3:
                    BulletLim = 70;
                    break;
                case 4:
                    BulletLim = 80;
                    break;
                case 5:
                    BulletLim = 90;
                    break;
                case 6:
                    BulletLim = 100;
                    break;
                default:
                    break;

            }
        }
    }

    public void MoveUp()
    {
        if (Scr_PlayerManager.instance.Coin > 0)
        {
            if (MoveLV == 6)
            {
                return;
            }
            MoveLV++;
            Level[6].text = MoveLV.ToString();
            Scr_PlayerManager.instance.Coin--;
            switch (MoveLV)
            {
                case 1:
                    maxMove = 4;
                    break;
                case 2:
                    maxMove = 5;
                    break;
                case 3:
                    maxMove = 6;
                    break;
                case 4:
                    maxMove = 7;
                    break;
                case 5:
                    maxMove = 8;
                    break;
                case 6:
                    maxMove = 10;
                    break;
                default:
                    break;

            }
        }
    }

    public void BulletReUp()
    {
        if (Scr_PlayerManager.instance.Coin > 0)
        {
            if (BulletReLV == 6)
            {
                return;
            }
            BulletReLV++;
            Level[1].text = BulletReLV.ToString();
            Scr_PlayerManager.instance.Coin--;
            switch (BulletReLV)
            {
                case 1:
                    BulletRe = 1.5f;
                    break;
                case 2:
                    BulletRe = 2;
                    break;
                case 3:
                    BulletRe = 2.5f;
                    break;
                case 4:
                    BulletRe = 3;
                    break;
                case 5:
                    BulletRe = 3.5f;
                    break;
                case 6:
                    BulletRe = 4;
                    break;
                default:
                    break;

            }
        }
    }

    public void MoveReUp()
    {
        if (Scr_PlayerManager.instance.Coin > 0)
        {
            if (MoveReLV == 6)
            {
                return;
            }
            MoveReLV++;
            Level[5].text = MoveReLV.ToString();
            Scr_PlayerManager.instance.Coin--;
            switch (MoveReLV)
            {
                case 1:
                    MoveDealy = 4f;
                    break;
                case 2:
                    MoveDealy = 3.5f;
                    break;
                case 3:
                    MoveDealy = 3f;
                    break;
                case 4:
                    MoveDealy = 2.5f;
                    break;
                case 5:
                    MoveDealy = 2f;
                    break;
                case 6:
                    MoveDealy = 1.5f;
                    break;
                default:
                    break;

            }
        }
    }

    public void HPUp()
    {
        if (Scr_PlayerManager.instance.Coin > 0)
        {
            if (HPLV == 6)
            {
                return;
            }
            HPLV++;
            Level[4].text = HPLV.ToString();
            Scr_PlayerManager.instance.Coin--;
            switch (HPLV)
            {
                case 1:
                    MaxHP = 100;
                    break;
                case 2:
                    MaxHP = 125;
                    break;
                case 3:
                    MaxHP = 150;
                    break;
                case 4:
                    MaxHP = 175;
                    break;
                case 5:
                    MaxHP = 200;
                    break;
                case 6:
                    MaxHP = 250;
                    break;
                default:
                    break;

            }
        }
    }
    public void HPReUp()
    {
        if (Scr_PlayerManager.instance.Coin > 0)
        {
            if (HPReLV == 6)
            {
                return;
            }
            HPReLV++;
            Level[3].text = HPReLV.ToString();
            Scr_PlayerManager.instance.Coin--;
            switch (HPReLV)
            {
                case 1:
                    HPrestore = 5;
                    break;
                case 2:
                    HPrestore = 15;
                    break;
                case 3:
                    HPrestore = 20;
                    break;
                case 4:
                    HPrestore = 25;
                    break;
                case 5:
                    HPrestore = 30;
                    break;
                case 6:
                    HPrestore = 40;
                    break;
                default:
                    break;

            }
        }
    }

    //스탯초기화
    public void playerone(string scene)
    {
        switch (scene)
        {
            case "0_Intro":
                PlayerStart();
                break;
            default:
                break;

        }
    }
}
