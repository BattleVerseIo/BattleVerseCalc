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
    
    public Bot(int id, EWeaponType weapon, EToyType toy, EPlatformType platform)
    {
        _id = id;
        _weapon = weapon;
        _toy = toy;
        _platform = platform;

        _currHp = Data.Base.baseHp;
        _critRound = Random.Range(0, Data.Base.roundCount);
        _blockRound = Random.Range(0, Data.Base.roundCount);
    }

    public void Damage(float value)
    {
        _currHp -= value;
    }

    public float Attack()
    {
        return Data.Base.baseAttack + Data.Weapos[(int) _weapon].Damage;
    }
    
    public float Block()
    {
        return 1 - (Data.Base.baseDeffence + Data.Toys[(int) _toy].Defence) / 100;
    }
    
}
