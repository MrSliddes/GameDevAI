using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAB.FOV;

namespace TAB.BehaviorTree
{
    public class NodeSeeTarget : Node
    {
        private FieldOfView fov;
        private Transform target;
        private Transform orgin;

        public NodeSeeTarget(FieldOfView fov)
        {
            this.fov = fov;
        }

        public NodeSeeTarget(FieldOfView fov, Transform target, Transform orgin)
        {
            this.fov = fov;
            this.target = target;
            this.orgin = orgin;
        }

        public override NodeState Run()
        {
            if(fov.visibleTargets.Count <= 0)
            {
                return NodeState.failure;
            }
            else
            {
                target = fov.GetClosestTarget(orgin);
                return NodeState.success;
            }
        }
    }
}