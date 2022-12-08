using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{
    public Sound Music;
    public GameObject onScreenControls;
    public GameObject miniMap;

    // Update is called once per frame
    void Awake()
    {
        onScreenControls = GameObject.Find("OnScreenControls");


        onScreenControls.SetActive(Application.isMobilePlatform);

        FindObjectOfType<SoundManager>().PlayMusic(Music);

        miniMap = GameObject.Find("MiniMap");
        if (miniMap)
        {
            miniMap.SetActive(false);
        }

        BulletManager.Instance().BuildBulletPool();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<HealthBarController>().TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.M) && miniMap)
        {
            miniMap.SetActive(!miniMap.activeInHierarchy);
        }
    }


}
