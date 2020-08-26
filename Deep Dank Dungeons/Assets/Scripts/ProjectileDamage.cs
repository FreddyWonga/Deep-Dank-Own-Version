//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

////Kyana Bowers

//public class ProjectileDamage : MonoBehaviour
//{
//    //this script is attached to the projectile that the player fires at the enemy.

//    public float damageValue = 1f;

//    /// <summary>
//    /// When the enemy touches the projectile, enemy takes damage.
//    /// </summary>
//    /// <param name="other"></param>
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Enemy")
//        {
//            if (other.GetComponent<Health>().GetCurrentHealth() > 0)
//            {
//                other.GetComponent<Health>().ApplyDamage(damageValue);
//            }
//        }
//    }
//}

