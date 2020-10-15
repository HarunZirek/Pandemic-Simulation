using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StreetLightController : MonoBehaviour
{
    Light[] spotLights;

    private bool lightsSet = false;

    private void Awake()
    {
        int children = GetComponentsInChildren<MeshRenderer>().Length;

        spotLights = new Light[children];

        for (int i = 0; i < children; i++)
        {
            spotLights[i] = transform.GetChild(i).GetComponentInChildren<Light>();
        }

    }

    private void Start()
    {
        if (GameManager.instantiate.isNight())
            lightsSet = false;
        else
            lightsSet = true;
    }

    private void LateUpdate()
    {
        if (!lightsSet && GameManager.instantiate.isNight())
        {
            lightsSet = true;
            foreach (Light light in spotLights)
            {
                light.enabled = true;
            }
        }
        else if (!GameManager.instantiate.isNight() && lightsSet)
        {
            lightsSet = false;
            foreach (Light light in spotLights)
            {
                light.enabled = false;
            }
        }
    }
}
