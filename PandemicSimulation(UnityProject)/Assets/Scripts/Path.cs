using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] pathPositions;

    private void Awake()
    {
        pathPositions = new Transform[GetComponentsInChildren<Transform>().Length - 1];

        for (int i = 0; i < pathPositions.Length + 1; i++)
        {
            if (transform.GetHashCode() != GetComponentsInChildren<Transform>()[i].GetHashCode())
                pathPositions[i - 1] = GetComponentsInChildren<Transform>()[i];
        }
    }
}
