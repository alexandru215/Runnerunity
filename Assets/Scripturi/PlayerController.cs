using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Miscare")]
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneSpeed = 5f;

    [Header("Saritura")]
    [SerializeField] private float jumpForce = 9f;
    private bool doJump = false;
    [SerializeField] private bool isGrounded = true;

    [Header("Sistem Invincibilitate")]
    public bool IsInvincible = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Keyboard.current != null)
        {
            bool aApasatSalt = Keyboard.current.spaceKey.wasPressedThisFrame || 
                               Keyboard.current.upArrowKey.wasPressedThisFrame || 
                               Keyboard.current.wKey.wasPressedThisFrame;

            if (aApasatSalt && isGrounded)
            {
                doJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = rb.position + transform.forward * forwardSpeed * Time.fixedDeltaTime;

        float horizontalInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                horizontalInput = -1f;
            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                horizontalInput = 1f;
            }
        }

        newPosition += transform.right * horizontalInput * laneSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        if (doJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            doJump = false;
        }
    }

    public void ActivateInvincibility()
    {
        StartCoroutine(InvincibilityRoutine());
    }

    private IEnumerator InvincibilityRoutine()
    {
        IsInvincible = true;
        Debug.Log("Scut Activ!");
        yield return new WaitForSeconds(5f);
        IsInvincible = false;
        Debug.Log("Scut Dezactivat!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        string numeObiect = collision.gameObject.name.ToLower();
        
        if (!numeObiect.Contains("obstac") && !numeObiect.Contains("barier"))
        {
            isGrounded = true;
        }
    }
}