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
    public StatTracker StatTracker;

    void Start()
    {
        health = GetComponent<Health>();
        currentCost = 500;
    }

    //If the player enters the area of the healing trade, enable the text displaying the price to heal
    //If the player presses "E", and they are below max health, and their score is above the price required to heal
    //Increase player health to max, subtract the cost from the players total score and generate a new cost to heal
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            itemPickup.SetActive(true);
            itemTextChange();
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
                if (StatTracker.Instance.health != StatTracker.Instance.MaxHealth)
                {
                    if(Score.scoreValue >= currentCost)
                    {
                        StatTracker.Instance.health = StatTracker.Instance.MaxHealth;
                        Score.scoreValue -= currentCost;
                        generateCost();
                    }
                }
            }
        }
    }

    //If the player leaves the range then disable the text displaying the price to heal
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            itemPickup.SetActive(false);
        }
    }

    //Update the text to display the current cost to heal
    public void itemTextChange()
    {
        itemPickupText.text = "E to heal -" + currentCost;
    }

    //Randomly increase the current cost by any amount up to 1000 more then the current cost
    private void generateCost()
    {
        updatedCost = Random.Range(currentCost, (currentCost + 1000));
        currentCost = updatedCost;
    }
}
