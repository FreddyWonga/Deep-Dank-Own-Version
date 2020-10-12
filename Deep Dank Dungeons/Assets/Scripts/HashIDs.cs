using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int walkingBool;

    private void Awake()
    {
        walkingBool = Animator.StringToHash("move");
    }
}
