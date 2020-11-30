using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Networking;

public class FirstPersonController : MonoBehaviour
{
    [SyncVar]
    private float currentSpeed;

    public float CurrentSpeed { get { return currentSpeed; } private set { currentSpeed = value; } }
    //public float Velocity { get; set; }
    //public bool IsGrounded { get; private set; }
    //public bool IsMoving { get; private set; }
    //public bool Crouching { get; private set; }


    NetworkIdentity identity;
    Spectator spectator;

    void NetworkSetup()
    {
        if (identity.isServer == true)
        {
            if (identity.isLocalPlayer == true)
            {
                name = "Local - Host";
                if (Stats.instance.player == null)
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
            if (identity.isLocalPlayer == true)
            {
                SetupSpectator(true);
            }
            else
            {
                StartCoroutine(SetupRemotePlayer());
            }
            name = name += " (Client Side)";
        }
    }

    IEnumerator SetupRemotePlayer()
    {
        while (Stats.instance.player == null)
        {
            yield return new WaitForEndOfFrame();
        }
        if (gameObject == Stats.instance.player)
        {
            name = "Remote - Host";
            transform.GetChild(0).gameObject.SetActive(false);
            enabled = false;
        }
        else
        {
            SetupSpectator(false);
        }
    }

    void SetupSpectator(bool local)
    {
        if (local == true)
        {
            name = "Local - Spectator";
            //transform.GetChild(0).GetComponent<MouseLook>().enabled = false;
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
        controller = GetComponent<CharacterController>();
        identity = GetComponent<NetworkIdentity>();
        spectator = GetComponent<Spectator>();
    }

    private void Start()
    {
        NetworkSetup();
    }

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9;
    public float jumpHeight = 3f;

    Vector3 velocity;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

}
