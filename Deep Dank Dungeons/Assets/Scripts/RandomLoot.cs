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

    public void GetRandomItem()
    {
        randomNumber = Random.Range(1, 101);
        if (randomNumber >= 10)
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
