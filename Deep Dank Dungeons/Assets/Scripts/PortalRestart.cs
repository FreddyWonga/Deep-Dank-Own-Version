using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalRestart: MonoBehaviour
{
    public static bool PortalActive;

    private void Start()
    {
        PortalActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PortalActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PortalActive = false;
        }
    }

    private void Update()
    {
        if (ItemSlot.RunePlaced == true)
        {
            SceneManager.LoadScene("Sewer");
            ItemSlot.RunePlaced = false;
        }
    }
}