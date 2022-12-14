using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;

    private Rigidbody playerRB;
    private Camera mainCamera;

    private Vector3 movementDirection;

    void Start()
    {
        mainCamera = Camera.main;
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void RotateToFaceVelocity()
    {
        if (playerRB.velocity == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(playerRB.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (viewportPosition.x > 1)
            newPosition.x = -newPosition.x + 0.1f;
        else if (viewportPosition.x < 0)
            newPosition.x = -newPosition.x - 0.1f;
        if (viewportPosition.y > 1)
            newPosition.y = -newPosition.y + 0.1f;
        else if (viewportPosition.y < 0)
            newPosition.y = -newPosition.y - 0.1f;


        transform.position = newPosition;
    }

    private void FixedUpdate()
    {
        PlayerForce();
    }

    private void PlayerForce()
    {
        if (movementDirection == Vector3.zero) return;
        playerRB.AddForce(movementDirection * forceMagnitude * Time.deltaTime, ForceMode.Force);
        playerRB.velocity = Vector3.ClampMagnitude(playerRB.velocity, maxVelocity);
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }
}
