using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Equipment currentHelmet;
    public Equipment currentChest;
    public Equipment currentStaff;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void GameReset()
    {
        currentChest = null;
        currentHelmet = null;
        currentStaff = null;
    }
}