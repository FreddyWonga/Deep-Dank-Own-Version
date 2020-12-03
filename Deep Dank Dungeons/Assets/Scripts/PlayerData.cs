using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public GameObject playerInstance;

    public Equipment currentHelmet;
    public Equipment currentChest;
    public Equipment currentStaff;
    public EquipmentManager equipmentManager;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //Stores the current equipment infomation
    public void GameReset()
    {
        currentChest = null;
        currentHelmet = null;
        currentStaff = null;
    }
}