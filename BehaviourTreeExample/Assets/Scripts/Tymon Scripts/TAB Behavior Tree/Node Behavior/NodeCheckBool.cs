using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAB.BehaviorTree;
using TAB.VariableTypes;

public class NodeCheckBool : Node
{
    private VariableBool value;

    public NodeCheckBool(VariableBool value)
    {
        this.value = value;
    }

    public override NodeState Run()
    {
        if(value.Value == true)
        {
            nodeState = NodeState.success;
            return nodeState;
        }
        else
        {
            nodeState = NodeState.failure;
            return nodeState;
        }
    }
}
