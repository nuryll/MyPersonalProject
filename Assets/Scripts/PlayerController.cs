using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Get movement input from arrow keys (or WASD)
        float moveX = Input.GetAxis("Horizontal");  
        float moveZ = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 movement = new Vector3(moveX, 0, moveZ);

        // If player is moving
        if (movement.magnitude > 0.1f)
        {
            // Normalize movement for consistent speed
            movement.Normalize();

            // Move player forward
            rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

            // Rotate player toward movement direction smoothly
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
