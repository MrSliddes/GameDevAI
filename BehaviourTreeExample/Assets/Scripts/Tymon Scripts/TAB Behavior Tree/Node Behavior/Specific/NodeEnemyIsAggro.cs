using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAB.BehaviorTree;
using TAB.VariableTypes;

public class NodeEnemyIsAggro : Node
{
    private float range;
    private LayerMask enemyLayer;
    private Transform origin;

    public NodeEnemyIsAggro(float range, LayerMask enemyLayer, Transform origin)
    {
        this.range = range;
        this.enemyLayer = enemyLayer;
        this.origin = origin;
    }

    public override NodeState Run()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(origin.position, range, enemyLayer);
        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            if(targetsInViewRadius[i].GetComponent<Guard>() != null)
            {
                // Check if guard has target
                if(targetsInViewRadius[i].GetComponent<Guard>().target.Value != null)
                {
                    // has target;
                    nodeState = NodeState.success;
                    Debug.Log("Enemy is aggroed");
                    return nodeState;
                }
            }
        }
        Debug.Log("NOOOOOOOO AGRO");
        nodeState = NodeState.failure;
        return nodeState;
    }
}
