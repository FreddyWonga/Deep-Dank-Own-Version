﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spectator : NetworkBehaviour
{
    NetworkIdentity identity;
    [SerializeField]
    Transform target;
    Camera cam;

    private void Awake()
    {
        identity = GetComponent<NetworkIdentity>();
    }

    private void Start()
    {
        CmdSetPlayerRating();
        ResetPlayerInstance();
    }

    public void Initialize(NetworkIdentity identityInstance)
    {
        identity = identityInstance;
        GetComponent<NetworkTransform>().transformSyncMode = NetworkTransform.TransformSyncMode.SyncTransform;
        Debug.Log(Stats.instance.name);
        StartCoroutine(SetSpectatorTarget());
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
            if (Input.GetKeyDown(KeyCode.O))
            {
                RatePlayer(5);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                RatePlayer(-5);
            }
        }
    }

    void RatePlayer(int rating)
    {
        Stats.instance.playerRating += rating;
        Rating.instance.UpdateRating(Stats.instance.playerRating);
        CmdRatePlayer(Stats.instance.playerRating);
    }

    public void ResetPlayerInstance()
    {
        CmdResetPlayerInstance();
    }

    [Command]
    public void CmdRatePlayer(int newRating)
    {
        Stats.instance.playerRating = newRating;
        RpcRatePlayer(Stats.instance.playerRating);
    }

    [ClientRpc]
    void RpcRatePlayer(int newRating)
    {
        if (identity.isLocalPlayer == false)
        {
            Stats.instance.playerRating = newRating;
            Rating.instance.UpdateRating(Stats.instance.playerRating);
        }
    }

    [Command]
    public void CmdSetPlayerRating()
    {
        RpcSetPlayerRating(Stats.instance.playerRating);
    }

    [ClientRpc]
    void RpcSetPlayerRating(int rating)
    {
        Stats.instance.playerRating = rating;
        Rating.instance.UpdateRating(Stats.instance.playerRating);
    }

    [Command]
    public void CmdSetPlayer(GameObject playerInstance)
    {
        Debug.Log("CmdSetPlayer");
        Stats.instance.player = playerInstance;
        Debug.Log(Stats.instance.player);
        RpcSetPlayer(Stats.instance.player);
    }

    [Command] 
    void CmdResetPlayerInstance()
    {
        RpcSetPlayer(Stats.instance.player);
    }

    [ClientRpc]
    void RpcSetPlayer(GameObject playerInstance)
    {
        Stats.instance.player = playerInstance;
        Debug.Log("RmdSetPlayer");
        Debug.Log(Stats.instance.player);
    }

    IEnumerator SetSpectatorTarget()
    {
        while(Stats.instance.player == null)
        {
            yield return new WaitForEndOfFrame();
        }
        target = Stats.instance.player.transform;
    }
}
