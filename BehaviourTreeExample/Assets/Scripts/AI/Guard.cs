using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using TAB.BehaviorTree;
using TAB.FOV;

public class Guard : MonoBehaviour
{
    public Transform[] patrolPoints;

    private Selector tree;
    private NavMeshAgent agent;
    private Animator animator;

    private Transform target = null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();        
    }

    private void Start()
    {
        //Create your Behaviour Tree here!
        #region patrolling
        NodePatrol nodePatrol = new NodePatrol(0.5f, patrolPoints, agent);
        NodeSeeTarget nodeSeeTarget = new NodeSeeTarget(GetComponent<FieldOfView>(), target, transform);

        // Patrol group
        Sequence sequencePatrolling = new Sequence(new List<Node> { nodePatrol, nodeSeeTarget});
        #endregion

        #region chasing
        NodeChase nodeChase = new NodeChase(0.5f, 5, target, agent);

        Sequence sequenceChasing = new Sequence(new List<Node> { nodeChase });
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
