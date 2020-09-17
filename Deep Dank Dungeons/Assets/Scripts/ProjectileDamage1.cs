using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage1 : MonoBehaviour
{
    public int projectileDamage = 1;

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
