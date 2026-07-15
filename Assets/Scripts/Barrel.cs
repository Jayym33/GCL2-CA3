using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour
{
    GameObject barrel;

    public float speed = 3f;

    private Transform[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        //finds the GameObject w BarrelPathscript attached & copies the waypoint
        waypoints = FindFirstObjectByType<BarrelPath>().waypoints;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        // Move the barrel towards the current waypoint
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].position,
            speed * Time.deltaTime
        );

        // Check if the barrel is very close to the waypoint
        if (Vector2.Distance(transform.position,waypoints[currentWaypoint].position) < 0.05f)
        {
            // Switch to the next waypoint
            currentWaypoint++;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(barrel);
            Debug.Log("Barrel collided");
            SceneManager.LoadScene("MainLevel");

        }
    }
}
