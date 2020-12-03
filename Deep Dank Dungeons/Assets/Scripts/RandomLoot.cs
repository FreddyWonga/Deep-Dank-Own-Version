using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    public List<GameObject> items;
    public Transform itemSpawnLoc;

    public int total;
    public int randomNumber;
    public int randomItemType;
    public int wandDrop;
    public int helmetDrop;
    public int chestpieceDrop;


    //Generate a random number between 1 and 100, if the number is above 85 generate a random number between 1 and 3
    //if the number is 1 then randomly generate a number between 1 and 100, 0-40 is a green staff, 41-70 is a blue staff, 71-90 is a yellow staff, 91-100 is a purple staff
    //if the number is 2 then randomly generate a number between 1 and 100, 0-40 is a iron helmet, 41-70 is a mana helmet, 71-90 is a gold helmet, 91-100 is a magma helmet
    //if the number is 3 then randomly generate a number between 1 and 100, 0-40 is a iron chest, 41-70 is a mana chest, 71-90 is a gold chest, 91-100 is a magma chest
    public void GetRandomItem()
    {
        randomNumber = Random.Range(1, 101);
        if (randomNumber >= 10)
            //85
        {
            Debug.Log("Dropped Item");
            randomItemType = Random.Range(1, 4);
            if(randomItemType == 1)
            {
                wandDrop = Random.Range(1, 100);
                if (wandDrop <= 40)
                {
                    Instantiate(items.ElementAt ( 0 ), itemSpawnLoc.transform.position, Quaternion.Euler(270,0,0));
                }
                else if (41 <= wandDrop && wandDrop <= 70)
                {
                    Instantiate(items.ElementAt(1), itemSpawnLoc.transform.position, Quaternion.Euler(270, 0, 0));
                }
                else if (71 <= wandDrop && wandDrop <= 90)
                {
                    Instantiate(items.ElementAt(2), itemSpawnLoc.transform.position, Quaternion.Euler(270, 0, 0));
                }
                else if (91 <= wandDrop)
                {
                    Instantiate(items.ElementAt(3), itemSpawnLoc.transform.position, Quaternion.Euler(270, 0, 0));
                }
            }
            else if(randomItemType == 2)
            {
                helmetDrop = Random.Range(1, 100);
                if (helmetDrop <= 40)
                {
                    Instantiate(items.ElementAt(4), itemSpawnLoc.transform.position, Quaternion.Euler(-90, 180, 0));
                }
                else if (41 <= helmetDrop && helmetDrop <= 70)
                {
                    Instantiate(items.ElementAt(5), itemSpawnLoc.transform.position, Quaternion.Euler(-90, 180, 0));
                }
                else if (71 <= helmetDrop && helmetDrop <= 90)
                {
                    Instantiate(items.ElementAt(6), itemSpawnLoc.transform.position, Quaternion.Euler(-90, 180, 0));
                }
                else if (91 <= helmetDrop)
                {
                    Instantiate(items.ElementAt(7), itemSpawnLoc.transform.position, Quaternion.Euler(-90, 180, 0));
                }
            }
            else if(randomItemType == 3)
            {
                chestpieceDrop = Random.Range(1, 100);
                if (chestpieceDrop <= 40)
                {
                    Instantiate(items.ElementAt(8), itemSpawnLoc.transform.position, Quaternion.Euler(-90, 180, 0));
                }
                else if (41 <= chestpieceDrop && chestpieceDrop <= 70)
                {
                    Instantiate(items.ElementAt(9), itemSpawnLoc.transform.position, Quaternion.Euler(-90, 180, 0));
                }
                else if (71 <= chestpieceDrop && chestpieceDrop <= 90)
                {
                    Instantiate(items.ElementAt(10), itemSpawnLoc.transform.position, Quaternion.Euler(-90, 180, 0));
                }
                else if (91 <= chestpieceDrop)
                {
                    Instantiate(items.ElementAt(11), itemSpawnLoc.transform.position, Quaternion.Euler(-90, 180, 0));
                }
            }
        }
    }
}
