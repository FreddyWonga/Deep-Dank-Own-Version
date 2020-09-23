using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingTrade : MonoBehaviour
{
    public Health health;
    public int currentCost;
    public int updatedCost;
    public Player player;
    public GameObject itemPickup;
    public Text itemPickupText;

    void Start()
    {
        health = GetComponent<Health>();
        player = GetComponent<Player>();
        currentCost = 500;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            itemPickup.SetActive(true);
            itemTextChange(other.gameObject.name);
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
                if (health.health != health.MaxHealth)
                {
                    if(player.score >= currentCost)
                    {
                        health.health = health.MaxHealth;
                        generateCost();
                    }
                }
            }
        }
        else
        {
            itemPickup.SetActive(false);
        }
    }

    public void itemTextChange(string itemName)
    {
        itemPickupText.text = "E to heal -" + currentCost;
    }

    private void generateCost()
    {
        updatedCost = Random.Range(currentCost, (currentCost + 1000));
        currentCost = updatedCost;
    }
}
