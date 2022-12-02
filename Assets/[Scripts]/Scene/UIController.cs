using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject MiniMap;

    private void Start()
    {
        MiniMap = GameObject.Find("MiniMap");
    }

    public void OnRestartButton_Pressed()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnYButton_Pressed()
    {
        MiniMap.SetActive(!MiniMap.activeInHierarchy);
    }
}
