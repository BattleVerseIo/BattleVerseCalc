using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    private EArenaType _arena;
    private Bot _bot_1;
    private Bot _bot_2;

    private List<Round> _roundsLog = new List<Round>();

    public Bot bot1 => _bot_1;
    public Bot bot2 => _bot_2;

    public void Init(EArenaType arena, Bot bot_1, Bot bot_2)
    {
        _arena = arena;
        _bot_1 = bot_1;
        _bot_2 = bot_2;
        
        _roundsLog.Clear();
    }

    public void Fight()
    {
        _roundsLog.Clear();
        for (int i = 0; i < Data.Base.roundCount; i++)
        {
            _roundsLog.Add(ExecuteRound(i));
        }
    }
    
    public Round ExecuteRound(int roundIndex)
    {
        Round round = new Round();

        round.Id = roundIndex;
        round.Bot_1.hpBefore = _bot_1.Hp;
        round.Bot_2.hpBefore = _bot_2.Hp;
        round.Bot_1.attack = _bot_1.Attack();
        round.Bot_2.attack = _bot_2.Attack();
        round.Bot_1.crit = 1;
        round.Bot_2.crit = 1;
        round.Bot_1.block = 1;
        round.Bot_2.block = 1;
        round.Bot_1.platform = 0;
        round.Bot_2.platform = 0;

        if (_bot_1.CritRound == roundIndex)
        {
            round.Bot_1.crit = Data.Base.critMultiplier;
            round.Bot_1.isCrit = true;
        }

        if (_bot_2.CritRound == roundIndex)
        {
            round.Bot_2.crit = Data.Base.critMultiplier;
            round.Bot_2.isCrit = true;
        }

        if (_bot_1.BlockRound == roundIndex)
        {
            round.Bot_1.block = _bot_1.Block();
            round.Bot_1.platform = _bot_1.Platform == (EPlatformType) _arena ? Data.Base.platformBonus : 0;
        }

        if (_bot_2.BlockRound == roundIndex)
        {
            round.Bot_2.block = _bot_2.Block();
            round.Bot_2.platform = _bot_2.Platform == (EPlatformType) _arena ? Data.Base.platformBonus : 0;
        }

        _bot_1.Damage(round.Bot_2.attack * round.Bot_2.crit * round.Bot_1.block - round.Bot_1.platform);
        _bot_2.Damage(round.Bot_1.attack * round.Bot_1.crit * round.Bot_2.block - round.Bot_2.platform);
        round.Bot_1.hpAfter = _bot_1.Hp;
        round.Bot_2.hpAfter = _bot_2.Hp;

        return round;
    }

    public List<Round> GetLog()
    {
        return new List<Round>(_roundsLog);
    }
}
