using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestTwoController : MonoBehaviour
{
    [SerializeField]
    private Bots _table;
    [SerializeField]
    private TMP_InputField _bot1_IdInput;
    [SerializeField]
    private TMP_Text _bot1_WeaponLabel;
    [SerializeField]
    private TMP_Text _bot1_ToyLabel;
    [SerializeField]
    private TMP_Text _bot1_PlatformLabel;
    [SerializeField]
    private TMP_Text _bot1_Log;
    
    [SerializeField]
    private TMP_InputField _bot2_IdInput;
    [SerializeField]
    private TMP_Text _bot2_WeaponLabel;
    [SerializeField]
    private TMP_Text _bot2_ToyLabel;
    [SerializeField]
    private TMP_Text _bot2_PlatformLabel;
    [SerializeField]
    private TMP_Text _bot2_Log;
    
    
    [SerializeField]
    private TMP_Dropdown _arenaDropdown;
    [SerializeField]
    private TMP_Text _winnerLabel;

    private Bot _bot1;
    private Bot _bot2;
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _arenaDropdown.ClearOptions();

        List<string> options = new List<string>();
        options.Clear();
        options.AddRange(Enum.GetNames(typeof(EArenaType)));
        _arenaDropdown.AddOptions(options);

        _bot1_IdInput.text = "1";
        _bot2_IdInput.text = "2";
        //ChangeBot(1, 1);
        //ChangeBot(2, 2);
    }

    public void ChangeBot1(string id)
    {
        ChangeBot(1, int.Parse(id));
    }
    
    public void ChangeBot2(string id)
    {
        ChangeBot(2, int.Parse(id));
    }
    
    private void ChangeBot(int index, int id)
    {
        var botsData = _table.dataArray[id];

        EWeaponType.TryParse(botsData.Weapon, out EWeaponType weapon);
        EToyType.TryParse(botsData.Toy, out EToyType toy);
        EPlatformType.TryParse(botsData.Platform.Replace(" ", ""), out EPlatformType platform);
        Bot bot = new Bot(id, weapon, toy, platform);

        if (index == 1)
        {
            _bot1 = bot;
            _bot1_WeaponLabel.text = $"{_bot1.Weapon}";
            _bot1_ToyLabel.text = $"{_bot1.Toy}";
            _bot1_PlatformLabel.text = $"{_bot1.Platform}";
        }
        else
        {
            _bot2 = bot;
            _bot2_WeaponLabel.text = $"{_bot2.Weapon}";
            _bot2_ToyLabel.text = $"{_bot2.Toy}";
            _bot2_PlatformLabel.text = $"{_bot2.Platform}";
        }
    }

    public void Fight()
    {
        EArenaType arena = (EArenaType) _arenaDropdown.value;
        
        _bot1.Reset();
        _bot2.Reset();
        
        Battle battle = new Battle();
        battle.Init(arena, _bot1, _bot2);
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

        for (int i = 0; i < rounds.Count; i++)
        {
            _bot1_Log.text += rounds[i].GetBotLog(rounds[i].Bot_1, rounds[i].Bot_2);
            _bot2_Log.text += rounds[i].GetBotLog(rounds[i].Bot_2, rounds[i].Bot_1);
        }
        
        _bot1_Log.text += $"HP: {rounds[rounds.Count - 1].Bot_1.hpAfter}\n";
        _bot2_Log.text += $"HP: {rounds[rounds.Count - 1].Bot_2.hpAfter}\n";
    }
}
