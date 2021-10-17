using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBattleResult
{
    WinBot1,
    WinBot2,
    Draw
}

public struct JobBattleResult
{
    public EBattleResult result;
    public int bot1;
    public int bot2;
}
