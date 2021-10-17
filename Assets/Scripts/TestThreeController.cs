using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class TestThreeController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _battleCountInput;
    [SerializeField]
    private TMP_InputField _strongBotWinPercentInput;
    [SerializeField]
    private TMP_InputField _weakBotWinPercentInput;
    [SerializeField]
    private Image _progress;
    [SerializeField]
    private TMP_Text _progressLabel;
    [SerializeField]
    private TMP_Text _log;
    
    [SerializeField]
    private Bots _table;


    private int _battleCount;
    private int _totalBattleCount;
    private int _prepareBattleCount;
    private int _completeBattleCount;
    private float _strongBotWinPercent;
    private float _weakBotWinPercent;

    private int _botCount;
    private NativeArray<JobBotData> _bots;
    private NativeArray<WeaponData> _weapons;
    private NativeArray<ToyData> _toys;
    private NativeArray<float> _resistance;
    private JobBotStatistic[] _botStatistics;

    private NativeArray<JobVersus> _versus;
    
    void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        //_versus.Dispose();
        _bots.Dispose();
        _weapons.Dispose();
        _toys.Dispose();
        _resistance.Dispose();
    }

    private void Init()
    {
        
        _botCount = _table.dataArray.Length;
        _bots = new NativeArray<JobBotData>(_botCount, Allocator.Persistent);
        for (int i = 0; i < _bots.Length; i++)
        {
            var botsData = _table.dataArray[i];
            Enum.TryParse(botsData.Weapon, out EWeaponType weapon);
            Enum.TryParse(botsData.Toy, out EToyType toy);
            Enum.TryParse(botsData.Platform.Replace(" ", ""), out EPlatformType platform);
            _bots[i] = new JobBotData()
            {
                id = i,
                weapon = weapon,
                toy = toy,
                platform = platform
            };
        }
        
        _weapons = new NativeArray<WeaponData>(Data.Weapons.Length, Allocator.Persistent);
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i] = Data.Weapons[i];
        }
        
        _toys = new NativeArray<ToyData>(Data.Toys.Length, Allocator.Persistent);
        for (int i = 0; i < _toys.Length; i++)
        {
            _toys[i] = Data.Toys[i];
        }
        
        int weaponIndex;
        int toyIndex;
        _resistance = new NativeArray<float>(_weapons.Length * _toys.Length, Allocator.Persistent);
        for (int i = 0; i < _resistance.Length; i++)
        {
            weaponIndex = i % _weapons.Length;
            toyIndex = i / _weapons.Length;
            _resistance[i] = Data.Resistance[weaponIndex].value[toyIndex];
        }
        
        _totalBattleCount = _bots.Length * _bots.Length * Enum.GetNames(typeof(EArenaType)).Length;
        
        //PrepareVersus();
    }

    private void PrepareVersus()
    {
        int arenaCount = Enum.GetValues(typeof(EArenaType)).Length;
        NativeArray<JobVersus> versus = new NativeArray<JobVersus>(_totalBattleCount, Allocator.Persistent);
        
        var job = new FillVersusJob()
        {
            arenaCount = arenaCount,
            botCount = _botCount,
            botCountPow = _botCount * arenaCount,
            versus = versus
        };
        
        JobHandle jobHandle = job.Schedule(versus.Length, 64);
        
        jobHandle.Complete();
    }

    public void Fight()
    {
        int arenaCount = Enum.GetValues(typeof(EArenaType)).Length;
        _battleCount = int.Parse(_battleCountInput.text);
        _strongBotWinPercent = float.Parse(_strongBotWinPercentInput.text, CultureInfo.InvariantCulture);
        _weakBotWinPercent = float.Parse(_weakBotWinPercentInput.text, CultureInfo.InvariantCulture);
        
        _botStatistics = new JobBotStatistic[_table.dataArray.Length];

        NativeArray<int> win = new NativeArray<int>(_botCount, Allocator.Persistent);
        NativeArray<int> loose = new NativeArray<int>(_botCount, Allocator.Persistent);
        NativeArray<int> draw = new NativeArray<int>(_botCount, Allocator.Persistent);
        
        var job = new JobBattle()
        {
            roundCount = Data.Base.roundCount,
            baseHP = Data.Base.baseHp,
            baseAttack = Data.Base.baseAttack,
            critMultiplier = Data.Base.critMultiplier,
            platformBonus = Data.Base.platformBonus,
            
            arenaCount = arenaCount,
            botCountPow = _botCount * arenaCount,
            
            bots = _bots,
            weapons = _weapons,
            toys = _toys,
            resistance = _resistance,
            
            winCount = win,
            looseCount = loose,
            drawCount = draw
        };
        
        JobHandle jobHandle = job.Schedule(_totalBattleCount, 64);
        
        jobHandle.Complete();
        
        
        PrintLog(win, loose, draw);
        
        win.Dispose();
        loose.Dispose();
        draw.Dispose();
    }
    
    
    private void PrintLog(NativeArray<int> win, NativeArray<int> loose, NativeArray<int> draw)
    {
        string s = String.Empty;
        for (int i = 0; i < 20; i++)
        {
            s += $"id={i} win={win[i]} loose={loose[i]} draw={draw[i]}\n";
        }

        _log.text = s;
    }
}

