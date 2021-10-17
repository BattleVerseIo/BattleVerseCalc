using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JobBotData
{
    public int id;
    public EWeaponType weapon;
    public EToyType toy;
    public EPlatformType platform; 
}

public struct JobBot
{
    public float hp;
    public int critRound;
    public int blockRound;
}

public struct JobBotStatistic
{
    public int WinCount;
    public int LooseCount;
    public int DrawCount;
}