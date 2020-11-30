using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using TAB.BehaviorTree;
using TAB.VariableTypes;

public class Rogue : MonoBehaviour
{
    private Selector tree;
    private NavMeshAgent agent;
    private Animator animator;

    private Node topNode;

    private VariableTransform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //TODO: Create your Behaviour tree here


        print(GameObject.FindWithTag("Player").transform);
        target = (VariableTransform)ScriptableObject.CreateInstance("VariableTransform"); //(VariableType<Transform>)ScriptableObject.CreateInstance(VariableType<Transform>);
        print(target);
        target.Value = GameObject.FindWithTag("Player").transform;
        NodeChase nodeChase = new NodeChase(2, 5, target, agent, 1f);

        tree = new Selector(new List<Node> { nodeChase });

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
