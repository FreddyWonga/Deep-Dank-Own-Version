using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy_Health : MonoBehaviour
{
    public GameObject BoneExplode;
    public Transform BoneExplodePosition;
    public int currentHealth = 3;
    public int enemy_max_health = 3;
    public RandomLoot randomLoot;

    private void Awake()
    {
        randomLoot = GetComponent<RandomLoot>();
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            GameObject boneExplode = Instantiate(BoneExplode, BoneExplodePosition.transform.position, BoneExplodePosition.transform.rotation);
            randomLoot.GetRandomItem();
            Destroy(boneExplode, 0.75f);
            Destroy(this.gameObject);
            Score.scoreValue += 100;
            
        }
    }
}