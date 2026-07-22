using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    private PlayerController player;

    

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        heart1.sprite = health >= 1 ? fullHeart : emptyHeart;
        heart2.sprite = health >= 2 ? fullHeart : emptyHeart;
        heart3.sprite = health >= 3 ? fullHeart : emptyHeart;
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            LoseNoHealth();
            // Restore all hearts
            //health = 3;

            // Respawn Mario
            //player.RespawnToSP();
        }
        else
        {
            // Mario still has hearts left
            player.RespawnToSP();
        }
    }

    public void LoseNoHealth()
    {
        Debug.Log("No more health!"); //no health, lose screeen triggered
        SceneManager.LoadScene("LoseScreen");
    }
}