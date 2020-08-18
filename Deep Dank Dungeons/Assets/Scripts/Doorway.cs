using System;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    void onDrawGizmos()
    {
        Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }

    internal void Remove(Doorway availableDoorway)
    {
        throw new NotImplementedException();
    }
}
