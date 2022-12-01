using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Animation")]
    public Animator animator;
    public PlayerAnimationState playerAnimationState;

    [Header("Dust Trail Effect")]
    public ParticleSystem dustTrail;
    public Color dustTrailColour;

    [Header("HealthSystem")]
    public HealthBarController health;
    public LifeCounterController life;
    public DeathPlaneController deathPlane;

    [Header("Controls")]
    public Joystick leftStick;
    [Range(0.1f, 1.0f)]
    public float verticalThreshHold;

    private SoundManager soundManager;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        leftStick = (Application.isMobilePlatform) ? GameObject.Find("LeftStick").GetComponent<Joystick>() : null;
        health = FindObjectOfType<PlayerHealth>().GetComponent<HealthBarController>();
        life = FindObjectOfType<LifeCounterController>();
        deathPlane = FindObjectOfType<DeathPlaneController>();
        soundManager = FindObjectOfType<SoundManager>();
        dustTrail = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (health.value <= 0)
        {
            life.LoseLife();
            if (life.value < 0)
            {
                health.ResetHealthBar();
                deathPlane.Respawn(this.gameObject);
                soundManager.PlaySoundFX(Sound.DEATH, Channel.PLAYER_DEATH_FX);
            }
        }

        if (life.value <= 0)
        {
            SceneManager.LoadScene("End");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;
        Move();
        Jump();
        AirCheck();
    }

    private void Move()
    {
        float X = Input.GetAxisRaw("Horizontal") + ((Application.isMobilePlatform) ? leftStick.Horizontal : 0.0f);
        if (X != 0.0f)
        {
            Flip(X);
            X = (X > 0.0f) ? 1.0f : -1.0f;

            rigidbody2D.AddForce(Vector2.right * X * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            var clampedXVelocity = Mathf.Clamp(rigidbody2D.velocity.x, -horizontalSpeed, horizontalSpeed);
            rigidbody2D.velocity = new Vector2(clampedXVelocity, rigidbody2D.velocity.y);

            ChangeAnimation(PlayerAnimationState.RUN);

            if (isGrounded)
            {
                CreateDustTrail();
            }
        }
        if (isGrounded && X == 0)
        {
            ChangeAnimation(PlayerAnimationState.IDLE);
        }
    }

    private void Jump()
    {
        float Y = Input.GetAxis("Jump") + ((Application.isMobilePlatform) ? leftStick.Vertical : 0.0f);

        if ((isGrounded) && (Y > verticalThreshHold))
        {
            rigidbody2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(Sound.JUMP, Channel.PLAYER_SOUND_FX);
        }
    }

    private void AirCheck()
    {
        if (!isGrounded)
        {
            ChangeAnimation(PlayerAnimationState.JUMP);
        }
    }

    private void Flip(float x)
    {
        if (x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
    }

    private void CreateDustTrail()
    {
        dustTrail.GetComponent<Renderer>().material.SetColor("_Color", dustTrailColour);
        dustTrail.Play();
    }

    private void ChangeAnimation(PlayerAnimationState animationState)
    {
        playerAnimationState = animationState;
        animator.SetInteger("AnimationState", (int)playerAnimationState);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(20);
            soundManager.PlaySoundFX(Sound.HURT, Channel.PLAYER_HURT_FX);
        }
    }
}
