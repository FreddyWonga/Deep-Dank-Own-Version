using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingTrade : MonoBehaviour
{
    public Health health;
    public int currentCost;
    public int updatedCost;
    public GameObject itemPickup;
    public Text itemPickupText;

    void Start()
    {
        health = GetComponent<Health>();
        currentCost = 500;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            health = other.GetComponent<Health>();
            itemPickup.SetActive(true);
            itemTextChange();
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
                if (health.health != health.MaxHealth)
                {
                    if(Score.scoreValue >= currentCost)
                    {
                        health.health = health.MaxHealth;
                        Score.scoreValue -= currentCost;
                        generateCost();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            itemPickup.SetActive(false);
        }
    }

    public void itemTextChange()
    {
        itemPickupText.text = "E to heal -" + currentCost;
    }

    private void generateCost()
    {
        updatedCost = Random.Range(currentCost, (currentCost + 1000));
        currentCost = updatedCost;
    }
}
