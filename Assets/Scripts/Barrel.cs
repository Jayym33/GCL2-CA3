using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour
{
    public float speed = 3f;
    public int points = 100;

    private Transform[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        // Finds the GameObject with BarrelPath attached and copies the waypoints
        waypoints = FindFirstObjectByType<BarrelPath>().waypoints;
    }

    void Update()
    {
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mario's BODY touches the barrel -> Mario dies
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Barrel collided");
            SceneManager.LoadScene("MainLevel");
        }
    }
}