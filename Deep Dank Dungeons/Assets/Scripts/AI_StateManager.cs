using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Schema;
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
    private BehaviourState currentState;

    public float waitStartTime;
    public float waitCooldown;



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
        Anim = GetComponent<Animator>();
        canAttack = true;
    }


    private void Start()
    {
        attackState.Prepare(this);
        if (initialState == AIState.wander)
        {
            SetState(new Idle(this) { ignoreState = wanderState.ignoreState});
        }
    }

    public bool ReadyAttack = true;

    //Triggers the attack animation and starts the attack Coroutine
    public void DoAttack()
    {
        Anim.SetTrigger("Attack");
        StartCoroutine(AttackCheck());
    }

    //Waits for attack animation then checks if the player is in range still
    //If player is still in range then subtract from the players health
    //Puts the enemy into a cooldown state before it can attack again and sets the state to chase
    IEnumerator AttackCheck()
    {
        yield return new WaitForSeconds(waitStartTime);
        if (attackState.Distance < attackState.range)
        {
            StatTracker.Instance.health = (StatTracker.Instance.health-1);
        }
        SetState(new Chase(this));
        yield return new WaitForSeconds(waitCooldown);
        ReadyAttack = true;
    }

    void Update()
    {
        //Checks if the player is in the enemies agro range
        state = currentState.GetType().ToString();
        if (Target == null)
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position, viewRadius);
            foreach (Collider collider in collisions)
            {
                if (collider.CompareTag("Player") == true)
                {
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

    //Draws the sphere showing the enemies agro range
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

    //Sets enemies state
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

    public virtual void Prepare(AI_StateManager sm) 
    {
        stateManager = sm;
    }

    public virtual void Initalize() { }
    public abstract void Update();
    public virtual void Exit() { }

    public virtual void DrawGizmos() { }
}
[System.Serializable]

// Idle state just keeps the enemy still and waiting for a player to enter agro range
public class Idle : BehaviourState
{

    public Idle(AI_StateManager sm) : base(sm)
    {

    }

    public override void Initalize()
    {
        stateManager.Agent.isStopped = false;
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

    public float distance;


    public GameObject enemy;

    public Chase(AI_StateManager sm) : base(sm)
    {

    }
    public Chase(AI_StateManager sm, BehaviourState previous) : base(sm)
    {
        previousState = previous;
    }

    //Sets the default settings such as speed and animations for the chase state
    public override void Initalize()
    {
        movement = stateManager.GetComponent<Animator>();
        stateManager.Agent.isStopped = false;
        stateManager.Agent.speed = chaseSpeed;
    }

    public override void Update()
    {
        //If the enemy is targeting the player, check to see if the player is within chasing range, if not then revert to idle state
        if (stateManager.Target != null)
        {
            movement.SetBool("moving", true);
            distance = Vector3.Distance(stateManager.transform.position, PlayerController.instance.transform.position);
            if (distance <= stateManager.attackState.range)
            {
                stateManager.SetState(stateManager.attackState);
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
    public Animator stab;

    public float Distance 
    {
        get
        {
            return Vector3.Distance(stateManager.transform.position, PlayerController.instance.transform.position);
        }
    }

    public override void Initalize()
    {
       stab = stateManager.GetComponent<Animator>();
        if(stateManager.Target == null) 
        {
            stateManager.SetState(new Idle(stateManager));
        }
    }

    public Attack(AI_StateManager sm, BehaviourState prevState) : base(sm)
    {
        previousState = prevState;
    }

    //If the enemy is targeting the player and the player is within attack range, run the DoAttack function, if not continue to chase the player
    public override void Update()
    {
        if (stateManager.Target != null) 
        {
            if(Distance <= range) 
            {
                if(stateManager.ReadyAttack == true)
                {
                    stateManager.ReadyAttack = false;
                    stateManager.DoAttack();
                }
            }
            else 
            {
                stateManager.SetState(new Chase(stateManager));
            }
        }
        else 
        {
            stateManager.SetState(new Chase(stateManager));
        }
    }

    public override void Exit()
    {

    }
}
