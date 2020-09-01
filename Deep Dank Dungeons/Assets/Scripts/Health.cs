﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 3;
    public int MaxHealth = 3;

    public Image[] Hearts;
    public Sprite fullHearts;
    public Sprite emptyHeart;

    void Update()
    {
        if (health > MaxHealth)
        {
            health = MaxHealth;
        }

        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < health)
            {
                Hearts[i].sprite = fullHearts;
            }
            else
            {
                Hearts[i].sprite = emptyHeart;
            }


            if (i < MaxHealth)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
        }
    }
}