using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackAction : MonoBehaviour, Action
{
    [Header("Ranged Attack Properties")]
    [Range(1, 100)]
    public int fireDelay = 30;
    public Transform bulletSpawn;

    private bool hadLOS;
    private PlayerDetections playerDetections;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Awake()
    {
        playerDetections = transform.parent.GetComponentInChildren<PlayerDetections>();
        soundManager = FindObjectOfType<SoundManager>();
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
        var bullet = BulletManager.Instance().GetBullet(bulletSpawn.position);
        soundManager.PlaySoundFX(Sound.BULLET, Channel.ENEMY_BULLET_FX);
    }

}
