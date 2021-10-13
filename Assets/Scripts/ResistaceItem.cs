using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResistaceItem : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputPrefab;

    private EWeaponType _weaponType;
    private List<TMP_InputField> _inputs = new List<TMP_InputField>();

    public void Init(EWeaponType weaponType)
    {
        _weaponType = weaponType;
        TMP_InputField input;

        string[] toyNames = Enum.GetNames(typeof(EToyType));
        for (int i = 0; i < toyNames.Length; i++)
        {
            input = Instantiate(_inputPrefab, transform);
            input.text = $"{Data.Resistance[(int)_weaponType].value[i]}";
            _inputs.Add(input);
        }
    }

    public void Save()
    {
        string[] toyNames = Enum.GetNames(typeof(EToyType));
        for (int i = 0; i < toyNames.Length; i++)
        {
            Data.Resistance[(int) _weaponType].value[i] = float.Parse(_inputs[i].text);
        }
    }
}
