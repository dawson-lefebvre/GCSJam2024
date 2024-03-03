using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    [SerializeField] GameObject Lamp;
    bool LampLit;

    // Start is called before the first frame update
    void Start()
    {
        LampLit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LampLit)
        {
            if (Lamp.GetComponent<LampControl>().holdingLightBugs)
            {
                GetComponentInChildren<FogControl>().DisableFog();
                LampLit = true;
            }
        }
        else
        {
            if (!Lamp.GetComponent<LampControl>().holdingLightBugs)
            {
                GetComponentInChildren<FogControl>().EnableFog();
                LampLit = false;
            }
        }

    }
}
