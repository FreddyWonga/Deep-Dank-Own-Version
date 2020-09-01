﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 500f;
    public float mana = 3f;
    public float max_mana = 3f;
    public float mana_regen_speed = 1f;
    private bool RegenRunning;


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (mana >= 1f)
            {
                mana -= 1f;
                //StopCoroutine(ManaRegen());
                Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
                instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
                GameObject.Destroy(instantiatedProjectile.gameObject, 10f);
                if (RegenRunning == false)
                {
                    StartCoroutine(ManaRegen());
                }
            }
        }
        Debug.Log(mana);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
        GameObject.Destroy(projectile);
    }


    IEnumerator ManaRegen()
    {
        RegenRunning = true;
        Debug.Log("WE BE RUNNING");
        yield return new WaitForSeconds(5);
        if (mana != max_mana)
        {
            for (; mana < max_mana;)
            {
                mana += mana_regen_speed * Time.deltaTime;
            }
            RegenRunning = false;
        }
        

    }
}
