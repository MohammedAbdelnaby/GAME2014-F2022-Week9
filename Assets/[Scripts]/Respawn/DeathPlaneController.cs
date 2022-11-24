using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeathPlaneController : MonoBehaviour
{

    public Transform currentCheckPoint;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            var player = collision.gameObject.GetComponent<PlayerMovement>();
            player.life.LoseLife();
            player.health.ResetHealthBar();
            if (player.life.value > 0)
            {
                Respawn(collision.gameObject);
                FindObjectOfType<SoundManager>().PlaySoundFX(Sound.DEATH, Channel.PLAYER_DEATH_FX);
            }
        }
    }

    public void Respawn(GameObject go)
    {
        go.transform.position = currentCheckPoint.position;
    }
}
