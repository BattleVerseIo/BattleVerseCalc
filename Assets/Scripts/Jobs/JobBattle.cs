using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public struct JobBattle : IJobParallelFor
{
    [ReadOnly]
    public int randomSeed;
    [ReadOnly]
    public int id_2; 
    [ReadOnly]
    public int arenaNum;
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
    
    [ReadOnly]
    public NativeArray<int> prevWinCount;
    [ReadOnly]
    public NativeArray<int> prevLooseCount;
    [ReadOnly]
    public NativeArray<int> prevDrawCount;
    [WriteOnly]
    public NativeArray<int> winCount;
    [WriteOnly]
    public NativeArray<int> looseCount;
    [WriteOnly]
    public NativeArray<int> drawCount;
    
    public void Execute(int i)
    {
        //int id_1 = i / botCountPow;
        //int j = i % botCountPow;
        //int id_2 = j / arenaCount;
        int id_1 = i;
        EArenaType arena = (EArenaType) arenaNum; //(i % arenaCount);
        
        //if (id_1 == id_2)
        //    return;

        Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint)randomSeed);
        
        
        JobBot bot1 = new JobBot();
        bot1.hp = baseHP;
        bot1.critRound = random.NextInt(0, roundCount);
        bot1.blockRound = random.NextInt(0, roundCount);
        
        JobBot bot2 = new JobBot();
        bot2.hp = baseHP;
        bot2.critRound = random.NextInt(0, roundCount);
        bot2.blockRound = random.NextInt(0, roundCount);

        float attack;
        float block;
        for (int round = 0; round < roundCount; round++)
        {
            attack = baseAttack + weapons[(int) bots[id_1].weapon].Damage;
            if (bot1.critRound == round)
                attack *= critMultiplier;
            if (bot2.blockRound == round)
            {
                block = 1 - (resistance[(int) bots[id_1].weapon * weapons.Length + (int) bots[id_2].toy] + toys[(int) bots[id_2].toy].Defence) / 100;
                if (block < 0)
                    block = 0;
                attack *= block;
                attack -= bots[id_2].platform == (EPlatformType) arena ? platformBonus : 0;
            }
            bot2.hp -= attack;
            
            attack = baseAttack + weapons[(int) bots[id_2].weapon].Damage;
            if (bot2.critRound == round)
                attack *= critMultiplier;
            if (bot1.blockRound == round)
            {
                block = 1 - (resistance[(int) bots[id_2].weapon * weapons.Length + (int) bots[id_1].toy] + toys[(int) bots[id_1].toy].Defence) / 100;
                if (block < 0)
                    block = 0;
                attack *= block;
                attack -= bots[id_1].platform == (EPlatformType) arena ? platformBonus : 0;
            }
            bot1.hp -= attack;
        }

        
        if (Mathf.Abs(bot1.hp - bot2.hp) < float.Epsilon)
        {
            winCount[i] = prevWinCount[i];
            looseCount[i] = prevLooseCount[i];
            drawCount[i] = prevDrawCount[i] + 1;
        }
        else
        {
            if (bot1.hp > bot2.hp)
            {
                winCount[i] = prevWinCount[i] + 1;
                looseCount[i] = prevLooseCount[i];
                drawCount[i] = prevDrawCount[i];
            }
            else
            {
                winCount[i] = prevWinCount[i];
                looseCount[i] = prevLooseCount[i] + 1;
                drawCount[i] = prevDrawCount[i];
            }
        }
    }
}
