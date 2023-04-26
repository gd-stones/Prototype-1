using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    private float turnSpeed = 25f;
    private float horizontalInput;
    private float verticalInput;

    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] float horsePower = 0;

    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // We move the vehicle forward
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);

        if (IsOnGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
            // We turn the vehicle
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f); // 3.6 for kph
            speedometerText.SetText("Speed: " + speed + "mph");
        }

        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (var wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
