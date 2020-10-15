using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPathController : MonoBehaviour
{
    [SerializeField] Car[] cars;

    [SerializeField] Path[] carPath;

    List<Car> carList = new List<Car>();

    float waitTime = 2f, waitingTime = 0f;

    private void Update()
    {
        waitingTime += Time.deltaTime;
        if (waitingTime > waitTime)
            waitingTime = 0f;

        if (carList.Count < 5)
        {
            if (waitingTime > 1.5f)
            {
                int randCar = Random.Range(0, cars.Length);
                int randCarPath = Random.Range(0, carPath.Length);

                GameObject nCar = Instantiate(cars[randCar].gameObject, carPath[randCarPath].pathPositions[0].transform.position, carPath[randCarPath].pathPositions[0].transform.rotation, null);

                nCar.GetComponent<Car>().carPath = carPath[randCarPath];
                nCar.GetComponent<Car>().carSpeed = Random.Range(4, 10);

                carList.Add(nCar.GetComponent<Car>());

                waitingTime = 0f;
            }
        }

        int finishedCar = -1;

        foreach (Car car in carList)
        {
            if (car.roadFinished)
            {
                finishedCar = carList.IndexOf(car);
            }
            else
            {
                car.MoveCar();
            }
        }


        if (finishedCar != -1)
        {
            Destroy(carList[finishedCar].gameObject);
            carList.RemoveAt(finishedCar);
        }
    }

}
