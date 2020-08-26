using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter1 : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 10f;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) ;
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            GameObject.Destroy(instantiatedProjectile.gameObject, 10f);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameObject.Destroy(projectile);
        }
    }

}
