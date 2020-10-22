using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public int health = 3;
    public int MaxHealth = 3;

    public float mana = 3f;
    public float max_mana = 3f;
    public float mana_regen_speed = 1f;

    public int scoreValue = 0;
    public int currentScore;

    public static StatTracker Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
