using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;
    [SerializeField]
    private TMP_InputField _input;

    public float value => float.Parse(_input.text, CultureInfo.InvariantCulture);
    
    public void Init(WeaponData inWeaponData)
    {
        _label.text = $"{inWeaponData.Type}";
        _input.text = $"{inWeaponData.Damage}";
    }
}
