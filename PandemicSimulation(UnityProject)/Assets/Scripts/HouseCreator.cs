using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] houses;

    public Transform[] housePoses;
    private bool[] avaliableHouses;

    [SerializeField] private Transform houseParent;

    private void Awake()
    {
        avaliableHouses = new bool[housePoses.Length];
        for(int houseIndex = 0; houseIndex < avaliableHouses.Length; houseIndex++)
        {
            avaliableHouses[houseIndex] = true;
        }
    }

    public void CreateHouses(int houseLimit)
    {
        for(int houseIndex = 0; houseIndex < houseLimit; houseIndex++)
        {
            //CreateHouse(peopleCount);
            CreateHouse();
        }
    }

    private void CreateHouse()
    {
        int randomPos = Random.Range(0, avaliableHouses.Length);
        if (avaliableHouses[randomPos])
        {
            GameObject cloneHouse = Instantiate(
                houses[Random.Range(0,houses.Length)], 
                housePoses[randomPos].position, 
                housePoses[randomPos].rotation, 
                houseParent);
            avaliableHouses[randomPos] = false;
            //cloneHouse.getcomponent<house>().setCurrentPeople(peopleCount);
        }
        else
        {
            CreateHouse();
            //CreateHouse(peopleCount);
        }
    }
}
