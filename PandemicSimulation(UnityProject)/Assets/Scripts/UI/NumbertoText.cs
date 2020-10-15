using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumbertoText : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text numText;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }


    public void OnValueChanged()
    {
        numText.text = ((int)slider.value).ToString();
    }
}
