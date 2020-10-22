using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{

    [Header("Navigation Properties")]
    public float viewRadius = 10f;
    [Header("Object References")]
    public TextMesh sceneText;
    [Header("Behaviour Definitions")]
    public AIState initialState = AIState.wander;
    public Idle wanderState;
    public Chase chaseState;
    public Attack attackState;
    public Animator Anim;
    public GameObject Sword;
    public bool canAttack = true;



    public void DrawGizmos()
    {
        //Gizmos.DrawCube(gameobject.this, new Vector3(4, 5, 4));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
