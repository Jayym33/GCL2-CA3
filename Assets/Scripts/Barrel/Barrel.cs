using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour
{
    public float speed = 3f;

    private Transform[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        // Finds the GameObject with BarrelPath attached and copies the waypoints
        waypoints = FindFirstObjectByType<BarrelPath>().waypoints;
    }

    void Update()
    {
        // Checks if there are waypoints available
        if (waypoints == null || waypoints.Length == 0)
            return;

        // Destroy barrel if it reaches the end of the path
        if (currentWaypoint >= waypoints.Length)
        {
            Destroy(gameObject);
            return;
        }

        // Move the barrel towards the current waypoint
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].position,
            speed * Time.deltaTime
        );

        // Check if the barrel is very close to the waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.05f)
        {
            currentWaypoint++;
            Debug.Log(currentWaypoint);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if Mario touched the barrel
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get Mario's PlayerController script
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            // If Mario has a shield
            if (player != null && player.hasShield)
            {
                Debug.Log("Shield blocked the barrel!");

                // Remove the shield
                player.RemoveShield();

                // Destroy this barrel
                Destroy(gameObject);

                return;
            }

            // Mario does not have a shield, so he takes damage
            Debug.Log("Barrel collided");

            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage();
            }

            Destroy(gameObject);

            player.Die();

        }
    }
}