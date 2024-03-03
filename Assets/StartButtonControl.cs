using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonControl : MonoBehaviour
{

    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}
