using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalFinish : MonoBehaviour
{
    public static bool PortalActive;

    private void Start()
    {
        PortalActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            PortalActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PortalActive = false;
        }
    }

    //If the runestone has been placed, increase current mana to max, load the next scene, and turn the RunePlaced bool to false
    //If the player is in the hell layer then load the Sewer scene instead of the next scene
    private void Update()
    {
        if (ItemSlot.RunePlaced == true)
        {
            if(SceneManager.GetActiveScene().buildIndex != 4)
            {
                StatTracker.Instance.mana = StatTracker.Instance.max_mana;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                ItemSlot.RunePlaced = false;
            }
            else
            {
                SceneManager.LoadScene("Sewer");
                ItemSlot.RunePlaced = false;
            }
            
        }
    }
}
