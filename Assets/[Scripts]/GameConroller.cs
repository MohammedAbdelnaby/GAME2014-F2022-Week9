using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{

    public GameObject onScreenControls;

    // Update is called once per frame
    void Awake()
    {
        onScreenControls = GameObject.Find("OnScreenControls");

        onScreenControls.SetActive(Application.isMobilePlatform);
    }
}
