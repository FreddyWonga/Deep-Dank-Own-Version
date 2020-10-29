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
    public Idle wanderState;
    public Chase chaseState;
    public Attack attackState;
    public Animator Anim;
    public GameObject Sword;
    public bool canAttack = true;

    public string state;
    public string targetName;

    public TextMesh debugText;

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
            SetState(new Idle(this) { ignoreState = wanderState.ignoreState});
        }
    }

    public bool ReadyAttack;

    public void DoAttack()
    {
        StartCoroutine(AttackCheck());
    }

    IEnumerator AttackCheck()
    {
        Anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1);
        if (attackState.distance < attackState.range)
        {
            Debug.Log("Damage Done");
            StatTracker.Instance.health = -1;
        }
        SetState(new Chase(this));
        ReadyAttack = true;
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
            debugText.text = currentState.ToString().ToUpper();
        }
    }
    public void ClearTarget()
    {
        Target = null;
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
public class Idle : BehaviourState
{

    public Idle(AI_StateManager sm) : base(sm)
    {

    }

    public override void Initalize()
    {
        stateManager.Agent.isStopped = false;
        //FindNewWanderPoint();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }

    public override void DrawGizmos()
    {

    }
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
            if (distance <= stateManager.attackState.range)
            {
                stateManager.SetState(new Attack(stateManager, this));
                movement.SetBool("moving", false);
            }
            else
            {
                if (distance > stateManager.viewRadius)
                {
                    stateManager.ClearTarget();
                    stateManager.SetState(new Idle(stateManager));
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
                stateManager.SetState(new Idle(stateManager));
                movement.SetBool("moving", false);
            }
        }
    }
}

[System.Serializable]
public class Attack : BehaviourState
{
    public float range = 3;
    public LayerMask ignoreMask;
    public Animator Anim;
    public float distance;
    public Animator stab;
    public override void Initalize()
    {
       stab= stateManager.GetComponent<Animator>();
        if(stateManager.Target == null) 
        {
            stateManager.SetState(new Idle(stateManager));
        }
    }

    public Attack(AI_StateManager sm, BehaviourState prevState) : base(sm)
    {
        previousState = prevState;
    }

    public override void Update()
    {
        // Gizmos.color = Color.red;

        if(stateManager.Target != null) 
        {
            
            //Debug.Log($"Distance: {distance} Range: {range}");
            if(distance <= range) 
            {
                if(stateManager.ReadyAttack == true)
                {
                    stateManager.ReadyAttack = false;
                    stateManager.DoAttack();
                }
            }
            else 
            {
                Debug.Log("Not within range");
                stateManager.SetState(new Chase(stateManager));
            }
        }
        else 
        {
            Debug.Log("Target lost");
            stateManager.SetState(new Chase(stateManager));
        }
    }

    public override void Exit()
    {

    }
}
