using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;



public enum AIState { none, wander, chase, attack }

public class AI_StateManager : MonoBehaviour
{
    [Header("Navigation Properties")]
    public float viewRadius = 10f;
    [Header("Object References")]
    public TextMesh sceneText;
    [Header("Behaviour Definitions")]
    public AIState initialState = AIState.wander;
    public Wander wanderState;
    public Chase chaseState;
    public Attack attackState;
    public Animator Anim;
    public GameObject Sword;
    public bool canAttack = true;

    public string state;
    public string targetName;

    private BehaviourState currentState;



    public NavMeshAgent Agent
    {
        get; private set;
    }
    public Transform Target
    {
        get; private set;
    }


    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
        canAttack = true;

    }

    // Start is called before the first frame update
    private void Start()
    {
        if (initialState == AIState.wander)
        {
            SetState(new Wander(this) { ignoreState = wanderState.ignoreState, boundBox = wanderState.boundBox });
        }
    }

    // Update is called once per frame
    void Update()
    {
        state = currentState.GetType().ToString();
        if (Target == null)
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position, viewRadius);
            foreach (Collider collider in collisions)
            {
                if (collider.CompareTag("Player") == true)
                {
                    // Check angle and visibility
                    // Check current state
                    // Change to chase state
                    Target = collider.transform;
                    SetState(new Chase(this, currentState));
                }
            }
        }
        else 
        {
            targetName = Target.name;
        }

        if (currentState != null)
        {
            currentState.Update();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        if (currentState != null)
        {
            currentState.DrawGizmos();
        }
        else if ((initialState == AIState.wander))
        {
            wanderState.DrawGizmos();
        }
    }

    public void SetState(BehaviourState newState)
    {
        if (newState.ignoreState == false)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = newState;
            if (sceneText != null)
            {
                sceneText.text = (currentState.GetType().ToString());
            }
            currentState.Initalize();
        }
    }
    public void ClearTarget()
    {
        Target = null;
    }

    public void EnableSword()
    {
        Collider[] collisions = Physics.OverlapBox((Vector3)transform.position, new Vector3(4, 5, 4));
        foreach (Collider collider in collisions)
        {
            Debug.Log(collider.tag);
            if (collider.CompareTag("Player") == true)
            {
                print("OUCH");
                StatTracker tracker = GameObject.Find("Stat Tracker").GetComponent<StatTracker>();
                if (tracker.health >= 0)
                {
                    tracker.health -= 1;
                    if (tracker.health <= 0)
                    {
                        SceneManager.LoadScene("DeathScreen");
                    }
                }
            }
        }
    }

    public void DisableSword()
    {
        canAttack = true;
    }
}

[System.Serializable]
public abstract class BehaviourState
{
    public bool ignoreState = false;

    protected AI_StateManager stateManager;
    protected BehaviourState previousState;

    public BehaviourState(AI_StateManager sm)
    {
        stateManager = sm;
    }

    public virtual void Initalize() { }
    public abstract void Update();
    public virtual void Exit() { }

    public virtual void DrawGizmos() { }
}
[System.Serializable]
public class Wander : BehaviourState
{
    public Bounds boundBox;

    private Vector3? targetPos;
    private float distance;

    public Wander(AI_StateManager sm) : base(sm)
    {

    }

    public override void Initalize()
    {
        stateManager.Agent.isStopped = false;
        //FindNewWanderPoint();
    }

    public override void Update()
    {
        if (targetPos != null)
        {
            // If AI has a target then switch states
            //distance = Vector3.Distance(stateManager.transform.position, (Vector3)targetPos);
            //if (distance <= stateManager.Agent.stoppingDistance)
            //{
            //    FindNewWanderPoint();
            //}
        }
    }

    public override void Exit()
    {

    }

    public override void DrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
        Gizmos.color = Color.red;
        if (targetPos != null)
        {
            Gizmos.DrawSphere((Vector3)targetPos, 0.5f);

        }
        targetPos = stateManager.transform.position;
        Gizmos.DrawCube((Vector3)stateManager.transform.position, new Vector3(4, 5, 4));
    }

    //Vector3 GetRandomPointInBounds()
    //{
        //float randomX = Random.Range(-boundBox.extents.x, boundBox.extents.x);
        //float randomZ = Random.Range(-boundBox.extents.z, boundBox.extents.z);
        //Vector3 randomVector = new Vector3(randomX, stateManager.transform.position.y + boundBox.center.y, randomZ);
        //if(boundBox.Contains(randomVector) == false)
        //{
        //    randomVector = GetRandomPointInBounds();
        //}
        //return randomVector;
    //}

    //void FindNewWanderPoint()
    //{
    //    targetPos = GetRandomPointInBounds();
    //    stateManager.Agent.SetDestination((Vector3)targetPos);
    //}
}

[System.Serializable]
public class Chase : BehaviourState
{
    public float chaseSpeed = 5f;
    public Animator movement;
    private float distance;

    public Chase(AI_StateManager sm) : base(sm)
    {

    }
    public Chase(AI_StateManager sm, BehaviourState previous) : base(sm)
    {
        previousState = previous;
    }

    public override void Initalize()
    {
        movement = stateManager.GetComponent<Animator>();
        stateManager.Agent.isStopped = false;
        stateManager.Agent.speed = chaseSpeed;
    }

    public override void Update()
    {
        if (stateManager.Target != null)
        {
            movement.SetBool("moving", true);
            distance = Vector3.Distance(stateManager.transform.position, stateManager.Target.position);
            if (distance <= stateManager.Agent.stoppingDistance)
            {
                stateManager.SetState(new Attack(stateManager, this));
                movement.SetBool("moving", false);
            }
            else
            {
                if (distance > stateManager.viewRadius)
                {
                    stateManager.ClearTarget();
                    stateManager.SetState(previousState);
                }
                else
                {
                    stateManager.Agent.SetDestination(stateManager.Target.position);
                }

            }
        }
        else
        {
            if (previousState != null)
            {
                stateManager.SetState(previousState);
                movement.SetBool("moving", false);
            }
        }
    }
}

[System.Serializable]
public class Attack : BehaviourState
{
    public Bounds boundBox;

    private Vector3? targetPos;
    private float distance;
    public Animator stab;
    public override void Initalize()
    {
       stab= stateManager.GetComponent<Animator>();
        targetPos = stateManager.transform.position;
        
    }
    public Attack(AI_StateManager sm, BehaviourState prevState) : base(sm)
    {
        previousState = prevState;
    }

    public override void Update()
    {
        // Gizmos.color = Color.red;



        Collider[] collisions = Physics.OverlapBox((Vector3)targetPos, new Vector3(4, 5, 4));
        foreach (Collider collider in collisions)
        {
            Debug.Log(collider.tag);
            if (collider.CompareTag("Player") == true)
            {
                if (stateManager.canAttack)
                {
                    stab.SetTrigger("Attack");
                   
                    stateManager.SetState(previousState);
                    stateManager.canAttack = false;

                    //collider.GetComponent<playerscript>().damagevoid(1`00090000);

                }


            }
           
        }
    }

   

    public override void Exit()
    {

    }
}
