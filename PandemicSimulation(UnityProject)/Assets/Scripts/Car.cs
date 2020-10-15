using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool roadFinished;

    public Path carPath;

    public int carSpeed = 4;

    private int carIndex = 0;

    public void MoveCar()
    {
        if (roadFinished)
            return;

       
        else if(Vector3.Distance(carPath.pathPositions[carIndex].position, transform.position) < 0.01f)
        {
            carIndex++;
            if (carIndex == carPath.pathPositions.Length)
            {
                roadFinished = true;
                return;
            }
            else
            {
                transform.LookAt(carPath.pathPositions[carIndex], Vector3.up);
            }
        }

        transform.position = Vector3.MoveTowards(
                transform.position,
                carPath.pathPositions[carIndex].position,
                Time.deltaTime * GameManager.instantiate.speed());
    }
}
