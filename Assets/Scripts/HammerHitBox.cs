using UnityEngine;

public class HammerHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barrel"))
        {
            Debug.Log("Hammer hit barrel!");
            Destroy(other.gameObject);
        }
    }
}