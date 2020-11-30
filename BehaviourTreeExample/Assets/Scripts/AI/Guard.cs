using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using TAB.BehaviorTree;
using TAB.FOV;
using TAB.VariableTypes;

public class Guard : MonoBehaviour
{
    public Transform[] patrolPoints;

    private Selector tree;
    private NavMeshAgent agent;
    private Animator animator;

    private VariableTransform target;
    private VariableBool hasWeapon;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();        
    }

    private void Start()
    {
        target = (VariableTransform)ScriptableObject.CreateInstance("VariableTransform");
        hasWeapon = (VariableBool)ScriptableObject.CreateInstance("VariableBool");
        hasWeapon.Value = false;

        //Create your Behaviour Tree here!
        #region patrolling
        // Create target that can be changed by other nodes
        NodePatrol nodePatrol = new NodePatrol(0.5f, patrolPoints, agent);
        NodeSeeTarget nodeSeeTarget = new NodeSeeTarget(GetComponent<FieldOfView>(), agent, transform, target);
        Invertor invertorSeeTarget = new Invertor(nodeSeeTarget);
        NodeHasTarget nodeHasTarget = new NodeHasTarget(target);
        Invertor invertorNodeHasTarget = new Invertor(nodeHasTarget);
        // Patrol group
        Sequence sequencePatrolling = new Sequence(new List<Node> { invertorNodeHasTarget, nodePatrol, invertorSeeTarget });
        #endregion

        #region chasing
        NodeCheckBool nodeHasWeapon = new NodeCheckBool(hasWeapon);
        NodeGoToTransform nodeGoToTransform = new NodeGoToTransform(1f, GameObject.FindWithTag("Weapon").transform, agent);
        NodeGetWeapon nodeGetWeapon = new NodeGetWeapon(1f, GameObject.FindWithTag("Weapon").transform, agent, hasWeapon);
        NodeChase nodeChase = new NodeChase(1f, 5, target, agent, 5f);
        NodeAttack nodeAttack = new NodeAttack(1f, agent, target);

        Sequence sequenceC1 = new Sequence(new List<Node> { nodeGoToTransform, nodeGetWeapon});
        Selector selectorC1 = new Selector(new List<Node> { nodeHasWeapon, sequenceC1});

        Sequence sequenceChasing = new Sequence(new List<Node> { selectorC1, nodeChase, nodeHasTarget, nodeAttack });
        
        #endregion


        tree = new Selector(new List<Node> { sequencePatrolling, sequenceChasing });

        // Show NodeState in editor
        if(Application.isEditor)
        {
            gameObject.AddComponent<ShowNodeTreeStatus>().AddConstructor(transform, tree);
        }
    }

    private void FixedUpdate()
    {
        tree?.Run();
        print(target.Value);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Handles.color = Color.yellow;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

    //    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
