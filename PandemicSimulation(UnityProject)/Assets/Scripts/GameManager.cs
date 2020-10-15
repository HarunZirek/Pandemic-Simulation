using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instantiate;

    private LightingManager lightingM;

    public People startPeople;

    public People simulationPeople;

    public bool isNight()
    {
        return lightingM.isNight;
    }

    public int currentDay
    {
        private set;
        get;
    } = 1;

    public int getPeopleNum()
    {
        return startPeople.peopleNum;
    }

    private void Awake()
    {
        if (instantiate)
            Destroy(this.gameObject);
        else
            instantiate = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1f;
        if (level == 1)
        {
            lightingM = GameObject.FindObjectOfType<LightingManager>();
            GameObject.FindObjectOfType<CityCreator>().CreateCity();
        }
    }

    public void StartSimulation()
    {
        SetAndStartSim();
    }

    public bool workTime()
    {
        return lightingM.workTime();
    }

    public bool parkTime()
    {
        return lightingM.parkTime();
    }

    public float speed()
    {
        return lightingM.speed;
    }

    public void NextDay()
    {
        currentDay++;
    }

    private void SetAndStartSim()
    {
        PeopleNumSet pNum = FindObjectOfType<PeopleNumSet>();

        startPeople.peopleNum = pNum.peopleNum;
        startPeople.normalPeopleNum = pNum.normalNum;
        startPeople.normalMaskedPeopleNum = pNum.maskedNormalNum;
        startPeople.infectedPeopleNum = pNum.infectedNum;
        startPeople.infectedMaskedPeopleNum = pNum.maskedInfectedNum;
        //startPeople.recoveredPeopleNum = pNum.recoveredNum;
        //startPeople.recoveredMaskedPeopleNum = pNum.maskedRecoveredNum;

        int counted = startPeople.normalPeopleNum + startPeople.infectedPeopleNum; //+ startPeople.recoveredPeopleNum;

        if (counted != startPeople.peopleNum || counted == 0)
        {
            Debug.LogError("Set all people!!");
            return;
        }

        simulationPeople.peopleNum = pNum.peopleNum;
        simulationPeople.normalPeopleNum = pNum.normalNum;// + pNum.maskedNormalNum;
        simulationPeople.infectedPeopleNum = pNum.infectedNum;// + pNum.maskedInfectedNum;
        //simulationPeople.recoveredPeopleNum = pNum.recoveredNum;// + pNum.maskedRecoveredNum;
        //Ill
        simulationPeople.infectedMaskedPeopleNum = 0;

        SceneManager.LoadScene(1);
    }
}

public struct People
{
    public int peopleNum;
    public int normalPeopleNum;
    public int normalMaskedPeopleNum;
    public int infectedPeopleNum;
    public int infectedMaskedPeopleNum;
    public int recoveredPeopleNum;
    public int recoveredMaskedPeopleNum;
    public int deadPeople;
    public int simulationDays;
}
