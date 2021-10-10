using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static BaseData Base;
    public static WeaponData[] Weapos;
    public static ToyData[] Toys;

    public static void InitDefault()
    {
        Base = new BaseData();
        
        Weapos = new WeaponData[10];
        Weapos[0].Type = EWeaponType.Lighting;
        Weapos[0].Damage = 3;
        Weapos[1].Type = EWeaponType.Claw;
        Weapos[1].Damage = 2.5f;
        Weapos[2].Type = EWeaponType.Club;
        Weapos[2].Damage = 2f;
        Weapos[3].Type = EWeaponType.DoubleSpike;
        Weapos[3].Damage = 1.5f;
        Weapos[4].Type = EWeaponType.Spike;
        Weapos[4].Damage = 1.2f;
        Weapos[5].Type = EWeaponType.Blade;
        Weapos[5].Damage = 1f;
        Weapos[6].Type = EWeaponType.Saw;
        Weapos[6].Damage = 1f;
        Weapos[7].Type = EWeaponType.Drill;
        Weapos[7].Damage = 1f;
        Weapos[8].Type = EWeaponType.Hammer;
        Weapos[8].Damage = 1f;
        Weapos[9].Type = EWeaponType.Axe;
        Weapos[9].Damage = 1f;

        Toys = new ToyData[11];
        Toys[0].Type = EToyType.Drum;
        Toys[0].Defence = 3;
        Toys[1].Type = EToyType.Windmill;
        Toys[1].Defence = 2.5f;
        Toys[2].Type = EToyType.Icecream;
        Toys[2].Defence = 2.5f;
        Toys[3].Type = EToyType.Ducky;
        Toys[3].Defence = 2;
        Toys[4].Type = EToyType.Bottle;
        Toys[4].Defence = 2;
        Toys[5].Type = EToyType.Shovel;
        Toys[5].Defence = 1.5f;
        Toys[6].Type = EToyType.Shaker;
        Toys[6].Defence = 1;
        Toys[7].Type = EToyType.Ball;
        Toys[7].Defence = 1;
        Toys[8].Type = EToyType.Lollipop;
        Toys[8].Defence = 1;
        Toys[9].Type = EToyType.Rocket;
        Toys[9].Defence = 1;
        Toys[10].Type = EToyType.Balloons;
        Toys[10].Defence = 1;
    }
}

public class BaseData
{
    public float baseHp = 100;
    public float baseAttack = 30;
    public float baseDeffence = 50;
    public float critMultiplier = 1.5f;
    public float platformBonus = 2;
    public int roundCount = 3;
}

[Serializable]
public enum EWeaponType 
{
    Lighting,
    Claw,
    Club,
    DoubleSpike,
    Spike,
    Blade,
    Saw,
    Drill,
    Hammer,
    Axe
}

[Serializable]
public struct WeaponData
{
    public EWeaponType Type;
    public float Damage;
}

[Serializable]
public enum EToyType
{
    Drum,
    Windmill,
    Icecream,
    Ducky,
    Bottle,
    Shovel,
    Shaker,
    Ball,
    Lollipop,
    Rocket,
    Balloons
}
[Serializable]
public struct ToyData
{
    public EToyType Type;
    public float Defence;
}

public enum EPlatformType
{
    Platform1,
    Platform2,
    Platform3
}

public enum EArenaType
{
    Round,
    Octogon,
    Square
}