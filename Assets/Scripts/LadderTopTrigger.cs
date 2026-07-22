using UnityEngine;

public class LadderTopTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player != null)
        {
            Animator anim = player.GetComponent<Animator>();

            anim.SetBool("NearEnd", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player != null)
        {
            Animator anim = player.GetComponent<Animator>();

            anim.SetBool("NearEnd", false);
        }
    }
}