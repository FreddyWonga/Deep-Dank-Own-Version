using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kyana Bowers

public class Shooter : MonoBehaviour
{
    //this script is connected to an empty game object in front of the player
    //requires a bullet prefab to fire.

    public Rigidbody projectile;
    public float speed = 40f;

    /// <summary>
    /// every time the user clicks the player fires a projectile
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0));
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;

            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));

            GameObject.Destroy(instantiatedProjectile.gameObject, 0.5f);
        }
    }
}
