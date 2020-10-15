using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCreator : MonoBehaviour
{
    private HouseCreator houseCreator;
    [SerializeField]  private HumanPathController humanPathC;

    [SerializeField] private Transform[] hospitalPoses;
    [SerializeField] private Transform[] workplacePoses;
    [SerializeField] private Transform[] humanStartPoses;

    [SerializeField] private GameObject hospitalObj;
    [SerializeField] private GameObject workplaceObj;
    [SerializeField] private GameObject[] humans;

    [SerializeField] private Transform hospitalParent;
    [SerializeField] private Transform workpalceParent;

    [SerializeField] GameObject maskObj;

    private void Awake()
    {
        houseCreator = GetComponent<HouseCreator>();
    }

    public void CreateCity()
    {
        houseCreator.CreateHouses(houseCreator.housePoses.Length);

        for (int index = 0; index < hospitalPoses.Length; index++)
        {
            Instantiate(
                hospitalObj,
                hospitalPoses[index].position,
                hospitalPoses[index].rotation,
                hospitalParent);
        }
        for (int index = 0; index < workplacePoses.Length; index++)
        {
            Instantiate(
                workplaceObj,
                workplacePoses[index].position,
                workplacePoses[index].rotation,
                workpalceParent);
        }

        int infectedNum = GameManager.instantiate.startPeople.infectedPeopleNum;
        int infectedMaskedNum = GameManager.instantiate.startPeople.infectedMaskedPeopleNum;
        int normalMaskedNum = GameManager.instantiate.startPeople.normalMaskedPeopleNum;
        infectedNum -= infectedMaskedNum;

        for (int index = 0; index < GameManager.instantiate.getPeopleNum(); index++)
        {
            int randNum = Random.Range(0, humanStartPoses.Length);

            int randomNum = Random.Range(0, humans.Length);

            GameObject humanClone = Instantiate(humans[randomNum], humanStartPoses[randNum].position, humanStartPoses[randNum].rotation, null);
            Human hScript = humanClone.GetComponent<Human>();

            int yas = Random.Range(0, 100);

            hScript.SetHuman("isim", yas);
            if (normalMaskedNum != 0)
            {
                hScript.SetMaskOn(Instantiate(maskObj));
                normalMaskedNum--;
            }
            else if (infectedMaskedNum != 0)
            {
                hScript.SetInfected(true);
                hScript.SetMaskOn(Instantiate(maskObj));
                hScript.GetComponent<SphereCollider>().radius = 0.07f;
                infectedMaskedNum--;
            }
            else if(infectedNum != 0)
            {
                hScript.SetInfected(true);
                infectedNum--;
            }
           

            humanPathC.AddHuman(hScript);
        }
    }

}
