using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{
    public Sound Music;
    public GameObject onScreenControls;

    // Update is called once per frame
    void Awake()
    {
        onScreenControls = GameObject.Find("OnScreenControls");


        onScreenControls.SetActive(Application.isMobilePlatform);

        FindObjectOfType<SoundManager>().PlayMusic(Music);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<HealthBarController>().TakeDamage(20);
        }
    }
}
