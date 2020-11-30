using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselook : MonoBehaviour
{
    Ray ray; 
    RaycastHit rayHit;

    //Turns the player to face the point on the "ground" where the mouse is
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
        transform.LookAt(targetPosition);
    }
}
