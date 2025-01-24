using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOffline : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float rotateSpeed = 100.0f;

    private Rigidbody rb;
    private Animator anim;
    private bool canJump = true;

    public MobileInputController joystickController;
    public bool isMobile = true;

    private Vector3 movement;

    private float touchSensitivity = 0.1f;
    private float rotationAmount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        isMobile = Application.isMobilePlatform;
    }

    void FixedUpdate()
    {
        if (isMobile && joystickController != null)
        {
            movement = new Vector3(joystickController.Horizontal, 0, joystickController.Vertical).normalized;
            HandleTouchRotation();
        }
        else
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            HandleMouseRotation();
        }

        if (movement != Vector3.zero || rotationAmount != 0)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotationAmount, 0));
        }

        rb.MovePosition(
            rb.position + transform.forward * movement.z * moveSpeed * Time.deltaTime +
            transform.right * movement.x * moveSpeed * Time.deltaTime
        );

        anim.SetFloat("BlendV", movement.z);
        anim.SetFloat("BlendH", movement.x);
    }

    private void HandleTouchRotation()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotationAmount = touch.deltaPosition.x * touchSensitivity;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                rotationAmount = 0;
            }
        }
    }

    private void HandleMouseRotation()
    {
        rotationAmount = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
    }

    private void Update()
    {
        if (isMobile && joystickController != null)
        {
            // Handle jump using joystick button on mobile
            /*if (joystickController.JumpButtonPressed && canJump)
            {
                HandleJump();
            }*/
        }
        else
        {
            // Handle jump using keyboard on desktop
            if (Input.GetButtonDown("Jump") && canJump)
            {
                HandleJump();
            }
        }
    }

    private void HandleJump()
    {
        canJump = false;
        rb.AddForce(Vector3.up * 1200 * Time.deltaTime, ForceMode.VelocityChange);
        StartCoroutine(JumpAgain());
    }

    IEnumerator JumpAgain()
    {
        yield return new WaitForSeconds(1);
        canJump = true;
    }
}
