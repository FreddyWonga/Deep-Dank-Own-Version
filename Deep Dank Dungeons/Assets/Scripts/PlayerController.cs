using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public LevelBuilder levelBuilder;

    public static PlayerController instance;

    private HashID hash;

    public Animator movement;
    public float speed = 12f;

    Vector3 velocity;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        movement = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HashID>();
    }

    private void Start()
    {
        levelBuilder = FindObjectOfType<LevelBuilder>();
    }

    //Move the player
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        if(x != 0 || z!= 0)
        {
            movement.SetBool(hash.moving, true);
        }
        else
        {
            movement.SetBool(hash.moving, false);
        }
    }

    //If player collides with the portal then load the next scene
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
