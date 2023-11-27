using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject pickUpParent;
    private int totalPickUps;
    public float jumpAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        totalPickUps = pickUpParent.transform.childCount;
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= totalPickUps)
        {
            winTextObject.SetActive(true);
        }
    }

    void Update()
    {
        // Listening to key presses in Update instead of FixedUpdate
        // This is due to how Unity implemented Input.GetKeyDown()
        // The methods returns True on the frame that the key is pressed
        // FixedUpdate() could therefore miss a key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // AddForce builds up momemtum by default
            // Use ForceMode.Impulse for immediate hit of force
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
