using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAB.BehaviorTree
{
    public class NodeDistanceToTarget : Node
    {
        private float minDistance;
        private Transform target;
        private NavMeshAgent navMeshAgent;

        public NodeDistanceToTarget(float minDistance, Transform target, NavMeshAgent navMeshAgent)
        {
            this.minDistance = minDistance;
            this.target = target;
            this.navMeshAgent = navMeshAgent;
        }

        public override NodeState Run()
        {
            if(Vector3.Distance(navMeshAgent.transform.position, target.position) <= minDistance)
            {
                return NodeState.success;
            }
            return NodeState.failure;
        }
    }
}