using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


public struct JobBattle : IJobParallelFor
{
    [ReadOnly]
    public int roundCount;
    [ReadOnly]
    public float baseHP;
    [ReadOnly]
    public float baseAttack;
    [ReadOnly]
    public float critMultiplier;
    [ReadOnly]
    public float platformBonus;
    
    [ReadOnly]
    public int arenaCount;
    [ReadOnly]
    public int botCountPow;
    
    [ReadOnly]
    public NativeArray<JobBotData> bots;
    [ReadOnly]
    public NativeArray<WeaponData> weapons;
    [ReadOnly]
    public NativeArray<ToyData> toys;
    [ReadOnly]
    public NativeArray<float> resistance;
    //[ReadOnly]
    //public NativeArray<JobVersus> versus;
    
    public NativeArray<int> winCount;
    public NativeArray<int> looseCount;
    public NativeArray<int> drawCount;
    
    public void Execute(int i)
    {
        int id_1 = i / botCountPow;
        int j = i % botCountPow;
        int id_2 = j / arenaCount;
        EArenaType arena = (EArenaType)(i % arenaCount);
        
        if (id_1 == id_2)
            return;

        Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint)i);
        
        
        JobBot bot1 = new JobBot();
        bot1.hp = baseHP;
        bot1.critRound = random.NextInt(0, roundCount);
        bot1.blockRound = random.NextInt(0, roundCount);
        
        JobBot bot2 = new JobBot();
        bot2.hp = baseHP;
        bot2.critRound = random.NextInt(0, roundCount);
        bot2.blockRound = random.NextInt(0, roundCount);

        float attack;
        for (int round = 0; round < roundCount; round++)
        {
            attack = baseAttack + weapons[(int) bots[id_1].weapon].Damage;
            if (bot1.critRound == round)
                attack *= critMultiplier;
            if (bot2.blockRound == round)
            {
                attack *= 1 - (resistance[(int) bots[id_1].weapon * weapons.Length + (int) bots[id_2].toy] + toys[(int) bots[id_2].toy].Defence) / 100;
                attack -= bots[id_2].platform == (EPlatformType) arena ? platformBonus : 0;
            }
            bot2.hp -= attack;
            
            attack = baseAttack + weapons[(int) bots[id_2].weapon].Damage;
            if (bot2.critRound == round)
                attack *= critMultiplier;
            if (bot1.blockRound == round)
            {
                attack *= 1 - (resistance[(int) bots[id_2].weapon * weapons.Length + (int) bots[id_1].toy] + toys[(int) bots[id_1].toy].Defence) / 100;
                attack -= bots[id_1].platform == (EPlatformType) arena ? platformBonus : 0;
            }
            bot1.hp -= attack;
        }

        if (Mathf.Approximately(bot1.hp, bot2.hp))
        {
            drawCount[id_1]++;
            drawCount[id_2]++;
        }
        else {
            if (bot1.hp > bot2.hp)
            {
                winCount[id_1]++;
                looseCount[id_2]++;
            }
            else
            {
                winCount[id_2]++;
                looseCount[id_1]++;
            }
        }
    }
}
