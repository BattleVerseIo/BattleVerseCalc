using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ToyItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;
    [SerializeField]
    private TMP_InputField _input;

    public float value => float.Parse(_input.text, CultureInfo.InvariantCulture);
    
    public void Init(ToyData inWeaponData)
    {
        _label.text = $"{inWeaponData.Type}";
        _input.text = $"{inWeaponData.Defence}";
    }
}
