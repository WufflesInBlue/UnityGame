using UnityEngine;
using TMPro;

public class VelocityDisplay : MonoBehaviour
{
    public Rigidbody playerRigidbody; // Drag and drop the player's Rigidbody in the Inspector
    public TMP_Text velocityText;

    private void Start()
    {
        if (playerRigidbody == null)
        {
            Debug.LogError("Player Rigidbody not assigned in the Inspector.");
        }

        if (velocityText != null)
        {
            // Set placeholder text
            velocityText.text = "Speed: N/A";
        }
    }

    private void Update()
    {
        if (playerRigidbody != null && velocityText != null)
        {
            // Get the player's velocity
            Vector3 velocity = playerRigidbody.velocity;

            // Display the velocity on the TextMeshPro Text element
            velocityText.text = $"Speed: {velocity.magnitude:F2} m/s";
        }
    }
}
