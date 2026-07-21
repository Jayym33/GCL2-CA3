using UnityEngine;
using System.Collections;

public class DisappearingPlatform : MonoBehaviour
{
    // Reference to the player (we will assign this in Unity)
    public Transform player;

    // How close the player must be before platform disappears
    public float triggerDistance = 3f;

    // How long the platform stays gone before coming back
    public float respawnTime = 3f;

    // Components of the platform
    private Collider2D platformCollider;
    private SpriteRenderer platformSprite;

    // To make sure we don’t trigger multiple times
    private bool isDisappeared = false;

    void Start()
    {
        // Get the platform's collider
        platformCollider = GetComponent<Collider2D>();

        // Get the platform's sprite renderer (the visual part)
        platformSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // If already disappeared, do nothing
        if (isDisappeared) return;

        // Calculate distance between player and platform
        float distance = Vector2.Distance(player.position, transform.position);

        // If player is close enough
        if (distance < triggerDistance)
        {
            // Start disappearing process
            StartCoroutine(DisappearAndRespawn());
        }
    }

    // Coroutine handles disappearing and reappearing
    IEnumerator DisappearAndRespawn()
    {
        // Mark as disappeared so it doesn't trigger again
        isDisappeared = true;

        // Disable collision (player falls through)
        platformCollider.enabled = false;

        // Disable sprite (platform becomes invisible)
        platformSprite.enabled = false;

        // Wait for respawn time
        yield return new WaitForSeconds(respawnTime);

        // Enable collision again
        platformCollider.enabled = true;

        // Enable sprite again
        platformSprite.enabled = true;

        // Reset state
        isDisappeared = false;
    }
}
