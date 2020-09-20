using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy_Health : MonoBehaviour
{
    public GameObject BoneExplode;
    public int currentHealth = 3;
    public int enemy_max_health = 3;

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(BoneExplode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Score.scoreValue += 100;
            
        }
    }
}