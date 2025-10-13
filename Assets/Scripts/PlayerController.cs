using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 45f;
    private float horizontalInput;
    private float forwardInput;

    [Header("Growth Settings")]
    public Transform model;             // Reference to the child model object
    public float speedDecreaseAmount = 0.3f;
    public float growthAmount = 0.05f;
    public float minSpeed = 0.3f;
    public float maxScale = 2f;

    void Update()
    {
        // Reset input values each frame
        horizontalInput = 0f;
        forwardInput = 0f;

        // Forward & backward (W / UpArrow, S / DownArrow)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            forwardInput = 1f;   // move forward
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            forwardInput = -1f;  // move backward
        }

        // Turning left & right (A / LeftArrow, D / RightArrow)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f; // turn left
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;  // turn right
        }

        // Move forward/backward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Rotate left/right
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }

    // Called when the player collects a fast-food item
    public void GainWeight()
    {
        // Decrease movement speed, but not below the minimum value
        speed = Mathf.Max(minSpeed, speed - speedDecreaseAmount);

        // Increase the scale of the model (visual only)
        if (model != null)
        {
            Vector3 newScale = model.localScale + Vector3.one * growthAmount;
            float clampedScale = Mathf.Min(newScale.x, maxScale);
            model.localScale = Vector3.one * clampedScale;
        }
        else
        {
            Debug.LogWarning("Model reference is missing in PlayerController!");
        }

        Debug.Log($"Gained weight! Speed: {speed:F2}");
    }
}
