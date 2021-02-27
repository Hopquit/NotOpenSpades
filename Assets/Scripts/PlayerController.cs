using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public Vector2 deltaInput;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    Vector2 moveInput;
    private bool jumpPressed;
    private float coyoteTime = 0;
    public float maxCoyoteTime = 0.1f;
    public Transform cameraTransform;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            coyoteTime = 0;
        }
        else
        {
            coyoteTime += Time.deltaTime;
        }

        groundedPlayer = controller.isGrounded || coyoteTime < maxCoyoteTime;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        if (jumpPressed && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        jumpPressed = false;
    }
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }
    public void OnJump()
    {
        jumpPressed = true;
    }
    public void OnLook(InputValue input)
    {
        deltaInput = input.Get<Vector2>();
    }
    public void OnFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit))
        {
            var collider = hit.collider;
            if (collider.CompareTag("target"))
            {
                collider.gameObject.SetActive(false);
            }
        }
    }
}
