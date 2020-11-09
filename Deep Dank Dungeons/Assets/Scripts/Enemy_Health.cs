using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy_Health : MonoBehaviour
{
    public GameObject BoneExplode;
    public Transform BoneExplodePosition;
    public int currentHealth = 3;
    public int enemy_max_health = 3;
    public RandomLoot randomLoot;

    private void Awake()
    {
        randomLoot = GetComponent<RandomLoot>();
    }

    //Subtract players damage from the enemies current health
    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
    }

    //If health gets to below zero, drop a random item, destroy the enemy and increase the players score by 100
    void Update()
    {
        if (currentHealth <= 0)
        {
            randomLoot.GetRandomItem();
            Destroy(this.gameObject);
            Score.scoreValue += 100;
            
        }
    }
}