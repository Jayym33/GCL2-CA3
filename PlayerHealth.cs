using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite fullHeart;
    public Sprite emptyHeart;

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
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Mario Died!");
    }
}
