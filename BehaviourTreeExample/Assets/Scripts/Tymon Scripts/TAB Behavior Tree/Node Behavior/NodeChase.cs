using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TAB.VariableTypes;

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
        /// The maximum distance the navMeshAgent can be from the target before returning NodeState.failure
        /// </summary>
        private float maxDistanceToTarget;
        /// <summary>
        /// The target transform the navMeshAgent moves towords
        /// </summary>
        private VariableTransform target;
        /// <summary>
        /// The agent that will be moving towords target
        /// </summary>
        private NavMeshAgent navMeshAgent;
        /// <summary>
        /// The time the objects stays agroed to target
        /// </summary>
        private float agroTime;
        private float agroTimer;
        /// <summary>
        /// When to far away, will the object lose the target transform?
        /// </summary>
        private bool loseTargetTransform;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="minDistanceToTarget">The minimum distance the navMeshAgent needs to be to the target before stopping</param>
        /// <param name="target">The target transform the navMeshAgent moves towords</param>
        /// <param name="navMeshAgent">The agent that will be moving towords target</param>
        public NodeChase(float minDistanceToTarget, float maxDistanceToTarget, VariableTransform target, NavMeshAgent navMeshAgent, float agroTime, bool loseTargetTransform)
        {
            this.minDistanceToTarget = minDistanceToTarget;
            this.maxDistanceToTarget = maxDistanceToTarget;
            this.target = target;
            this.navMeshAgent = navMeshAgent;
            this.agroTime = agroTime;
            agroTimer = agroTime;
            this.loseTargetTransform = loseTargetTransform;
        }

        public override NodeState Run()
        {
            agroTimer -= Time.fixedDeltaTime; // keep in mind that the tree is run in fixedupdate
            if(agroTimer < 0) agroTimer = 0;

            if(navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.LogWarning("NavMeshAgent cannot reach path");
                nodeState = NodeState.failure;
                return nodeState;
            }
            if(target.Value == null)
            {
                nodeState = NodeState.failure;
                Debug.Log("no target");
                return nodeState;
            }            

            float distance = Vector3.Distance(target.Value.position, navMeshAgent.transform.position);
            navMeshAgent.SetDestination(target.Value.position);

            if(distance >= maxDistanceToTarget && agroTimer <= 0)
            {
                // Too far away
                Debug.Log("too far & lost agro");
                if(loseTargetTransform) target.Value = null;
                agroTimer = agroTime;
                //navMeshAgent.isStopped = false;
                nodeState = NodeState.failure;
                return nodeState;
            }

            navMeshAgent.stoppingDistance = minDistanceToTarget;
            if(distance <= minDistanceToTarget)
            {
                // Close enough, stop
                //navMeshAgent.isStopped = true;
                Debug.Log("close enough");
                nodeState = NodeState.success;
                return nodeState;                
            }
            else
            {
                // Running
                //navMeshAgent.isStopped = false;
                Debug.Log("running");
                nodeState = NodeState.running;
                return nodeState;
            }            
        }
    }
}