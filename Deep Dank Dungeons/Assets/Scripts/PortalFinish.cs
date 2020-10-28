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

    private void Update()
    {
        if (ItemSlot.RunePlaced == true)
        {
            if(SceneManager.GetActiveScene().buildIndex != 4)
            {
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
