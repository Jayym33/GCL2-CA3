using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour
{
    GameObject barrel;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
