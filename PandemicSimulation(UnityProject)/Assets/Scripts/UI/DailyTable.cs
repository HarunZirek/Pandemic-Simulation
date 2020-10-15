using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyTable : MonoBehaviour
{
    [SerializeField] GameObject dailyTableObj;

    [SerializeField]
    TMPro.TMP_Text dayText, dailyIll, allIll,dailyInfected,
        allInfected, dailyRecovered, allRecovered,
        dailyDead, allDead;

    People currentDayPeople;

    private void Start()
    {
        currentDayPeople = new People();

        currentDayPeople.peopleNum = GameManager.instantiate.simulationPeople.peopleNum;
        currentDayPeople.normalPeopleNum = GameManager.instantiate.simulationPeople.normalPeopleNum;
        currentDayPeople.infectedPeopleNum = GameManager.instantiate.simulationPeople.infectedPeopleNum;
        currentDayPeople.infectedMaskedPeopleNum = 0;
        currentDayPeople.deadPeople = 0;

        GameManager.instantiate.simulationPeople.infectedMaskedPeopleNum = 0;
    }

    public void DayEnd()
    {
        HumanPathController hPath = GameObject.FindObjectOfType<HumanPathController>();
        hPath.setInfection();
        dayText.text = GameManager.instantiate.currentDay + " . GÜN SONUÇLARI";

        int _allDead = 0, _allIll = 0, _allRecovered = 0, _allInfected = 0;

        foreach (Human human in hPath.humans)
        {
            if(human)
            {
                if(!human.isAlive)
                {
                    _allDead++;
                }
                else
                {
                    if(human.isIll)
                    {
                        _allIll++;
                    }

                    if(human.isRecovered)
                    {
                        _allRecovered++;
                    }

                    if(human.isInfected)
                    {
                        _allInfected++;
                    }
                }
            }
        }
        int _todayDead = 0;
        int _todayRecovered = 0;
        int _todayIll = 0;
        int _todayInfected = 0;

        _todayDead = _allDead - currentDayPeople.deadPeople;
        _todayRecovered = _allRecovered - currentDayPeople.recoveredPeopleNum;
        _todayIll = _allIll - currentDayPeople.infectedMaskedPeopleNum;
        _todayInfected = _allInfected - currentDayPeople.infectedPeopleNum;

        if (_todayIll < 0)
            _todayIll = 0;
        if (_todayRecovered < 0)
            _todayRecovered = 0;
        if (_todayInfected < 0)
            _todayInfected = 0;

        GameManager.instantiate.simulationPeople.infectedPeopleNum += _todayInfected;
        GameManager.instantiate.simulationPeople.infectedMaskedPeopleNum += _todayIll;
        GameManager.instantiate.simulationPeople.recoveredPeopleNum += _todayRecovered;
        GameManager.instantiate.simulationPeople.deadPeople += _todayDead;

        currentDayPeople.deadPeople = _allDead;
        currentDayPeople.recoveredPeopleNum = _allRecovered;
        currentDayPeople.infectedPeopleNum = _allInfected;
        currentDayPeople.infectedMaskedPeopleNum = _allIll;

        dailyIll.text = "Günlük Vaka Sayısı : " + _todayIll;
        allIll.text = "Toplam Vaka Sayısı : " + GameManager.instantiate.simulationPeople.infectedMaskedPeopleNum;

        dailyInfected.text = "Günlük Taşıyıcı Sayısı : " + _todayInfected;
        allInfected.text = "Toplam Taşıyıcı Sayısı : " + GameManager.instantiate.simulationPeople.infectedPeopleNum;


        dailyRecovered.text = "Günlük İyileşen Sayısı : " + _todayRecovered;
        allRecovered.text = "Toplam İyileşen Sayısı : " + GameManager.instantiate.simulationPeople.recoveredPeopleNum;

        dailyDead.text = "Günlük Ölüm Sayısı : " + _todayDead;
        allDead.text = "Toplam Ölüm Sayısı : " + GameManager.instantiate.simulationPeople.deadPeople;
        dailyTableObj.SetActive(true);
    }

    public void SimulationEnd()
    {
        SceneManager.LoadScene(0);
    }

    public void NextDay()
    {
        dailyTableObj.SetActive(false);
        GameManager.instantiate.NextDay();
        GameObject.FindObjectOfType<LightingManager>().ResetTime();
        Time.timeScale = 1f;
    }
}
