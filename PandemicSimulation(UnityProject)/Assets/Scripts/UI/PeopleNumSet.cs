using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleNumSet : MonoBehaviour
{
    //Sets number of people
    public int peopleNum
    {
        private set;
        get;
    } = 0;
    //Sets number of normal people
    public int normalNum 
    {
        private set;
        get;
    } = 0;
    //Sets number of infected people
    public int infectedNum
    {
        private set;
        get;
    } = 0;
    //Sets number of recovered people
    public int recoveredNum
    {
        private set;
        get;
    } = 0;

    //Sets number of masked normal people
    public int maskedNormalNum
    {
        private set;
        get;
    } = 0;
    //Sets number of masked infected people
    public int maskedInfectedNum
    {
        private set;
        get;
    } = 0;
    //Sets number of masked recovered people
    public int maskedRecoveredNum
    {
        private set;
        get;
    } = 0;


    //Gets number of people from input
    [SerializeField] private TMPro.TMP_InputField peopleInput;
    
    /// <summary>
    /// Gets all active sliders
    /// 0 -> Normal People
    /// 1 -> Infected People
    /// 2 -> Healed People
    /// </summary>
    [SerializeField] private Slider[] settingSliders;
    [SerializeField] private NumbertoText[] textChanger;

    private void Start()
    {
        settingSliders[0].onValueChanged.AddListener(delegate { NormalPeopleChanged(); });
        settingSliders[1].onValueChanged.AddListener(delegate { InfectedPeopleChanged(); });
        //settingSliders[2].onValueChanged.AddListener(delegate { RecoveredPeopleChanged(); });
    }

    private void Update()
    {
        if (peopleInput.text == String.Empty)
            peopleInput.text = "0";
        int people = 0;
        if (Int32.TryParse(peopleInput.text, out people))
        {
            if (people != peopleNum)
            {
                peopleNum = people;

                foreach (Slider slider in settingSliders)
                {
                    slider.maxValue = peopleNum;
                    slider.value = 0f;
                }

                SetMaskedNormal(0);
                SetMaskedInfected(0);
                SetMaskedRecovered(0);
            }
        }
    }
    public void NormalPeopleChanged()
    {
        int oldValue = normalNum;
        normalNum = Convert.ToInt32(settingSliders[0].value);
        settingSliders[0].value = normalNum;
        int changedVal = normalNum - oldValue;
        settingSliders[1].maxValue = settingSliders[1].maxValue - changedVal;
        textChanger[1].OnValueChanged();
        //settingSliders[2].maxValue = settingSliders[2].maxValue - changedVal;
        //textChanger[2].OnValueChanged();
    }
    public void InfectedPeopleChanged()
    {
        int oldValue = infectedNum;
        infectedNum = Convert.ToInt32(settingSliders[1].value);
        settingSliders[1].value = infectedNum;
        int changedVal = infectedNum - oldValue;
        settingSliders[0].maxValue = settingSliders[0].maxValue - changedVal;
        textChanger[0].OnValueChanged();
        //settingSliders[2].maxValue = settingSliders[2].maxValue - changedVal;
        //textChanger[2].OnValueChanged();
    }
    /*public void RecoveredPeopleChanged()
    {
        int oldValue = recoveredNum;
        recoveredNum = Convert.ToInt32(settingSliders[2].value);
        settingSliders[2].value = recoveredNum;
        int changedVal = recoveredNum - oldValue;
        settingSliders[0].maxValue = settingSliders[0].maxValue - changedVal;
        textChanger[0].OnValueChanged();
        settingSliders[1].maxValue = settingSliders[1].maxValue - changedVal;
        textChanger[1].OnValueChanged();
    }*/

    public void SetMaskedNormal(int val)
    {
        maskedNormalNum = val;
    }

    public void SetMaskedInfected(int val)
    {
        maskedInfectedNum = val;
    }

    public void SetMaskedRecovered(int val)
    {
        maskedRecoveredNum = val;
    }

    public void CheckPeople(TMPro.TMP_InputField peopleInput)
    {
        int allPeople = 0;
        if (Int32.TryParse(peopleInput.text, out allPeople))
        {
            if (allPeople > 1000)
            {
                allPeople = 1000;
            }
            else if (allPeople < 0)
            {
                allPeople = 0;
            }
        }
        else
        {
            allPeople = 0;
        }
        peopleInput.text = allPeople.ToString();
    }
}
