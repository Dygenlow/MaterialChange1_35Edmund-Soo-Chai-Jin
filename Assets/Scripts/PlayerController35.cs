using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController35 : MonoBehaviour
{
    bool isGround;
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;
    float moveSpeed = 20.0f;
    float xLimit = 20.0f;
    float zLimit = 20.0f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMaterials;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();

        Physics.gravity *= gravityModifier;

        isGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontalInput);

        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);

            playerRdr.material.color = playerMaterials[2].color;
        }

        else if (transform.position.x > xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);

            playerRdr.material.color = playerMaterials[1].color;
        }

        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);

            playerRdr.material.color = playerMaterials[3].color;
        }

        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);

            playerRdr.material.color = playerMaterials[5].color;
        }

        Jump();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;

            playerRdr.material.color = playerMaterials[4].color;
        }
    }
    private void Jump()
    {
        if (isGround == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                isGround = false;

                playerRdr.material.color = playerMaterials[0].color;
            }
        }
    }
}
