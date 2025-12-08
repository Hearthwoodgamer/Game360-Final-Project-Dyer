using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 10f;

    [Header("Combat")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private PlayerState currentState;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeState(new IdleState());
    }

    void Update()
    {
        // NEW: Only update if game is not paused
        if (GameManager.Instance != null && GameManager.Instance.IsPaused())
        {
            return; // Skip all input when paused
        }

        if (currentState != null)
        {
            currentState.UpdateState(this);
        }

        
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);

        EventManager.TriggerEvent("OnPlayerStateChanged", currentState.GetStateName());
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayer);
               
    }

    public void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {

            
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            AudioManager.Instance.PlayShootSound();
        }
    }

    public void Fire1()
    {
        if (bulletPrefab != null && firePoint1 != null)
        {


            Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
            AudioManager.Instance.PlayShootSound();
        }
    }

    public void Fire2()
    {
        if (bulletPrefab != null && firePoint2 != null)
        {


            Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            AudioManager.Instance.PlayShootSound();
        }
    }

    public void Fire3()
    {
        if (bulletPrefab != null && firePoint3 != null)
        {


            Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
            AudioManager.Instance.PlayShootSound();
        }
    }


    public void TakeDamage()
    {
        GameManager.Instance.PlayerDied();
        GameManager.Instance.GameOver();
        //Respawn();
    }

    void Respawn()
    {
        transform.position = GameManager.Instance.spawnPoint;
        ChangeState(new IdleState());
    }

    public string GetCurrentStateName()
    {
        return currentState != null ? currentState.GetStateName() : "None";
    }
}