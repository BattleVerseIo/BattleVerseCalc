using System.Collections.Generic;
using UnityEngine;

public class DataStorage
{
    private const string BASE = "base";
    private const string WEAPONS = "weapons";
    private const string TOYS = "toys";

    public void Load()
    {
        if (!PlayerPrefs.HasKey(BASE))
        {
            Data.InitDefault();
            return;
        }
        
        if (PlayerPrefs.HasKey(BASE))
            Data.Base = JsonUtility.FromJson<BaseData>(PlayerPrefs.GetString(BASE));
        if (PlayerPrefs.HasKey(WEAPONS))
            Data.Weapos = JsonHelper.FromJson<WeaponData>(PlayerPrefs.GetString(WEAPONS));
        if (PlayerPrefs.HasKey(TOYS))
            Data.Toys = JsonHelper.FromJson<ToyData>(PlayerPrefs.GetString(TOYS));
    }

    public void Save()
    {
        PlayerPrefs.SetString(BASE, JsonUtility.ToJson(Data.Base));
        PlayerPrefs.SetString(WEAPONS, JsonHelper.ToJson(Data.Weapos));
        PlayerPrefs.SetString(TOYS, JsonHelper.ToJson(Data.Toys));
        
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        Data.InitDefault();
        PlayerPrefs.Save();
    }
}
