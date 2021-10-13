using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResistanceController : MonoBehaviour
{
    [SerializeField]
    private ResistaceItem _resistaceItemPrefab;
    
    [SerializeField]
    private RectTransform _tableRoot;

    private List<ResistaceItem> _items = new List<ResistaceItem>();
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        ResistaceItem item;
        
        string[] weaponNames = Enum.GetNames(typeof(EWeaponType));
        for (int i = 0; i < weaponNames.Length; i++)
        {
            item = Instantiate(_resistaceItemPrefab, _tableRoot);
            item.Init((EWeaponType)i);
            _items.Add(item);
        }
    }

    public void Save()
    {
        foreach (var resistaceItem in _items)
        {
            resistaceItem.Save();
        }
        
        CalcController.instance.SaveData();
    }
}
