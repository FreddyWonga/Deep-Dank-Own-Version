using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Projectile : MonoBehaviour
{
    //If the spell collides with the walls, doors or enemy, destroy the spell
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rooms" || other.tag == "Doors" || other.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
       
        if (other.tag == "Doors")
        {
            Destroy(this.gameObject);
        }
    }
}