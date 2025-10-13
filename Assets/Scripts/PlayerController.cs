using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI speedText;   
    public TextMeshProUGUI weightText;  

    void Update()
    {

        UpdateUI();
        HandleMovement();

    }

    // Handles player movement
    private void HandleMovement()
    {
        horizontalInput = 0f;
        forwardInput = 0f;

        // Forward/backward input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            forwardInput = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            forwardInput = -1f;

        // Turning input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontalInput = 1f;

        // Move and rotate
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }

    // Updates Speed and Weight text on UI
    private void UpdateUI()
    {
        if (speedText != null)
            speedText.text = $"Speed: {speed:F2}";

        if (weightText != null)
        {
            float currentScale = model != null ? model.localScale.x : 1f;
            weightText.text = $"Weight: {50 + currentScale*5 :F2}";
        }
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
