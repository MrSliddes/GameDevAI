using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAB.BehaviorTree
{
    /// <summary>
    /// Behaviour Tree Node Behaviour, chases a target
    /// </summary>
    public class NodeChase : Node
    {
        /// <summary>
        /// The minimum distance the navMeshAgent needs to be to the target before stopping
        /// </summary>
        private float minDistanceToTarget;
        /// <summary>
        /// The target transform the navMeshAgent moves towords
        /// </summary>
        private Transform target;
        /// <summary>
        /// The agent that will be moving towords target
        /// </summary>
        private NavMeshAgent navMeshAgent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="minDistanceToTarget">The minimum distance the navMeshAgent needs to be to the target before stopping</param>
        /// <param name="target">The target transform the navMeshAgent moves towords</param>
        /// <param name="navMeshAgent">The agent that will be moving towords target</param>
        public NodeChase(float minDistanceToTarget, Transform target, NavMeshAgent navMeshAgent)
        {
            this.minDistanceToTarget = minDistanceToTarget;
            this.target = target;
            this.navMeshAgent = navMeshAgent;
        }

        public override NodeState Run()
        {
            if(navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.LogWarning("NavMeshAgent cannot reach path");
                return NodeState.failure;
            }

            float distance = Vector3.Distance(target.position, navMeshAgent.transform.position);
            if(distance > minDistanceToTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.position);
                return NodeState.running;
            }
            else
            {
                // Close enough, stop
                navMeshAgent.isStopped = true;
                return NodeState.success;
            }            
        }
    }
}