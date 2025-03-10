using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 움직임
    [Header("Movement")]
    public float moveSpeed;
    private float originalSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public float superJump;

    // 보기
    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDalta;
    public bool canLook = true;

    private Rigidbody rb;
    public Action inventory;
    private Animator animator;
    public bool isDead = false;

    private Vector3 platformVelocity;
    private Vector3 previousPlatformPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        // 커서 안보이게 하기
        Cursor.lockState = CursorLockMode.Locked;

        originalSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
        }
    }

    private void LateUpdate()
    {
        if (canLook && !isDead)
        {
            CameraLook();
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        if (transform.parent != null)
        {
            Vector3 platformMovement = transform.parent.position - previousPlatformPosition;
            dir += platformMovement / Time.deltaTime;
            previousPlatformPosition = transform.parent.position;
        }

        rb.velocity = dir;

        bool isMoving = curMovementInput != Vector2.zero;
        animator.SetBool("Moving", isMoving);
    }

    void CameraLook()
    {
        camCurXRot += mouseDalta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDalta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDalta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            SoundManager.Instance.PlaySFX("Jump");
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            animator.SetTrigger("Jumping");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SuperJump"))
        {
            SoundManager.Instance.PlaySFX("Jump");
            rb.AddForce(Vector3.up * superJump, ForceMode.Impulse);
            animator.SetTrigger("SuperJumping");
        }

        if (collision.gameObject.CompareTag("SuperSpeed"))
        {
            StartCoroutine(SuperSpeedBoost());
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
            platformVelocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
            previousPlatformPosition = collision.transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    IEnumerator SuperSpeedBoost()
    {
        animator.SetTrigger("SuperSpeeding");
        moveSpeed *= 2;
        yield return new WaitForSeconds(2f);
        moveSpeed = originalSpeed;
    }


    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f)+ (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+ (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f)+ (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f)+ (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnInvenTory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SoundManager.Instance.PlaySFX("Inventory");
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
