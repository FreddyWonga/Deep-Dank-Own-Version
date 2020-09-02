using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Projectile : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rooms")
        {
            Destroy(this.gameObject);
        }
       
        if (other.tag == "Doors")
        {
            Destroy(this.gameObject);
        }
    }
}