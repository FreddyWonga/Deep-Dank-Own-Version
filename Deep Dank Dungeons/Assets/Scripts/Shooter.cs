using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 500f;
    public Animator shoot;

    public StatTracker StatTracker;

    private float timer = 0;

    private void Awake()
    {
        shoot = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //If the player left clicks and they have enough mana, fire a spell and start regenning mana
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (StatTracker.Instance.mana >= 1f)
            {
                StatTracker.Instance.mana -= 1f;
                shoot.SetTrigger("cast");

                Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
                instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
                GameObject.Destroy(instantiatedProjectile.gameObject, 10f);
                timer = 0;
            }
        }
        
        if(timer < StatTracker.Instance.mana_regen_speed) 
        {
            timer += Time.deltaTime;
            if(timer >= StatTracker.Instance.mana_regen_speed) 
            {
                if(StatTracker.Instance.mana < StatTracker.Instance.max_mana) 
                {
                    StatTracker.Instance.mana++;
                    timer = 0;
                }
                //reset to,timer
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(projectile);
    }
}
