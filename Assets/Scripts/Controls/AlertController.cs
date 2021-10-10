using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertController : MonoBehaviour
{
    [SerializeField] TMP_Text _countLabel;

    public void SetAlertCount(int inValue)
    {
        if (_countLabel)
            _countLabel.text = $"{inValue}";
    }
}
