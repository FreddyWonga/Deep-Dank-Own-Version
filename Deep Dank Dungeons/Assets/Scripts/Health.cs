using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int MaxHealth;

    public StatTracker StatTracker;

    public Image[] Hearts;
    public Sprite fullHearts;
    public Sprite emptyHeart;

    private void Start()
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            GameObject healthImage = GameObject.Find("Health (" + (i + 1) + ")");

            if (healthImage != null)
            {
                Hearts[i] = healthImage.GetComponent<Image>();
            }
        }
    }
    void Update()
    {
        health = StatTracker.Instance.health;
        MaxHealth = StatTracker.Instance.MaxHealth;

        if (health > MaxHealth)
        {
            health = MaxHealth;
        }

        //if (health <= 0)
        //{
        //    SceneManager.LoadScene("DeathScreen");
        //}

        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < health)
            {
                Hearts[i].sprite = fullHearts;
            }
            else
            {
                Hearts[i].sprite = emptyHeart;
            }


            if (i < MaxHealth)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
        }
    }

    public void DoDamage(int damageAmount)
    {

        StatTracker.Instance.health -= damageAmount;
    }

}
