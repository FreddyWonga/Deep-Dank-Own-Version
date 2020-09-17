using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int currentHealth = 3;
    public int enemy_max_health = 3;

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
    }
    

}
