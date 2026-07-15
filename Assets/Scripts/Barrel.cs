using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour
{
    GameObject barrel;

    public float speed = 3f;
    //public BarrelPath path;

    private Transform[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        waypoints = FindFirstObjectByType<BarrelPath>().waypoints;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position,
                             waypoints[currentWaypoint].position) < 0.05f)
        {
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
