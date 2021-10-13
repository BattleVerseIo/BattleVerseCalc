using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Round
{
    public int Id;
    public RoundBot Bot_1;
    public RoundBot Bot_2;

    public string GetBotLog(RoundBot bot1, RoundBot bot2)
    {
        string log = String.Empty;
        string damage = String.Empty;
        string damageAdditional = String.Empty;

        log += $"Round {Id + 1}\n";
        log += $"HP: {bot1.hpBefore}\n";
        log += $"Attack: {bot1.attack} {(bot1.isCrit ? $"* {bot1.crit} Crit = {bot1.attack * bot1.crit}" : "")}\n";
        damage = $"Damage: {bot2.attack * bot2.crit}";
        damageAdditional = string.Empty;
        if (bot1.block < 1) 
            damageAdditional += $" * {bot1.block} Block";
        if (bot1.platform > 0)
            damageAdditional += $" - {bot1.platform} Platform";
        if (damageAdditional != String.Empty)
            damageAdditional += $" = {bot2.attack * bot2.crit * bot1.block - bot1.platform}";
        
        log += $"{damage} {damageAdditional}\n";
        log += "-----------------------------------------------------\n";

        return log;
    }
}

public struct RoundBot
{
    public float hpBefore;
    public float hpAfter;
    public float attack;
    public float crit;
    public float block;
    public float platform;
    public bool isCrit;
}