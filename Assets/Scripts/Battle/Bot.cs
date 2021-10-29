using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot
{
    private int _id;
    private EWeaponType _weapon;
    private EToyType _toy;
    private EPlatformType _platform;

    private float _currHp;
    private int _critRound;
    private int _blockRound;

    public bool IsAlive => _currHp > 0;
    public float Hp => _currHp; 
    
    public EWeaponType Weapon => _weapon;
    public EToyType Toy => _toy;
    public EPlatformType Platform => _platform;

    public int CritRound => _critRound;
    public int BlockRound => _blockRound;

    private static int RandomSeed;
    
    public Bot(int id, EWeaponType weapon, EToyType toy, EPlatformType platform)
    {
        _id = id;
        _weapon = weapon;
        _toy = toy;
        _platform = platform;

        if (RandomSeed == default)
            RandomSeed = DateTime.Now.Millisecond;
        
        Reset();
    }

    public void Reset()
    {
        _currHp = Data.Base.baseHp;
        RandomSeed++;
        //var random = new System.Random(RandomSeed);
        //_critRound = random.Next(0, Data.Base.roundCount);
        //_blockRound = random.Next(0, Data.Base.roundCount)
        _critRound = UnityEngine.Random.Range(0, Data.Base.roundCount);
        _blockRound = UnityEngine.Random.Range(0, Data.Base.roundCount);
    }
    
    public void Damage(float value)
    {
        _currHp -= value;
    }

    public float Attack()
    {
        return Data.Base.baseAttack + Data.Weapons[(int) _weapon].Damage;
    }
    
    public float Block(EWeaponType strikeWeapon)
    {
        return 1 - (Data.Resistance[(int)strikeWeapon].value[(int)_toy] + Data.Toys[(int) _toy].Defence) / 100;
    }
    
}
