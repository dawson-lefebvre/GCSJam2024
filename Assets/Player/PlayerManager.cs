using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int currentSize = 1; //currentSize of player
    CinemachineVirtualCamera cam;

    private void Start()
    {
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    //Vars for growing "animation"
    Vector3 currentScale;
    float currentCamSize;
    bool isGrowing = false;
    public void Grow()
    {
        currentSize++;
        currentScale = gameObject.transform.localScale;
        currentCamSize = cam.m_Lens.OrthographicSize;
        isGrowing = true;
    }

    private void Update()
    {
        if (isGrowing)
        {
            //Add delta to scale and camera lens size until they reach double
            if(gameObject.transform.localScale.x < currentScale.x * 2)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + Time.deltaTime, gameObject.transform.localScale.x + Time.deltaTime, 1);
            }

            if(cam.m_Lens.OrthographicSize < currentCamSize * 2)
            {
                cam.m_Lens.OrthographicSize += Time.deltaTime * 2;
            }

            if(gameObject.transform.localScale.x >= currentScale.x * 2 && cam.m_Lens.OrthographicSize >= currentCamSize * 2)
            {
                isGrowing = false;
            }
        }
    }
}
