using System;
using Shooting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPlayerDeath;
    public event Action<bool> OnGunStateChange;

    [SerializeField] private GameObject marker;
    [SerializeField] private GameObject diggingMarker;
    [SerializeField] private float jumpPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer2;
    [SerializeField] private AmmoController ammoController;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject gunMarker;

    public bool IsOnGun { get; private set; }
    
    private bool _isGrounded;
    private bool _canGetOnGun;
    
    private void Update()
    {
        if (IsOnGun)
        {
            HandleGettingOffGun();
        }
        else
        {
            HandleGettingOnGun();
        }
        HandleMovement();
    }

    private void HandleGettingOnGun()
    {
        if (!_canGetOnGun)
        {
            return;
        }

        if (!ammoController.HasEnoughAmmo())
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            gunMarker.SetActive(false);
            ToggleGun(true);
        }
    }

    private void HandleGettingOffGun()
    {
        if (!ammoController.HasEnoughAmmo())
        {
            gunMarker.SetActive(false);
            ToggleGun(false);
            return;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(horizontalInput) > 0f)
        {
            ToggleGun(false);
        }
    }

    private void ToggleGun(bool state)
    {
        IsOnGun = state;
        spriteRenderer.enabled = !state;
        spriteRenderer2.enabled = !state;
        OnGunStateChange?.Invoke(state);
        marker.SetActive(!state);
        diggingMarker.SetActive(!state);
    }

    private void HandleMovement()
    {
        if (IsOnGun)
        {
            return;
        }
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 translation = horizontalInput * moveSpeed * Time.deltaTime * Vector2.right;
        
        transform.Translate(translation);
        
        anim.SetBool("IsMoving", Mathf.Abs(horizontalInput) > 0f);
        
        if (_isGrounded && verticalInput > 0f)
        {
            _isGrounded = false;
            rb.AddForce(Vector2.up * jumpPower);
        }
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Hole"))
        {
            Die();
        }
        else if (other.transform.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInGunRange(other.transform))
        {
            _canGetOnGun = true;
            gunMarker.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsInGunRange(other.transform))
        {
            _canGetOnGun = false;
            gunMarker.SetActive(false);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (IsOnGun)
        {
            return;
        }
        
        if (IsInGunRange(other.transform) && ammoController.HasEnoughAmmo())
        {
            _canGetOnGun = true;
            gunMarker.SetActive(true);
        }
    }

    private bool IsInGunRange(Component otherTransform)
    {
        return otherTransform.CompareTag("GroundGun") && ammoController.HasEnoughAmmo();
    }

}