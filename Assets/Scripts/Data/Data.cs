using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public static class Data
{
    public static BaseData Base;
    public static WeaponData[] Weapons;
    public static ToyData[] Toys;
    public static ResistanceData[] Resistance;

    public static void InitDefault()
    {
        Base = new BaseData();

        string[] weaponNames = Enum.GetNames(typeof(EWeaponType));
        Weapons = new WeaponData[weaponNames.Length];
        Weapons[0].Type = EWeaponType.Lighting;
        Weapons[0].Damage = 3;
        Weapons[1].Type = EWeaponType.Claw;
        Weapons[1].Damage = 2.5f;
        Weapons[2].Type = EWeaponType.Club;
        Weapons[2].Damage = 2f;
        Weapons[3].Type = EWeaponType.DoubleSpike;
        Weapons[3].Damage = 1.5f;
        Weapons[4].Type = EWeaponType.Spike;
        Weapons[4].Damage = 1.2f;
        Weapons[5].Type = EWeaponType.Blade;
        Weapons[5].Damage = 1f;
        Weapons[6].Type = EWeaponType.Saw;
        Weapons[6].Damage = 1f;
        Weapons[7].Type = EWeaponType.Drill;
        Weapons[7].Damage = 1f;
        Weapons[8].Type = EWeaponType.Hammer;
        Weapons[8].Damage = 1f;
        Weapons[9].Type = EWeaponType.Axe;
        Weapons[9].Damage = 1f;

        string[] toyNames = Enum.GetNames(typeof(EToyType));
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
        
        Resistance = new ResistanceData[weaponNames.Length];
        Resistance[0] = new ResistanceData() {value = new float[] {5, 10, 15, 20, 25, 10, 40, 30, 15, 25, 50}};
        Resistance[1] = new ResistanceData() {value = new float[] {5, 40, 10, 50, 30, 10, 20, 10, 15, 40, 15}};
        Resistance[2] = new ResistanceData() {value = new float[] {10, 5, 20, 5, 35, 20, 25, 15, 50, 25, 45}};
        Resistance[3] = new ResistanceData() {value = new float[] {10, 25, 10, 10, 20, 25, 30, 50, 25, 30, 15}};
        Resistance[4] = new ResistanceData() {value = new float[] {15, 30, 30, 15, 10, 25, 50, 30, 10, 25, 10}};
        Resistance[5] = new ResistanceData() {value = new float[] {25, 15, 15, 30, 20, 35, 10, 10, 35, 10, 25}};
        Resistance[6] = new ResistanceData() {value = new float[] {40, 15, 35, 15, 50, 15, 15, 25, 10, 25, 10}};
        Resistance[7] = new ResistanceData() {value = new float[] {25, 35, 50, 35, 10, 25, 10, 10, 25, 10, 15}};
        Resistance[8] = new ResistanceData() {value = new float[] {40, 35, 15, 20, 20, 15, 15, 25, 10, 15, 10}};
        Resistance[9] = new ResistanceData() {value = new float[] {45, 15, 30, 30, 10, 50, 10, 15, 15, 10, 5}};
        
        /*
            Resistance = new float[,]
        {
            {5, 10, 15, 20, 25, 10, 40, 30, 15, 25, 50},
            {5, 40, 10, 50, 30, 10, 20, 10, 15, 40, 15},
            {10, 5, 20, 5, 35, 20, 25, 15, 50, 25, 45}, 
            {10, 25, 10, 10, 20, 25, 30, 50, 25, 30, 15}, 
            {15, 30, 30, 15, 10, 25, 50, 30, 10, 25, 10}, 
            {25, 15, 15, 30, 20, 35, 10, 10, 35, 10, 25}, 
            {40, 15, 35, 15, 50, 15, 15, 25, 10, 25, 10}, 
            {25, 35, 50, 35, 10, 25, 10, 10, 25, 10, 15}, 
            {40, 35, 15, 20, 20, 15, 15, 25, 10, 15, 10}, 
            {45, 15, 30, 30, 10, 50, 10, 15, 15, 10, 5} 
        };
         */
    }
}

public class BaseData
{
    public float baseHp = 100;
    public float baseAttack = 30;
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

[Serializable]
public struct ResistanceData
{
    public float[] value;
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