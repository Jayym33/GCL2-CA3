using UnityEngine;
using System.Collections;

public class DisappearingPlatform : MonoBehaviour
{
    // Reference to the player ( to assign player into, so it knows which is the player)
    public Transform player;

    // How close the player must be before the platform disappears
    public float triggerDistance = 3f;

    // How long the platform stays gone before it respawns 
    public float respawnTime = 3f;

    // Components of the platform
    private Collider2D platformCollider;
    private SpriteRenderer platformSprite;

    // The starting condition to make sure the platform does not disappear at the start
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
        // If platform has already disappeared,the script knows its triggered and do nothing
        if (isDisappeared) return;

        // Calculate distance between player and platform
        float distance = Vector2.Distance(player.position, transform.position);

        // If player is close enough to the platform
        if (distance < triggerDistance)
        {
            // Start disappearing process 
            StartCoroutine(DisappearAndRespawn());
        }
    }

    // Coroutine handles disappearing and reappearing of platform
    IEnumerator DisappearAndRespawn()
    {
        // Mark as disappeared so it doesn't trigger again
        isDisappeared = true;

        // Disable collision, causing the player to fall through
        platformCollider.enabled = false;

        // Disable sprite, making the sprite invisible/disappear
        platformSprite.enabled = false;

        // Wait for respawn time
        yield return new WaitForSeconds(respawnTime);

        // Enable collision again, so the player doesn't fall through
        platformCollider.enabled = true;

        // Enable sprite again, so the platform reappears
        platformSprite.enabled = true;

        // Reset state to starting condition
        isDisappeared = false;
    }
}
