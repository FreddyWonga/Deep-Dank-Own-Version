//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Item : MonoBehaviour
//{
//    public string itemName;
//    public int index;

//    //Custom data and funconality can be inserted here

//    private void OnTriggerEnter(Collider Other)
//    {
//        if (Other.CompareTag("Player") == true)
//        {
//            Inventory inv = other.GetComponent<Inventory>();
//            if(inv.items.AddNode(new BinaryNode_Item(this, index)) == true)
//            {
//                inv.TraverseItems(inv.items.root);
//                gameObject.SetActive(false);
//            }
//        }
//    }
//}
