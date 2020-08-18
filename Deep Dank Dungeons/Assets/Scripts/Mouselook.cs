using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselook : MonoBehaviour
{
    Ray ray; 
    RaycastHit rayHit;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (rayHit.transform.tag == "Ground")
            {
                Vector3 targetPosition = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }
    }
}
