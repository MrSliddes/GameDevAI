using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAB.BehaviorTree;
using TAB.VariableTypes;
using UnityEngine.AI;

public class NodeGetWeapon : Node
{
    private float minDistance;
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private VariableBool gotWeapon;

    public NodeGetWeapon(float minDistance, Transform target, NavMeshAgent navMeshAgent, VariableBool gotWeapon)
    {
        this.minDistance = minDistance;
        this.target = target;
        this.navMeshAgent = navMeshAgent;
        this.gotWeapon = gotWeapon;
    }

    public override NodeState Run()
    {
        if(Vector3.Distance(target.position, navMeshAgent.transform.position) <= minDistance)
        {
            gotWeapon.Value = true;
            nodeState = NodeState.success;
            return NodeState.success;
        }
        else
        {
            nodeState = NodeState.running;
            return NodeState.running;
        }
    }
}
