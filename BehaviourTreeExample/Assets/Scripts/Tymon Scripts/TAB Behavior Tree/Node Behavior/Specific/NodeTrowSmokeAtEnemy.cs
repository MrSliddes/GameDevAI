using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAB.BehaviorTree;
using TAB.VariableTypes;

public class NodeTrowSmokeAtEnemy : Node
{
    private float timeItTakesToSmoke;
    private float timerItTakesToSmoke;
    private LayerMask layerEnemy;
    private float range;
    private Transform origin;

    public NodeTrowSmokeAtEnemy(float timeItTakesToSmoke, LayerMask layerEnemy, float range, Transform origin)
    {
        this.timeItTakesToSmoke = timeItTakesToSmoke;        
        this.layerEnemy = layerEnemy;
        this.range = range;
        this.origin = origin;
    }

    public override NodeState Run()
    {
        timerItTakesToSmoke -= Time.fixedDeltaTime;

        if(timerItTakesToSmoke <= 0)
        {
            // Trow smoke

            Collider[] targetsInViewRadius = Physics.OverlapSphere(origin.position, 100, layerEnemy);
            targetsInViewRadius[0].GetComponent<Guard>().Smoked();
            timerItTakesToSmoke = timeItTakesToSmoke;
            nodeState = NodeState.success;
            return nodeState;
        }
        nodeState = NodeState.running;
        return nodeState;
    }
}
