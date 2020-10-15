using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskedPeople : MonoBehaviour
{
    private int sliderInfo = 0;

    private GameObject maskedMenu;

    [SerializeField] private GameObject background;

    [SerializeField] private Slider maskedSlider;

    [SerializeField] private NumbertoText maskedText;

    private void Start()
    {
        maskedMenu = this.gameObject;
        maskedSlider.value = 0;
        maskedSlider.maxValue = 0;

        maskedSlider.onValueChanged.AddListener(delegate { ChangeValue(); });
    }

    public void OpenMenu(int _sliderInfo)
    {
        sliderInfo = _sliderInfo;
        background.SetActive(true);
        LeanTween.scale(maskedMenu, Vector3.one, 0.4f);

        PeopleNumSet pNum = FindObjectOfType<PeopleNumSet>();
        if (sliderInfo == 0)
        {
            maskedSlider.maxValue = pNum.normalNum;
            maskedSlider.value = pNum.maskedNormalNum;
        }
        else if(sliderInfo == 1)
        {
            maskedSlider.maxValue = pNum.infectedNum;
            maskedSlider.value = pNum.maskedInfectedNum;
        }
        else if(sliderInfo == 2)
        {
            maskedSlider.maxValue = pNum.recoveredNum;
            maskedSlider.value = pNum.maskedRecoveredNum;
        }
    }

    public void CloseMenu()
    {
        if(sliderInfo == 0)
        {
            FindObjectOfType<PeopleNumSet>().SetMaskedNormal(Convert.ToInt32(maskedSlider.value));
        }
        else if(sliderInfo == 1)
        {
            FindObjectOfType<PeopleNumSet>().SetMaskedInfected(Convert.ToInt32(maskedSlider.value));
        }
        else if(sliderInfo == 2)
        {
            FindObjectOfType<PeopleNumSet>().SetMaskedRecovered(Convert.ToInt32(maskedSlider.value));
        }
        LeanTween.scale(maskedMenu, Vector3.zero, 0.4f).setOnComplete(DeactiveBackground);
    }
   
    private void DeactiveBackground()
    {
        maskedSlider.value = 0;
        maskedSlider.maxValue = 0;
        background.SetActive(false);
    }

    private void ChangeValue()
    {
        int val = Convert.ToInt32(maskedSlider.value);
        maskedSlider.value = val;
        maskedText.OnValueChanged();
    }
}
