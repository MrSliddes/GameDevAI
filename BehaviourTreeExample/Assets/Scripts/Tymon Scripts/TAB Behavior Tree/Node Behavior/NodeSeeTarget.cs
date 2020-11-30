using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TAB.FOV;
using TAB.VariableTypes;

namespace TAB.BehaviorTree
{
    public class NodeSeeTarget : Node
    {
        private FieldOfView fov;
        private NavMeshAgent navMeshAgent;
        private Transform orgin;
        private VariableTransform target;

        public NodeSeeTarget(FieldOfView fov)
        {
            this.fov = fov;
        }

        public NodeSeeTarget(FieldOfView fov, NavMeshAgent navMeshAgent, Transform orgin, VariableTransform target)
        {
            this.fov = fov;
            this.navMeshAgent = navMeshAgent;
            this.orgin = orgin;
            this.target = target;
        }

        public override NodeState Run()
        {
            if(fov.visibleTargets.Count <= 0)
            {
                Debug.Log("f");
                nodeState = NodeState.failure;
                return nodeState;
            }
            else
            {
                Debug.Log("s");
                target.Value = fov.GetClosestTarget(orgin); // maak object van, zie veriableFloat
                //navMeshAgent.SetDestination(target.position);
                nodeState = NodeState.success;
                return nodeState;
            }
        }
    }
}