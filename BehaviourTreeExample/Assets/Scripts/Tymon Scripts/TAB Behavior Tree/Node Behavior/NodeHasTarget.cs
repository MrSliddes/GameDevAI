using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAB.BehaviorTree;
using TAB.VariableTypes;

public class NodeHasTarget : Node
{
    private VariableTransform target;

    public NodeHasTarget(VariableTransform target)
    {
        this.target = target;
    }

    public override NodeState Run()
    {
        if(target.Value == null || target.Value.gameObject.activeSelf == false)
        {
            nodeState = NodeState.failure;
        }
        else
        {
            nodeState = NodeState.success;
        }
        return nodeState;
    }
}
