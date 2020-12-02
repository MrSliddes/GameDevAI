using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using TAB.BehaviorTree;
using TAB.VariableTypes;

public class Rogue : MonoBehaviour
{
    public LayerMask enemyLayerMask;

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
        target = (VariableTransform)ScriptableObject.CreateInstance("VariableTransform"); //(VariableType<Transform>)ScriptableObject.CreateInstance(VariableType<Transform>);
        target.Value = GameObject.FindWithTag("Player").transform;

        NodeEnemyIsAggro nodeEnemyIsAggro = new NodeEnemyIsAggro(10, enemyLayerMask, transform);
        //Invertor invertorNodeEnemyIsAggro = new Invertor(nodeEnemyIsAggro);
        NodeChase nodeChase = new NodeChase(2, 5, target, agent, 5f, true);

        Sequence sequenceFollow = new Sequence(nodeChase );

        NodeGoToTransform nodeGoToTransform = new NodeGoToTransform(0.5f, GameObject.FindWithTag("HidingSpot").transform, agent);
        NodeTrowSmokeAtEnemy nodeTrowSmokeAtEnemy = new NodeTrowSmokeAtEnemy(1f, enemyLayerMask, 10, transform);
        //Invertor invertorNodeTrowSmokeAtEnemy = new Invertor(nodeTrowSmokeAtEnemy);

        Sequence sequenceSmoking = new Sequence (nodeEnemyIsAggro, nodeGoToTransform, nodeTrowSmokeAtEnemy);

        tree = new Selector(new List<Node> { sequenceSmoking, sequenceFollow });

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
