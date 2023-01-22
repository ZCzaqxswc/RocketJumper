using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Stat : MonoBehaviour
{
    public int GunMax;
    public int GunMaxRe;
    public int MoveMax;
    public int MoveMaxRe;
    public int MaxHP;
    public int HPRe;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        ResetStat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetStat()
    {
        GunMax = 0;
        GunMaxRe = 0;
        MoveMax = 0;
        MoveMaxRe = 0;
        MaxHP = 0;
        HPRe = 0;
        Damage = 1;
    }

}
