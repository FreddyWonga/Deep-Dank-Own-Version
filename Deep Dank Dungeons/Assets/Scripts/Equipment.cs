using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public ArmourData data;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
                other.GetComponent<EquipmentManager>().EquipArmour(this);
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