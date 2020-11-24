using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAB.BehaviorTree
{
    public class NodeGoToTransform : Node
    {
        private float minDistanceToTarget;
        private Transform target;
        private NavMeshAgent navMeshAgent;

        public NodeGoToTransform(float minDistanceToTarget, Transform target, NavMeshAgent navMeshAgent)
        {
            this.minDistanceToTarget = minDistanceToTarget;
            this.target = target;
            this.navMeshAgent = navMeshAgent;
        }

        public override NodeState Run()
        {
            // Go towords defined target
            // Check if agent is close engouh
            if(Vector3.Distance(navMeshAgent.transform.position, target.position) <= minDistanceToTarget)
            {
                return NodeState.success;
            }
            navMeshAgent.SetDestination(target.position);
            return NodeState.running;
        }
    }
}
