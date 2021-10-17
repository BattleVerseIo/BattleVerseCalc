using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _baseHpInput;
    [SerializeField]
    private TMP_InputField _baseAttackInput;
    [SerializeField]
    private TMP_InputField _baseDeffenceInput;
    [SerializeField]
    private TMP_InputField _critMultiplierInput;
    [SerializeField]
    private TMP_InputField _platformBonusInput;
    [SerializeField]
    private TMP_InputField _roundCountInput;
    
    [SerializeField]
    private WeaponItem _weaponItemPrefab;
    [SerializeField]
    private RectTransform _weaponsRoot;

    [SerializeField]
    private ToyItem _toyItemPrefab;
    [SerializeField]
    private RectTransform _toysRoot;

    private List<WeaponItem> _weaponItems = new List<WeaponItem>();
    private List<ToyItem> _toyItems = new List<ToyItem>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _baseHpInput.text = $"{Data.Base.baseHp}";
        _baseAttackInput.text = $"{Data.Base.baseAttack}";
        _baseDeffenceInput.text = $"{Data.Base.baseDeffence}";
        _critMultiplierInput.text = $"{Data.Base.critMultiplier}";
        _platformBonusInput.text = $"{Data.Base.platformBonus}";
        _roundCountInput.text = $"{Data.Base.roundCount}";
        
        InitWeapons();
        InitToys();
    }
    
    private void InitWeapons()
    {
        WeaponItem weaponItem;
        foreach (var weaponData in Data.Weapos)
        {
            weaponItem = Instantiate(_weaponItemPrefab, _weaponsRoot);
            weaponItem.name = $"{weaponData.Type}";
            weaponItem.Init(weaponData);
            _weaponItems.Add(weaponItem);
        }
    }
    
    private void InitToys()
    {
        ToyItem toyItem;
        foreach (var toyData in Data.Toys)
        {
            toyItem = Instantiate(_toyItemPrefab, _toysRoot);
            toyItem.name = $"{toyData.Type}";
            toyItem.Init(toyData);
            _toyItems.Add(toyItem);
        }
    }
    
    public void Save()
    {
        Data.Base.baseHp = float.Parse(_baseHpInput.text, CultureInfo.InvariantCulture);
        Data.Base.baseAttack = float.Parse(_baseAttackInput.text, CultureInfo.InvariantCulture);
        Data.Base.baseDeffence = float.Parse(_baseDeffenceInput.text, CultureInfo.InvariantCulture);
        Data.Base.critMultiplier = float.Parse(_critMultiplierInput.text, CultureInfo.InvariantCulture);
        Data.Base.platformBonus = float.Parse(_platformBonusInput.text, CultureInfo.InvariantCulture);
        Data.Base.roundCount = int.Parse(_roundCountInput.text, CultureInfo.InvariantCulture);

        for (int i = 0; i < Data.Weapos.Length; i++)
        {
            Data.Weapos[i].Damage = _weaponItems[i].value;
        }
        
        for (int i = 0; i < Data.Toys.Length; i++)
        {
            Data.Toys[i].Defence = _toyItems[i].value;
        }
        
        CalcController.instance.SaveData();
    }

    public void Reset()
    {
        foreach (var item in _weaponItems)
        {
            Destroy(item.gameObject);
        }
        _weaponItems.Clear();
        
        foreach (var item in _toyItems)
        {
            Destroy(item.gameObject);
        }
        _toyItems.Clear();
        
        CalcController.instance.ResetData();
        CalcController.instance.SaveData();
        Init();
    }
}
