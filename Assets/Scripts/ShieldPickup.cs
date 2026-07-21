using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if Mario touched the shield
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController script
            PlayerController player = other.GetComponent<PlayerController>();

            // Give Mario the shield
            player.StartShieldTimer();

            // Remove the shield pickup from the level
            Destroy(gameObject);
        }
    }
}
