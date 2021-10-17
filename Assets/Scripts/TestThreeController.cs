using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class TestThreeController : MonoBehaviour
{
    private struct BotStatistic
    {
        public int WinCount;
        public int LooseCount;
        public int DrawCount;
    }
    
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
    private int _completeBattleCount;
    private float _strongBotWinPercent;
    private float _weakBotWinPercent;
    
    private Bot[] _bots;
    private BotStatistic[] _botStatistics;
    private Coroutine _coroutine;
    
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _bots = new Bot[_table.dataArray.Length];
        for (int i = 0; i < _table.dataArray.Length; i++)
        {
            var botsData = _table.dataArray[i];

            EWeaponType.TryParse(botsData.Weapon, out EWeaponType weapon);
            EToyType.TryParse(botsData.Toy, out EToyType toy);
            EPlatformType.TryParse(botsData.Platform.Replace(" ", ""), out EPlatformType platform);
            
            _bots[i] = new Bot(i, weapon, toy, platform);
        }
        
        _totalBattleCount = _bots.Length * _bots.Length * Enum.GetNames(typeof(EArenaType)).Length;
    }

    public void Fight()
    {
        _battleCount = int.Parse(_battleCountInput.text);
        _strongBotWinPercent = float.Parse(_strongBotWinPercentInput.text, CultureInfo.InvariantCulture);
        _weakBotWinPercent = float.Parse(_weakBotWinPercentInput.text, CultureInfo.InvariantCulture);
        
        _botStatistics = new BotStatistic[_table.dataArray.Length];

        if (_coroutine == null)
            _coroutine = StartCoroutine(DoFight());
    }

    private IEnumerator DoFight()
    {
        float lastTime = Time.realtimeSinceStartup;
        
        int botCount = _table.dataArray.Length;
        Parallel.For(0, botCount, i =>
        {
            Parallel.For(0, botCount, j =>
            {
                if (i != j)
                {
                    foreach (EArenaType arena in Enum.GetValues(typeof(EArenaType)))
                    {
                        _bots[i].Reset();
                        _bots[j].Reset();
                        Battle battle = new Battle();
                        battle.Init(arena, _bots[i], _bots[j]);
                        battle.Fight();

                        if (Mathf.Approximately(battle.bot1.Hp, battle.bot2.Hp))
                        {
                            _botStatistics[i].DrawCount++;
                            _botStatistics[j].DrawCount++;
                        }
                        else
                        {
                            if (battle.bot1.Hp > battle.bot2.Hp)
                            {
                                _botStatistics[i].WinCount++;
                                _botStatistics[j].LooseCount++;
                            }
                            else
                            {
                                _botStatistics[j].WinCount++;
                                _botStatistics[i].LooseCount++;
                            }
                        }

                        Interlocked.Increment(ref _completeBattleCount);
/*
                        if (Time.realtimeSinceStartup - lastTime > 0.01f)
                        {
                            
                            //yield return new WaitForEndOfFrame();
                        }
                        */
                    }
                }
            });
        });
        /*
        for (int i = 0; i < _table.dataArray.Length; i++)
        {
            for (int j = 0; j < _table.dataArray.Length; j++)
            {
                if (i == j)
                    continue;

                foreach (EArenaType arena in Enum.GetValues(typeof(EArenaType)))
                {
                    _bots[i].Reset();
                    _bots[j].Reset();
                    battle.Init(arena, _bots[i], _bots[j]);
                    battle.Fight();

                    if (Mathf.Approximately(battle.bot1.Hp, battle.bot2.Hp))
                    {
                        _botStatistics[i].DrawCount++;
                        _botStatistics[j].DrawCount++;
                    }
                    else
                    {
                        if (battle.bot1.Hp > battle.bot2.Hp)
                        {
                            _botStatistics[i].WinCount++;
                            _botStatistics[j].LooseCount++;
                        }
                        else
                        {
                            _botStatistics[j].WinCount++;
                            _botStatistics[i].LooseCount++;
                        }
                    }

                    completeBattleCount++;

                    if (Time.realtimeSinceStartup - lastTime > 0.01f)
                    {
                        _progress.fillAmount = completeBattleCount / totalBattleCount;
                        _progressLabel.text = $"{completeBattleCount} / {totalBattleCount}";
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }
        */
        
        PrintLog();
        _coroutine = null;
        yield break;
    }
    
    void Update()
    {
        _progress.fillAmount = _completeBattleCount / _totalBattleCount;
        _progressLabel.text = $"{_completeBattleCount} / {_totalBattleCount}";
    }
    
    private void PrintLog()
    {
        string s = String.Empty;
        for (int i = 0; i < _botStatistics.Length; i++)
        {
            s += $"id={i} win={_botStatistics[i].WinCount} loose={_botStatistics[i].LooseCount} draw={_botStatistics[i].DrawCount}\n";
        }

        _log.text = s;
    }
}
