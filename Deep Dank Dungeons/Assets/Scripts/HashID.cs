using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashID : MonoBehaviour
{
    public int moving;
    public int Attack;

    //Hash move and cast bools
    private void Awake()
    {
        moving = Animator.StringToHash("move");
        Attack = Animator.StringToHash("cast");
    }
}
