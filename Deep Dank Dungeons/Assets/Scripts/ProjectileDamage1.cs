//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ProjectileDamage1 : MonoBehaviour
//{
//    public float damageValue = 1f;

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
