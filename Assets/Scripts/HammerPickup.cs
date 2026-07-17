using UnityEngine;

public class HammerPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.hasHammer = true;
            player.StartHammerTimer();

            Destroy(gameObject);
        }
    }
}