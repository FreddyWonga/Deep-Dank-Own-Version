using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Projectile : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rooms")
        {
            Destroy(collision.gameObject);
        }
    }
}
