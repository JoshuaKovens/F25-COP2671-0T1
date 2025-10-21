using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; //Movement speed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from keyboard (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 movement = new Vector3(moveX, moveY, 0f);

        // Move the player
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}
