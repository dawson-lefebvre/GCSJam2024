using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    [SerializeField] GameObject Lamp;
    [SerializeField] GameObject particles;
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
                GetComponentInChildren<TheFog>().DisableFog();
                LampLit = true;
            }
        }
        else
        {
            if (!Lamp.GetComponent<LampControl>().holdingLightBugs)
            {
                GetComponentInChildren<TheFog>().EnableFog();
                LampLit = false;
                particles.GetComponent<ParticleSystem>().Stop();
            }
        }

    }
}
