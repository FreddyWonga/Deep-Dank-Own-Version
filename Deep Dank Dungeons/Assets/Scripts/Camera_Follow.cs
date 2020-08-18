using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    // Created by David Borger
    // https://www.youtube.com/watch?v=urNrY7FgMao Based off this script

    public Transform playerTransform;

    private Vector3 cameraOffset;
    public bool lookAtPlayer = false;

    [Range(0.01f, 1.0f)]
    public float Smoothfactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = playerTransform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, Smoothfactor);

        if (lookAtPlayer)
        {
            transform.LookAt(playerTransform);
        }
    }
}
