using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Human : MonoBehaviour
{
    public Path parkPath
    {
        get;
        private set;
    }
    public Path workPath
    {
        get;
        private set;
    }

    [SerializeField] Transform maskPos;

    private int workPosNum = 0;
    private int parkPosNum = 0;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void SetHuman(string hName, int hAge)
    {
        humanName = hName;
        humanAge = hAge;
    }

    public void AddWorkPath(Path wPath)
    {
        workPath = wPath;
    }

    public void AddParkPath(Path pPath)
    {
        parkPath = pPath;
    }

    public int humanAge
    {
        get;
        private set;
    }

    public bool isAlive
    {
        get;
        private set;
    } = true;

    public bool isRecovered
    {
        get;
        private set;
    } = false;

    public bool isInfected
    {
        get;
        private set;
    } = false;

    public bool isIll
    {
        get;
        private set;
    } = false;

    public bool hasMask
    {
        get;
        private set;
    } = false;

    public bool isWorking
    {
        get;
        private set;
    } = false;

    public bool isGoingHouse
    {
        get;
        private set;
    } = false;

    public bool isInHouse
    {
        get;
        private set;
    } = true;

    public bool isInPark
    {
        get;
        private set;
    } = false;

    public int infectedDays = 0;

    public string humanName
    {
        get;
        private set;
    }

    public void SetMaskOn(GameObject gObj)
    {
        hasMask = true;
        Vector3 gObjScale = gObj.transform.localScale;
        gObj.transform.parent = this.transform.GetChild(0);
        gObj.transform.position = maskPos.position;
        gObj.transform.rotation = maskPos.rotation;
        gObj.transform.localScale = gObjScale;
    }

    public void SetInfected(bool val)
    {
        isInfected = val;
    }

    public void SetIll(bool val)
    {
        isIll = val;
        if (!val)
            isRecovered = true;
    }

    public void KillHuman()
    {
        isInfected = false;
        isAlive = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetParkPath(Path pPath)
    {
        parkPath = pPath;
    }

    public void SetWorkPath(Path wPath)
    {
        workPath = wPath;
    }

    public void MoveWork()
    {
        if (!isAlive)
            return;
        if (isWorking)
            return;

        if (isInHouse)
        {
            int randNum = Random.Range(0, 40);

            if (randNum > 0)
            {
                return;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        isInHouse = false;
        isGoingHouse = false;

        if (Vector3.Distance(workPath.pathPositions[workPosNum].position, transform.position) < 0.01f)
        {
            workPosNum++;
            if (workPosNum == workPath.pathPositions.Length)
            {
                workPosNum--;
                isWorking = true;
                transform.GetChild(0).gameObject.SetActive(false);
                return;
            }
            else
            {
                transform.LookAt(workPath.pathPositions[workPosNum], Vector3.up);
            }
        }
        transform.position = Vector3.MoveTowards(
            transform.position,
            workPath.pathPositions[workPosNum].position,
            Time.deltaTime * GameManager.instantiate.speed());
    }

    public void MoveHouse()
    {
        if (!isAlive)
            return;
        if (isInHouse)
            return;

        if (isWorking)
        {
            int randNum = Random.Range(0, 40);

            if (randNum > 0)
            {
                return;
            }
            else
            {
                isWorking = false;

                transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        if(isInPark)
        {
            int randNum = Random.Range(0, 40);

            if (randNum > 0)
            {
                return;
            }
            else
            {
                isInPark = false;

                transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        isGoingHouse = true;

        if (workPath)
        {
            if (Vector3.Distance(workPath.pathPositions[workPosNum].position, transform.position) < 0.01f)
            {
                workPosNum--;
                if (workPosNum < 0)
                {
                    workPosNum = 0;
                    isInHouse = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    return;
                }
                else
                {
                    transform.LookAt(workPath.pathPositions[workPosNum], Vector3.up);
                }
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                workPath.pathPositions[workPosNum].position,
                Time.deltaTime * GameManager.instantiate.speed());
        }
        else
        {
            if(isInPark)
            {
                isInPark = false;
                Park.DecreaseHuman();
            }
            if (Vector3.Distance(parkPath.pathPositions[parkPosNum].position, transform.position) < 0.01f)
            {
                parkPosNum--;
                if (parkPosNum < 0)
                {
                    parkPosNum = 0;
                    isInHouse = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    return;
                }
                else
                {
                    transform.LookAt(parkPath.pathPositions[parkPosNum], Vector3.up);
                }
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                parkPath.pathPositions[parkPosNum].position,
                Time.deltaTime * GameManager.instantiate.speed());
        }
    }

    public void MovePark()
    {
        if (!isAlive)
            return;
        if (isInPark)
            return;

        if (isInHouse)
        {
            int randNum = Random.Range(0, 40);

            if (randNum > 0)
            {
                return;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        isInHouse = false;
        isGoingHouse = false;
        isInHouse = false;

        if (Vector3.Distance(parkPath.pathPositions[parkPosNum].position, transform.position) < 0.1f)
        {
            parkPosNum++;
            if (parkPosNum == parkPath.pathPositions.Length)
            {
                parkPosNum--;
                isInPark = true;
                transform.GetChild(0).gameObject.SetActive(false);
                if(!Park.AddHuman())
                {
                    MoveHouse();
                }
                return;
            }
            else
            {
                transform.LookAt(parkPath.pathPositions[workPosNum], Vector3.up);
            }
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            parkPath.pathPositions[parkPosNum].position,
            Time.deltaTime * GameManager.instantiate.speed());
    }


    private void OnTriggerEnter(Collider other)
    {
        Human oHuman = other.GetComponentInParent<Human>();

        if (!isAlive || !oHuman.isAlive)
            return;

        int infectChance = Random.Range(0, 2);

        if (infectChance == 1)
            return;

        if (oHuman.isIll && !isInfected && !isIll)
        {
            if (oHuman.hasMask)
            {
                if (hasMask)
                {
                    int randNum = Random.Range(0, 100);

                    if (randNum == 0)
                        isInfected = true;
                }
                else
                {
                    int randNum = Random.Range(0, 20);

                    if (randNum == 0)
                        isInfected = true;
                }
            }
            else
            {
                if (hasMask)
                {
                    int randNum = Random.Range(0, 100);

                    if (randNum < 70)
                        isInfected = true;
                }
                else
                {
                    int randNum = Random.Range(0, 10);

                    if (randNum < 9)
                        isInfected = true;
                }
            }
        }
    }
}
