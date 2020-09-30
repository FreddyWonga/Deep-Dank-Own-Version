//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine;

//public class Inventory : MonoBehaviour
//{
//    public BinarySearchTree items;

//    public void TraverseItems(BinaryNode parent)
//    {
//        if(parent != null)
//        {
//            TraverseItems(parent.left);
//            BinaryNode_Item item = parent as BinaryNode_Item;
//            if(item != null)
//            {
//                Debug.Log(item.data.itemName + " ");
//            }
//            TraverseItems(parent.right);
//        }
//    }
//}
