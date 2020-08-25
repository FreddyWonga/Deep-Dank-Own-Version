using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Created by David Borger
    // Based off this online tutorial https://www.youtube.com/watch?v=_QajrabyTJc&t= 
    public CharacterController controller;
    public LevelBuilder levelBuilder;

    public float speed = 12f;

    Vector3 velocity;

    private void Start()
    {
        levelBuilder = FindObjectOfType<LevelBuilder>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
