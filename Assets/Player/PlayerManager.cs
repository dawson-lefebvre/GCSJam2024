using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject checkPoint;

    public int currentSize = 1; //currentSize of player
    CinemachineVirtualCamera cam;
    PlayerController controller;
    [SerializeField] TextMeshProUGUI gemCountText;
    int gemsCollected = 0;

    [SerializeField] Image fadeOut;
    bool fading = false;
    private void Start()
    {
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
        controller = GetComponent<PlayerController>();

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
        gemsCollected++;
        gemCountText.text = $"{gemsCollected}/3";
    }

    //Vine climbing
    public bool canClimb = false;

    //Health
    public int health = 20;

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
                cam.m_Lens.OrthographicSize += Time.deltaTime * 6;
            }

            if(gameObject.transform.localScale.x >= currentScale.x * 2 && cam.m_Lens.OrthographicSize >= currentCamSize * 2)
            {
                isGrowing = false;
                controller.maxSpeed *= 2;
                controller.jumpForce *= 1.2f;
            }
        }

        if (fading)
        {
            fadeOut.color = new Color(0, 0, 0, fadeOut.color.a + Time.deltaTime);
            if(fadeOut.color.a >= 1)
            {
                SceneManager.LoadScene("YouWin");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Vine")
        {
            canClimb = true;
            controller.rb.velocity = new Vector2(controller.rb.velocity.x, 0);
            Debug.Log("Can climb");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Vine")
        {
            canClimb = false;
            Debug.Log("Can't climb");
        }
    }

    public void HurtPlayer(int damage)
    {
        health -= damage; 
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        transform.position = checkPoint.transform.position;
        health = 20;
    }

    public void Fade()
    {
        fading = true;
        fadeOut.gameObject.SetActive(true);
    }
}
