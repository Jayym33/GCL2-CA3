using System.Collections;
using UnityEngine;

public class IntroFreeze : MonoBehaviour
{
    // Reference your movement script here
    [SerializeField] private MonoBehaviour PlayerController;
    [SerializeField] private float freezeDuration = 3f;

    private void Start()
    {
        if (PlayerController != null)
        {
            // Disable movement immediately on load
            PlayerController.enabled = false;

            // Start the 3-second countdown
            StartCoroutine(WaitAndEnableMovement());
        }
    }

    private IEnumerator WaitAndEnableMovement()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(freezeDuration);

        // Re-enable the movement script
        PlayerController.enabled = true;
    }
}
