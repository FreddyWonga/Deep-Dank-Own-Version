using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public Transform itemDropLocation;

    public GameObject ironChest;
    public GameObject ironHelmet;
    public GameObject goldenChest;
    public GameObject goldenHelmet;
    public GameObject manaChest;
    public GameObject manaHelmet;
    public GameObject magmaChest;
    public GameObject magmaHelmet;
    public GameObject greenStaff;
    public GameObject blueStaff;
    public GameObject yellowStaff;
    public GameObject purpleStaff;

    public GameObject PlayerData;

    public Equipment currentHelmet;
    public Equipment currentChest;
    public Equipment currentStaff;

    public GameObject itemPickup;
    public Text itemPickupText;
    public Shooter shooter;

    private void Start()
    {
        GetComponent<Shooter>();
        GetComponent<Health>();
        //itemPickupText = GameObject.FindGameObjectWithTag("PickupText").GetComponent<Text>();
    }

    public void EquipArmour(Equipment equipment)
    {
        if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Iron_Helmet")
        {
            SetHelmet(equipment);
            Debug.Log("Before Activated");
            ironHelmet.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.MaxHealth += currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Iron_Chest")
        {
            SetChest(equipment);
            Debug.Log("Before Activated");
            ironChest.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.MaxHealth += currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Gold_Helmet")
        {
            SetHelmet(equipment);
            Debug.Log("Before Activated");
            goldenHelmet.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.MaxHealth += currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Gold_Chest")
        {
            SetChest(equipment);
            Debug.Log("Before Activated");
            goldenChest.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.MaxHealth += currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Mana_Helmet")
        {
            SetHelmet(equipment);
            Debug.Log("Before Activated");
            manaHelmet.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.max_mana += currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Mana_Chest")
        {
            SetChest(equipment);
            Debug.Log("Before Activated");
            manaChest.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.max_mana += currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Magma_Helmet")
        {
            SetHelmet(equipment);
            Debug.Log("Before Activated");
            magmaHelmet.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.max_mana += currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Magma_Chest")
        {
            SetChest(equipment);
            Debug.Log("Before Activated");
            magmaChest.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.max_mana += currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Green Staff")
        {
            SetStaff(equipment);
            Debug.Log("Before Activated");
            greenStaff.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.mana_regen_speed += currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Blue Staff")
        {
            SetStaff(equipment);
            Debug.Log("Before Activated");
            blueStaff.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.mana_regen_speed += currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Yellow Staff")
        {
            SetStaff(equipment);
            Debug.Log("Before Activated");
            yellowStaff.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.mana_regen_speed += currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Purple Staff")
        {
            SetStaff(equipment);
            Debug.Log("Before Activated");
            purpleStaff.gameObject.SetActive(true);
            Debug.Log("After Activated");
            StatTracker.Instance.mana_regen_speed += currentStaff.data.bonus;
        }
    }
    public void UnequipArmour(Equipment equipment)
    {
        if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Iron_Helmet")
        {
            ironHelmet.gameObject.SetActive(false);
            StatTracker.Instance.MaxHealth -= currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Iron_Chest")
        {
            ironChest.gameObject.SetActive(false);
            StatTracker.Instance.MaxHealth -= currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Gold_Helmet")
        {
            goldenHelmet.gameObject.SetActive(false);
            StatTracker.Instance.MaxHealth -= currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Gold_Chest")
        {
            goldenChest.gameObject.SetActive(false);
            StatTracker.Instance.MaxHealth -= currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Mana_Helmet")
        {
            manaHelmet.gameObject.SetActive(false);
            StatTracker.Instance.max_mana -= currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Mana_Chest")
        {
            manaChest.gameObject.SetActive(false);
            StatTracker.Instance.max_mana -= currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Magma_Helmet")
        {
            magmaHelmet.gameObject.SetActive(false);
            StatTracker.Instance.max_mana -= currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Magma_Chest")
        {
            magmaChest.gameObject.SetActive(false);
            StatTracker.Instance.max_mana -= currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Green Staff")
        {
            greenStaff.gameObject.SetActive(false);
            StatTracker.Instance.mana_regen_speed -= currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Blue Staff")
        {
            blueStaff.gameObject.SetActive(false);
            StatTracker.Instance.mana_regen_speed -= currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Yellow Staff")
        {
            yellowStaff.gameObject.SetActive(false);
            StatTracker.Instance.mana_regen_speed -= currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Purple Staff")
        {
            purpleStaff.gameObject.SetActive(false);
            StatTracker.Instance.mana_regen_speed -= currentStaff.data.bonus;
        }
    }
    private void SetHelmet(Equipment helmet)
    {
        if (currentHelmet != null)
        {
            SpawnArmour(currentHelmet);
            UnequipArmour(currentHelmet);
        }
        currentHelmet = helmet;
        helmet.gameObject.SetActive(false);

    }

    private void SetChest(Equipment chest)
    {
        if (currentChest != null)
        {
            SpawnArmour(currentChest);
            UnequipArmour(currentChest);
        }
        currentChest = chest;
        chest.gameObject.SetActive(false);
    }
    private void SetStaff(Equipment staff)
    {
        if (currentStaff != null)
        {
            SpawnArmour(currentStaff);
            UnequipArmour(currentStaff);
        }
        currentStaff = staff;
        staff.gameObject.SetActive(false);
    }

    private void SpawnArmour(Equipment armour)
    {
        armour.gameObject.SetActive(true);
        armour.transform.position = itemDropLocation.transform.position;
        armour.transform.localRotation = itemDropLocation.transform.rotation;
    }

//    private void OnTriggerStay(Collider other)
//    {
//        if(other.tag == "ItemDrop")
//        {
//            itemPickup.SetActive(true);
//            itemTextChange(other.gameObject.name);
//        }
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            this.transform.parent = PlayerData.transform;
//            itemPickup.SetActive(false);
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if(other.tag == "ItemDrop")
//        {
//            itemPickup.SetActive(false);
//        }
//    }

//    public void itemTextChange(string itemName)
//    {
//        itemPickupText.text = "E to equip";
//    }

}
