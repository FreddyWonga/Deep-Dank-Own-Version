using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject DragDrop;

    // Start is called before the first frame update
    void Start()
    {
        DragDrop.SetActive(false);
    }

    // Update is called once per frame
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
