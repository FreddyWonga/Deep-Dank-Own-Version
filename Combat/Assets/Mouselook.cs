using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselook : MonoBehaviour
{
    // Created by David Borger
    // Based off this script https://answers.unity.com/questions/805776/isometric-game-player-look-at-cursor.html
    Ray Ray; 

    RaycastHit RayHit;

    // Update is called once per frame
    void Update()
    {
        // Cast a Ray to the Ground
        Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If Ray hits an object..
        if (Physics.Raycast(Ray, out RayHit))
        {
            if (RayHit.transform.tag == "Ground")
            {
                Vector3 targetPosition = new Vector3(RayHit.point.x, transform.position.y, RayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }
    }
}
