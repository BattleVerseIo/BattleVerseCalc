using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct JobVersus
{
    public EArenaType arena;
    public int botID1;
    public int botID2;

    public JobVersus(EArenaType inArena, int id1, int id2)
    {
        arena = inArena;
        botID1 = id1;
        botID2 = id2;
    }
}


public struct FillVersusJob : IJobParallelFor
{
    [ReadOnly]
    public int botCount;
    [ReadOnly]
    public int botCountPow;
    [ReadOnly]
    public int arenaCount;

    public NativeArray<JobVersus> versus;


    public void Execute(int i)
    {
        int bot1 = i / botCountPow;
        int j = i % botCountPow;
        int bot2 = j / arenaCount;
        EArenaType arena = (EArenaType)(i % arenaCount);

        versus[i] = new JobVersus() {arena = arena, botID1 = bot1, botID2 = bot2};
    }
}