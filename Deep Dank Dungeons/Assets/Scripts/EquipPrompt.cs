using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPrompt : MonoBehaviour
{
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
    public bool promptOn;

    public Equipment currentHelmet;
    public Equipment currentChest;
    public Equipment currentStaff;
    public static string PromptText = "test";


    public Shooter shooter;
    Text Prompt;

    void Start()
    {
        Prompt = GetComponent<Text>();
    }


    void Update()
    {
        if (promptOn == true)
        {
            Prompt.text = "E to equip" +
            "testing" + currentChest.data.bonus;
        }
        
    }

    
}