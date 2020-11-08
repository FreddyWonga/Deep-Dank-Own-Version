using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage1 : MonoBehaviour
{
    public int projectileDamage = 1;

    //When the spell touches the enemy and the enemy has more then 0 health, subtract the spells damage from the enemies health
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<Enemy_Health>().currentHealth > 0)
            {
                other.GetComponent<Enemy_Health>().ApplyDamage(projectileDamage);
            }
        }
    }
}
