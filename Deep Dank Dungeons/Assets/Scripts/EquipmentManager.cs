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

    public PlayerData playerData;

    public Equipment currentHelmet;
    public Equipment currentChest;
    public Equipment currentStaff;

    public GameObject itemPickup;
    public Text itemPickupText;
    public Shooter shooter;

    //If the player already has armor equipt then equip that armor and staff
    private void Start()
    {
        GetComponent<Shooter>();
        GetComponent<Health>();
        GetComponent<PlayerData>();
        if(playerData.currentHelmet != null)
        {
            EquipArmour(playerData.currentHelmet);
        }
        if (playerData.currentChest != null)
        {
            EquipArmour(playerData.currentChest);
        }
        if (playerData.currentStaff != null)
        {
            EquipArmour(playerData.currentStaff);
        }
    }

    //Find what the item is, run the set item function, turn the mesh on for the player and increase the the players stats by the bonus
    public void EquipArmour(Equipment equipment)
    {
        if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Iron_Helmet")
        {
            SetHelmet(equipment);
            ironHelmet.gameObject.SetActive(true);

            StatTracker.Instance.MaxHealth += currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Iron_Chest")
        {
            SetChest(equipment);
            ironChest.gameObject.SetActive(true);
            StatTracker.Instance.MaxHealth += currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Gold_Helmet")
        {
            SetHelmet(equipment);
            goldenHelmet.gameObject.SetActive(true);
            StatTracker.Instance.MaxHealth += currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Gold_Chest")
        {
            SetChest(equipment);
            goldenChest.gameObject.SetActive(true);
            StatTracker.Instance.MaxHealth += currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Mana_Helmet")
        {
            SetHelmet(equipment);
            manaHelmet.gameObject.SetActive(true);
            StatTracker.Instance.max_mana += currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Mana_Chest")
        {
            SetChest(equipment);
            manaChest.gameObject.SetActive(true);
            StatTracker.Instance.max_mana += currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Magma_Helmet")
        {
            SetHelmet(equipment);
            magmaHelmet.gameObject.SetActive(true);
            StatTracker.Instance.max_mana += currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Magma_Chest")
        {
            SetChest(equipment);
            magmaChest.gameObject.SetActive(true);
            StatTracker.Instance.max_mana += currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Green Staff")
        {
            SetStaff(equipment);
            greenStaff.gameObject.SetActive(true);
            StatTracker.Instance.mana_regen_speed += currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Blue Staff")
        {
            SetStaff(equipment);
            blueStaff.gameObject.SetActive(true);
            StatTracker.Instance.mana_regen_speed += currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Yellow Staff")
        {
            SetStaff(equipment);
            yellowStaff.gameObject.SetActive(true);
            StatTracker.Instance.mana_regen_speed += currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Purple Staff")
        {
            SetStaff(equipment);
            purpleStaff.gameObject.SetActive(true);
            StatTracker.Instance.mana_regen_speed += currentStaff.data.bonus;
        }
    }

    //Find what the item is, turn the mesh off for the player and decrease the the players stats by the bonus
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

    //If player already has an item equipted then unequip the current item and spawn it in the scene
    //Disable the item being picked up from the scene and set the current helmet to the one picked up
    private void SetHelmet(Equipment helmet)
    {
        if (currentHelmet != null)
        {
            SpawnArmour(currentHelmet);
            UnequipArmour(currentHelmet);
        }
        currentHelmet = helmet;
        playerData.currentHelmet = helmet;
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
        playerData.currentChest = chest;
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
        playerData.currentStaff = staff;
        staff.gameObject.SetActive(false);
    }

    //Spawn the dropped item in the world and set it to active
    private void SpawnArmour(Equipment armour)
    {
        armour.gameObject.SetActive(true);
        armour.transform.position = itemDropLocation.transform.position;
        armour.transform.localRotation = itemDropLocation.transform.rotation;
    }

}
