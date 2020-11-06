using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Created by David Borger
    // Based off this online tutorial https://www.youtube.com/watch?v=_QajrabyTJc&t= 
    public CharacterController controller;
    public LevelBuilder levelBuilder;

    public static PlayerController instance;

    public Animator movement;
    private HashIDs hash;
    public float speed = 12f;

    Vector3 velocity;

    [SyncVar]
    private float currentSpeed;

    NetworkIdentity identity;
    Spectator spectator;

    void NetworkSetup()
    {
        if(identity.isServer == true)
        {
            if(identity.isLocalPlayer == true)
            {
                name = "Local - Host";
                if(Stats.instance.player == null)
                {
                    spectator.CmdSetPlayer(gameObject);

                }
            }
            else
            {
                spectator.ResetPlayerInstance();
                StartCoroutine(SetupRemotePlayer());
            }
            name = name += " (Server Side)";
        }
        else
        {
            spectator.ResetPlayerInstance();
            if(identity.isLocalPlayer == true)
            {
                SetupSpectator(true);
            }
            else
            {
                StartCoroutine(SetupRemotePlayer())
            }
            name = name += " (Server Side)";
        }
    }

    IEnumerator SetupRemotePlayer()
    {
        while(Stats.instance.player == null)
        {
            yield return new WaitForEndOfFrame();
        }
        if(gameObject == Stats.instance.player)
        {
            name = "Remote - Host";
            transform.GetChild(0).gameObject.SetActive(false);
            enabled = false;
        }
    }

    void SetupSpectator(bool local)
    {
        if(local == true)
        {
            name = "Local - Spectator";
            transform.GetChild(0).GetComponent<Mouselook>().enabled = false;
            transform.GetChild(0).transform.position = new Vector3(0, 3, -5);
            transform.GetChild(0).transform.eulerAngles = new Vector3(45, 0, 0);
            GetComponent<MeshRenderer>().enabled = false;
            spectator.enabled = true;
            tag = "Spectator";
            spectator.Initialize(identity);
            enabled = false;
            controller.enabled = false;
        }
        else
        {
            name = "Remote - Spectator";
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<NetworkTransform>().transformSyncMode = NetworkTransform.TransformSyncMode.SyncTransform;
            enabled = false;
            controller.enabled = false;
            tag = "Spectator";
        }
    }


    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        movement = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HashIDs>();

        identity = GetComponent<NetworkIdentity>();
        spectator = GetComponent<Spectator>();
    }

    private void Start()
    {
        NetworkSetup();
        levelBuilder = FindObjectOfType<LevelBuilder>();
    }

    void Update()
    {

        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        if(x != 0 || z!= 0)
        {
            movement.SetBool(hash.walkingBool, true);
        }
        else
        {
            movement.SetBool(hash.walkingBool, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
   
   

}
