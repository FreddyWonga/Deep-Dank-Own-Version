using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// geeksforgeeks.org/c-program-hashing-chaining/

// dont include int main()



public class HashIDs : MonoBehaviour
{
    public int walkingBool;

    private void Awake()
    {
        walkingBool = Animator.StringToHash("move");
    }
}
