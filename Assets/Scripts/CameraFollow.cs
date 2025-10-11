using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          
    public Vector3 offset = new Vector3(0, 6, -8); // Distance from player
    public float followSpeed = 5f;    
    public float rotationSpeed = 5f;  

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate the target position based on player's rotation
        Vector3 targetPosition = player.TransformPoint(offset);

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate the camera to match the player's facing direction
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
