using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    // Created by David Borger
    // https://www.youtube.com/watch?v=urNrY7FgMao Based off this script

    public Transform PlayerTransform;

    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float Smoothfactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - PlayerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = PlayerTransform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, Smoothfactor);
    }
}
