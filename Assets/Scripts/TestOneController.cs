using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestOneController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _bot1_WeaponDropdown;
    [SerializeField]
    private TMP_Dropdown _bot1_ToyDropdown;
    [SerializeField]
    private TMP_Dropdown _bot1_PlatformDropdown;
    [SerializeField]
    private TMP_Text _bot1_Log;
    
    [SerializeField]
    private TMP_Dropdown _bot2_WeaponDropdown;
    [SerializeField]
    private TMP_Dropdown _bot2_ToyDropdown;
    [SerializeField]
    private TMP_Dropdown _bot2_PlatformDropdown;
    [SerializeField]
    private TMP_Text _bot2_Log;
    
    
    [SerializeField]
    private TMP_Dropdown _arenaDropdown;
    [SerializeField]
    private TMP_Text _winnerLabel;
    
    
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _bot1_WeaponDropdown.ClearOptions();
        _bot2_WeaponDropdown.ClearOptions();
        _bot1_ToyDropdown.ClearOptions();
        _bot2_ToyDropdown.ClearOptions();
        _bot1_PlatformDropdown.ClearOptions();
        _bot2_PlatformDropdown.ClearOptions();
        _arenaDropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (var weaponData in Data.Weapos)
        {
            options.Add($"{weaponData.Type}");
        }
        _bot1_WeaponDropdown.AddOptions(options);
        _bot2_WeaponDropdown.AddOptions(options);
        
        options.Clear();
        foreach (var toyData in Data.Toys)
        {
            options.Add($"{toyData.Type}");
        }
        _bot1_ToyDropdown.AddOptions(options);
        _bot2_ToyDropdown.AddOptions(options);
        
        options.Clear();
        options.AddRange(Enum.GetNames(typeof(EPlatformType)));
        _bot1_PlatformDropdown.AddOptions(options);
        _bot2_PlatformDropdown.AddOptions(options);
        
        options.Clear();
        options.AddRange(Enum.GetNames(typeof(EArenaType)));
        _arenaDropdown.AddOptions(options);
    }

    public void Fight()
    {
        Bot bot1 = new Bot(1, (EWeaponType) _bot1_WeaponDropdown.value, (EToyType) _bot1_ToyDropdown.value, (EPlatformType) _bot1_PlatformDropdown.value);
        Bot bot2 = new Bot(2, (EWeaponType) _bot2_WeaponDropdown.value, (EToyType) _bot2_ToyDropdown.value, (EPlatformType) _bot2_PlatformDropdown.value);

        EArenaType arena = (EArenaType) _arenaDropdown.value;
        
        Battle battle = new Battle();
        battle.Init(arena, bot1, bot2);
        battle.Fight();
        
        PrintLog(battle);
    }

    private void PrintLog(Battle battle)
    {
        if (battle.bot1.Hp == battle.bot2.Hp)
            _winnerLabel.text = "Draw";
        else
            _winnerLabel.text = battle.bot1.Hp > battle.bot2.Hp ? "Winner: Bot1" : "Winner: Bot2";
        
        var rounds = battle.GetLog();
        _bot1_Log.text = string.Empty;
        _bot2_Log.text = string.Empty;

        string damage;
        string damageAdditional;
        for (int i = 0; i < rounds.Count; i++)
        {
            _bot1_Log.text += rounds[i].GetBotLog(rounds[i].Bot_1, rounds[i].Bot_2);
            _bot2_Log.text += rounds[i].GetBotLog(rounds[i].Bot_2, rounds[i].Bot_1);
            /*
            _bot1_Log.text += $"Round {i+1}\n";
            _bot1_Log.text += $"HP: {rounds[i].Bot_1.hpBefore}\n";
            _bot1_Log.text += $"Attack: {rounds[i].Bot_1.attack} {(rounds[i].Bot_1.isCrit ? $"* {rounds[i].Bot_1.crit} Crit = {rounds[i].Bot_1.attack * rounds[i].Bot_1.crit}" : "")}\n";
            damage = $"Damage: {rounds[i].Bot_2.attack * rounds[i].Bot_2.crit}";
            damageAdditional = string.Empty;
            if (rounds[i].Bot_1.block < 1)
                damageAdditional += $" * {rounds[i].Bot_1.block} Block";
            if (rounds[i].Bot_1.platform > 0)
                damageAdditional += $" - {rounds[i].Bot_1.platform} Platform";
            if (damageAdditional != String.Empty)
                damageAdditional += $" = {rounds[i].Bot_2.attack * rounds[i].Bot_2.crit * rounds[i].Bot_1.block - rounds[i].Bot_1.platform}";
            _bot1_Log.text += $"{damage} {damageAdditional}\n";
            _bot1_Log.text += "-----------------------------------------------------\n";
            
            _bot2_Log.text += $"Round {i+1}\n";
            _bot2_Log.text += $"HP: {rounds[i].Bot_2.hpBefore}\n";
            _bot2_Log.text += $"Attack: {rounds[i].Bot_2.attack} {(rounds[i].Bot_2.isCrit ? $"* {rounds[i].Bot_2.crit} Crit = {rounds[i].Bot_2.attack * rounds[i].Bot_2.crit}" : "")}\n";
            damage = $"Damage: {rounds[i].Bot_1.attack * rounds[i].Bot_1.crit}";
            damageAdditional = string.Empty;
            if (rounds[i].Bot_2.block < 1)
                damageAdditional += $" * {rounds[i].Bot_2.block} Block";
            if (rounds[i].Bot_2.platform > 0)
                damageAdditional += $" - {rounds[i].Bot_2.platform} Platform";
            if (damageAdditional != String.Empty)
                damageAdditional += $" = {rounds[i].Bot_1.attack * rounds[i].Bot_1.crit * rounds[i].Bot_2.block - rounds[i].Bot_2.platform}";
            _bot2_Log.text += $"{damage} {damageAdditional}\n";
            //_bot2_Log.text += $"Damage: {rounds[i].Bot_1.attack * rounds[i].Bot_1.crit} {(rounds[i].Bot_2.isBlock ? $"* {rounds[i].Bot_2.block} Block {(rounds[i].Bot_2.platform > 0 ? $"- {rounds[i].Bot_2.platform} Platform" : "")} = {rounds[i].Bot_1.attack * rounds[i].Bot_1.crit * rounds[i].Bot_2.block - rounds[i].Bot_2.platform}" : "")}\n";
            _bot2_Log.text += "-----------------------------------------------------\n";
            */
        }
        
        _bot1_Log.text += $"HP: {rounds[rounds.Count - 1].Bot_1.hpAfter}\n";
        _bot2_Log.text += $"HP: {rounds[rounds.Count - 1].Bot_2.hpAfter}\n";
    }
}
