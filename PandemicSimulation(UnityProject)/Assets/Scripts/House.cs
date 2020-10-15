using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private int _maxPeople;
    public int maxPeople
    {
        get { return _maxPeople; }
    }

    public int currentPeople
    {
        get;
        private set;
    }
}
