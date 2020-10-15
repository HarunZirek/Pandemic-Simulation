using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPathController : MonoBehaviour
{
    public List<Human> humans
    {
        get;
        private set;
    }

    private Path[] workPaths, parkPaths;

    [SerializeField] Park park;

    private void Awake()
    {
        humans = new List<Human>();

        GameObject wPaths = transform.Find("WorkPaths").gameObject;
        GameObject pPaths = transform.Find("ParkPaths").gameObject;

        int childrenWorkPath = wPaths.GetComponentsInChildren<Path>().Length;
        int childrenParkPath = pPaths.GetComponentsInChildren<Path>().Length;

        workPaths = new Path[childrenWorkPath];

        for (int i = 0; i < childrenWorkPath; i++)
        {
            workPaths[i] = wPaths.transform.GetChild(i).GetComponent<Path>();
        }

        parkPaths = new Path[childrenParkPath];

        for (int i = 0; i < childrenParkPath; i++)
        {
            parkPaths[i] = pPaths.transform.GetChild(i).GetComponent<Path>();
        }
    }

    public void AddHuman(Human hMan)
    {
        int randWorkNum = Random.Range(0, workPaths.Length);
        int randParkNum = Random.Range(0, parkPaths.Length);

        int randNum = Random.Range(0, 5);

        if(randNum > 2)
        {
            hMan.AddWorkPath(workPaths[randWorkNum]);

            hMan.transform.position = workPaths[randWorkNum].pathPositions[0].position;

            if (!GameManager.instantiate.workTime())
            {
                hMan.MoveHouse();
            }
        }
        else
        {
            hMan.AddParkPath(parkPaths[randParkNum]);

            hMan.transform.position = parkPaths[randParkNum].pathPositions[0].position;

            if (!GameManager.instantiate.parkTime())
            {
                hMan.MoveHouse();
            }
        }

        humans.Add(hMan);
    }

    public void setInfection()
    {
        foreach (Human human in humans)
        {
            if (human)
            {
                if (human.isInfected)
                {
                    human.infectedDays++;

                    if (human.infectedDays > 14)
                    {
                        human.infectedDays = 0;
                        human.SetIll(true);
                        human.SetInfected(false);
                    }
                    else if (human.infectedDays < 14 && human.infectedDays > 4)
                    {
                        int randNum = Random.Range(0, 4);

                        if (randNum == 0)
                        {
                            human.infectedDays = 0;
                            human.SetIll(true);
                            human.SetInfected(false);
                        }
                    }
                }
                else if(human.isIll)
                {
                    human.infectedDays++;
                    if (human.infectedDays > 11)
                    {
                        human.infectedDays = 0;
                        human.SetIll(false);
                        human.SetInfected(false);
                    }
                    else if(human.infectedDays > 4 && human.infectedDays <= 11)
                    {
                        int randNum = Random.Range(0, 3);

                        if (randNum == 0)
                        {
                            human.infectedDays = 0;
                            human.SetIll(false);
                            human.SetInfected(false);
                        }
                        else if(randNum == 1)
                        {
                            human.infectedDays = 0;
                            human.SetIll(false);
                            human.SetInfected(false);
                            //go to hospital
                            //hospital.addHuman();
                        }
                        else if(randNum == 2)
                        {
                            int randNum2 = Random.Range(0, 4);

                            if (human.humanAge < 65)
                            {
                                if(randNum > 1)
                                {
                                    human.KillHuman();
                                }
                            }
                            else
                            {
                                if(randNum > 2)
                                {
                                    human.KillHuman();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void Update()
    {
        foreach (Human human in humans)
        {
            if (human && human.isAlive)
            {
                if (!human.workPath)
                {
                    if (human.parkPath)
                    {
                        if (GameManager.instantiate.parkTime())
                        {
                            if (!human.isGoingHouse)
                            {
                                if (!park.isFull)
                                {
                                    int randNum = Random.Range(0, 40);

                                    if (randNum > 3 && !park.isFull)
                                        human.MovePark();
                                    else
                                    {
                                        human.MoveHouse();
                                    }
                                }
                                else
                                {
                                        human.MoveHouse();
                                }
                            }
                            else
                            {
                                int randNum = Random.Range(0, 40);

                                if (randNum > 3)
                                    human.MoveHouse();
                            }
                        }
                        else
                        {
                            int randNum = Random.Range(0, 40);

                            if (randNum > 5)
                                human.MoveHouse();
                        }
                    }
                }
                else
                {
                    if (GameManager.instantiate.workTime())
                    {
                        human.MoveWork();
                    }
                    else
                    {
                        human.MoveHouse();
                    }
                }
            }
        }
    }
}
