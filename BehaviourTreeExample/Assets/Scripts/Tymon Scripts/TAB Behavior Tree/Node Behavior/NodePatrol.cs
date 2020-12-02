using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAB.BehaviorTree
{
    public class NodePatrol : Node
    {
        /// <summary>
        /// The range the agent needs to be in to trigger the next patrolpoint. Minimum: 0.1f.
        /// </summary>
        private float patrolPointRange;
        /// <summary>
        /// The points the agent follows in order
        /// </summary>
        private Transform[] patrolPoints;
        /// <summary>
        /// The agent patrolling
        /// </summary>
        private NavMeshAgent navMeshAgent;

        private int currentPatrolPoint = 0;

        /// <summary>
        /// Contstructor
        /// </summary>
        /// <param name="patrolPoints"></param>
        /// <param name="navMeshAgent"></param>
        public NodePatrol(float patrolPointRange, Transform[] patrolPoints, NavMeshAgent navMeshAgent)
        {
            this.patrolPointRange = Mathf.Clamp(patrolPointRange, 0.1f, Mathf.Infinity);
            this.patrolPoints = patrolPoints;
            this.navMeshAgent = navMeshAgent;
        }

        public override NodeState Run()
        {            
            // Move agent to current patrol point
            navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].position);
            navMeshAgent.isStopped = false;
            navMeshAgent.stoppingDistance = 0;
            // Check if agent reached point
            if(Vector3.Distance(navMeshAgent.transform.position, patrolPoints[currentPatrolPoint].position) <= patrolPointRange)
            {
                // Next point
                currentPatrolPoint++;
                currentPatrolPoint = currentPatrolPoint == patrolPoints.Length ? currentPatrolPoint = 0 : currentPatrolPoint; // If current patrol point is the last, loop back to 0, else just keep the currentPatrolPoint
            }
            nodeState = NodeState.success;
            return nodeState;
        }
    }
}