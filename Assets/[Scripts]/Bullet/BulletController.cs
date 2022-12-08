using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Properties")]
    public Vector2 direction;
    public Rigidbody2D rigidbody2D;
    [Range(1.0f, 30.0f)]
    public float force;
    public Vector3 offSet;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Activate();
    }

    public void Activate()
    {
        Vector3 playerPostion = FindObjectOfType<PlayerMovement>().transform.position + offSet;
        direction = (playerPostion - transform.position).normalized;
        Rotate();
        Move();
        Invoke("DestroyYourself", 2.0f);
    }

    private void Rotate()
    {
        rigidbody2D.AddTorque(Random.Range(5.0f, 15.0f) * direction.x * -1.0f, ForceMode2D.Impulse);
    }

    private void Move()
    {
        rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void DestroyYourself()
    {
        if (gameObject.activeInHierarchy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Destroy(this.gameObject);
                break;
            case "Ground":
                Destroy(this.gameObject);
                break;
            case "Prop":
                Destroy(this.gameObject);
                break;
            case "Platform":
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
