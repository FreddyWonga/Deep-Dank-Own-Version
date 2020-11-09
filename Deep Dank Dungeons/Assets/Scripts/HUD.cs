using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject DragDrop;

    //Disable the rune and runestone
    void Start()
    {
        DragDrop.SetActive(false);
    }

    //Enable the rune and runestone if the the portal active bool is true
    void Update()
    {
        if(PortalFinish.PortalActive == true)
        {
            DragDrop.SetActive(true);
        }
        else
        {
            DragDrop.SetActive(false);
        }
    }
}
