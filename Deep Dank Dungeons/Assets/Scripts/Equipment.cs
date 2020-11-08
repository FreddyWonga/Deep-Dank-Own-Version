using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public PlayerData playerData;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }
    public ArmourData data;

    //When the player is touching the item and pressing "E" equip the item to the player
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
                other.GetComponent<EquipmentManager>().EquipArmour(this);
                this.gameObject.transform.parent = playerData.gameObject.transform;
            }
        }
    }
}

public enum ArmourType { none, helmet, chest, staff }

[System.Serializable]
public struct ArmourData
{
    public ArmourType type;
    public string name;
    public int bonus;
}