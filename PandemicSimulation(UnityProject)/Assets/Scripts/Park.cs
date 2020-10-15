using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    static int parkIndex = 0;
    static int humanSize = 20;

    public int maxHumanSize = 20;


    private void Start()
    {
        humanSize = maxHumanSize;
    }

    public bool isFull
    {
        get;
        private set;
    }

    public static bool AddHuman()
    {
        parkIndex++;

        if (parkIndex > humanSize)
        {
            parkIndex--;
            return false;
        }
        else
            return true;
    }

    public static void DecreaseHuman()
    {
        parkIndex--;
        if (parkIndex < 0)
            parkIndex = 0;
    }

}
