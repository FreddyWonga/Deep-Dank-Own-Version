using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public Image[] manaImg;
    public Sprite fullMana;
    public Sprite emptyMana;
    public Shooter mana;

    private void Start()
    {
        for(int i=0; i < manaImg.Length; i++)
        {
            GameObject manaImage = GameObject.Find("Mana (" + (i + 1) + ")");

            if(manaImage != null)
            {
                manaImg[i] = manaImage.GetComponent<Image>();
            }
        }
    }//disable and enable mana crystals on the HUD to display current player mana
    void Update()
    {
        if (mana.mana > mana.max_mana)
        {
            mana.mana = mana.max_mana;
        }

        for (int i = 0; i < manaImg.Length; i++)
        {
            if (i < mana.mana)
            {
                manaImg[i].sprite = fullMana;
            }
            else
            {
                manaImg[i].sprite = emptyMana;
            }


            if (i < mana.max_mana)
            {
                manaImg[i].enabled = true;
            }
            else
            {
                manaImg[i].enabled = false;
            }
        }
    }
}
