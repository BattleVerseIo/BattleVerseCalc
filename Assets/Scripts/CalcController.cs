using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcController : MonoBehaviour
{
    private DataStorage _dataStorage;

    public static CalcController instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        _dataStorage = new DataStorage();
        _dataStorage.Load();

        instance = this;
    }

    public void SaveData()
    {
        _dataStorage.Save();
    }

    public void ResetData()
    {
        _dataStorage.Reset();
    }
}
