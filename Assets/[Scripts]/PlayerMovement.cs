using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float horizontalForce;
    public float horizontalSpeed;
    public float verticalForce;
    public float airFactor;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;
        Move();
        Jump();
    }

    private void Move()
    {
        float X = Input.GetAxisRaw("Horizontal");
        if (X != 0.0f)
        {
            Flip(X);
            X = (X > 0.0f) ? 1.0f : -1.0f;

            rigidbody2D.AddForce(Vector2.right * X * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            var clampedXVelocity = Mathf.Clamp(rigidbody2D.velocity.x, -horizontalSpeed, horizontalSpeed);
            rigidbody2D.velocity = new Vector2(clampedXVelocity, rigidbody2D.velocity.y);
        }
    }

    private void Jump()
    {
        float Y = Input.GetAxis("Jump");

        if ((isGrounded) && (Y > 0.0f))
        {
            rigidbody2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    private void Flip(float x)
    {
        if (x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }
}
