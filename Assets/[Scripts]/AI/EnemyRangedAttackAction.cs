using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackAction : MonoBehaviour, Action
{
    [Header("Ranged Attack Properties")]
    [Range(1, 100)]
    public int fireDelay = 30;
    public Transform bulletSpawn;

    public GameObject bulletPrefab;
    public Transform bulletParent;

    private bool hadLOS;
    private PlayerDetections playerDetections;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Awake()
    {
        playerDetections = transform.parent.GetComponentInChildren<PlayerDetections>();
        soundManager = FindObjectOfType<SoundManager>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("[BULLETS]").transform;
    }

    // Update is called once per frame
    void Update()
    {
        hadLOS = playerDetections.LOS;
    }
    private void FixedUpdate()
    {
        if (hadLOS && Time.frameCount % fireDelay ==0)
        {
            Execute();
        }
    }
    public void Execute()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, bulletParent);
        bullet.GetComponent<BulletController>().Activate();
        soundManager.PlaySoundFX(Sound.BULLET, Channel.ENEMY_BULLET_FX);
    }

}
