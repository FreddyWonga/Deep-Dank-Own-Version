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

    public GameObject itemPickup;
    public Text itemPickupText;
    public Shooter shooter;

    //If the player already has armor equipt then equip that armor and staff
    private void Start()
    {
        GetComponent<Shooter>();
        GetComponent<Health>();
        playerData = FindObjectOfType<PlayerData>();
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
            ironHelmet.gameObject.SetActive(true);
            SetHelmet(ironHelmet.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.MaxHealth += playerData.currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Iron_Chest")
        {
            ironChest.gameObject.SetActive(true);
            SetChest(ironChest.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.MaxHealth += playerData.currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Gold_Helmet")
        {
            goldenHelmet.gameObject.SetActive(true);
            SetHelmet(goldenHelmet.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.MaxHealth += playerData.currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Gold_Chest")
        {
            goldenChest.gameObject.SetActive(true);
            SetChest(goldenChest.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.MaxHealth += playerData.currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Mana_Helmet")
        {
            manaHelmet.gameObject.SetActive(true);
            SetHelmet(manaHelmet.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.max_mana += playerData.currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Mana_Chest")
        {
            manaChest.gameObject.SetActive(true);
            SetChest(manaChest.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.max_mana += playerData.currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Magma_Helmet")
        {
            magmaHelmet.gameObject.SetActive(true);
            SetHelmet(magmaHelmet.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.max_mana += playerData.currentHelmet.data.bonus;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Magma_Chest")
        {
            magmaChest.gameObject.SetActive(true);
            SetChest(magmaChest.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.max_mana += playerData.currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Green Staff")
        {
            greenStaff.gameObject.SetActive(true);
            SetStaff(greenStaff.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.mana_regen_speed += playerData.currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Blue Staff")
        {
            blueStaff.gameObject.SetActive(true);
            SetStaff(blueStaff.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.mana_regen_speed += playerData.currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Yellow Staff")
        {
            yellowStaff.gameObject.SetActive(true);
            SetStaff(yellowStaff.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.mana_regen_speed += playerData.currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Purple Staff")
        {
            purpleStaff.gameObject.SetActive(true);
            SetStaff(purpleStaff.GetComponent<Equipment>(), equipment);
            StatTracker.Instance.mana_regen_speed += playerData.currentStaff.data.bonus;
        }
    }

    //Find what the item is, turn the mesh off for the player and decrease the the players stats by the bonus
    public void UnequipArmour(Equipment equipment)
    {
        if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Iron_Helmet")
        {
            ironHelmet.gameObject.SetActive(false);
            StatTracker.Instance.MaxHealth -= playerData.currentHelmet.data.bonus;
            playerData.currentHelmet = null;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Iron_Chest")
        {
            ironChest.gameObject.SetActive(false);
            StatTracker.Instance.MaxHealth -= playerData.currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Gold_Helmet")
        {
            goldenHelmet.gameObject.SetActive(false);
            StatTracker.Instance.MaxHealth -= playerData.currentHelmet.data.bonus;
            playerData.currentHelmet = null;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Gold_Chest")
        {
            goldenChest.gameObject.SetActive(false);
            StatTracker.Instance.MaxHealth -= playerData.currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Mana_Helmet")
        {
            manaHelmet.gameObject.SetActive(false);
            StatTracker.Instance.max_mana -= playerData.currentHelmet.data.bonus;
            playerData.currentHelmet = null;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Mana_Chest")
        {
            manaChest.gameObject.SetActive(false);
            StatTracker.Instance.max_mana -= playerData.currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.helmet && equipment.data.name == "Magma_Helmet")
        {
            magmaHelmet.gameObject.SetActive(false);
            StatTracker.Instance.max_mana -= playerData.currentHelmet.data.bonus;
            playerData.currentHelmet = null;
        }
        else if (equipment.data.type == ArmourType.chest && equipment.data.name == "Magma_Chest")
        {
            magmaChest.gameObject.SetActive(false);
            StatTracker.Instance.max_mana -= playerData.currentChest.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Green Staff")
        {
            greenStaff.gameObject.SetActive(false);
            StatTracker.Instance.mana_regen_speed -= playerData.currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Blue Staff")
        {
            blueStaff.gameObject.SetActive(false);
            StatTracker.Instance.mana_regen_speed -= playerData.currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Yellow Staff")
        {
            yellowStaff.gameObject.SetActive(false);
            StatTracker.Instance.mana_regen_speed -= playerData.currentStaff.data.bonus;
        }
        else if (equipment.data.type == ArmourType.staff && equipment.data.name == "Purple Staff")
        {
            purpleStaff.gameObject.SetActive(false);
            StatTracker.Instance.mana_regen_speed -= playerData.currentStaff.data.bonus;
        }
    }

    //If player already has an item equipted then unequip the current item and spawn it in the scene
    //Disable the item being picked up from the scene and set the current helmet to the one picked up
    private void SetHelmet(Equipment helmet, Equipment groundItem)
    {
        if (helmet.name != groundItem.name)
        {
            if (playerData.currentHelmet != null)
            {
                Debug.Log("Unequip");
                SpawnArmour(playerData.currentHelmet);
                UnequipArmour(playerData.currentHelmet);
            }
            playerData.currentHelmet = helmet;
            Debug.Log(groundItem);
            Destroy(groundItem.gameObject);
            Debug.Log(groundItem);
        }
    }

    private void SetChest(Equipment chest, Equipment groundItem)
    {
        if (playerData.currentChest != null)
        {
            Debug.Log("Unequip");
            SpawnArmour(playerData.currentChest);
            UnequipArmour(playerData.currentChest);
        }
        Debug.Log(chest);
        playerData.currentChest = chest;
        Debug.Log(groundItem);
        Destroy(groundItem.gameObject);
        Debug.Log(groundItem);
    }
    private void SetStaff(Equipment staff, Equipment groundItem)
    {
        if (playerData.currentStaff != null)
        {
            Debug.Log("Unequip");
            SpawnArmour(playerData.currentStaff);
            UnequipArmour(playerData.currentStaff);
        }
        Debug.Log(staff);
        playerData.currentStaff = staff;
        Debug.Log(groundItem);
        Destroy(groundItem.gameObject);
        Debug.Log(groundItem);
    }

    //Spawn the dropped item in the world and set it to active
    private void SpawnArmour(Equipment armour)
    {
        Debug.Log($"Spawn {armour.data.prefab} {itemDropLocation.transform.position}");
        var spawn = Instantiate(armour.data.prefab, itemDropLocation.transform.position, itemDropLocation.transform.rotation);
        spawn.SetActive(true);
    }

}
