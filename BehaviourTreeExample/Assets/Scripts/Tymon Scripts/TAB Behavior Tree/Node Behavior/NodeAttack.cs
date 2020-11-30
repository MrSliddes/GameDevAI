using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TAB.VariableTypes;

namespace TAB.BehaviorTree
{
    public class NodeAttack : Node
    {
        private float attackRange;
        private NavMeshAgent navMeshAgent;
        private VariableTransform target;

        public NodeAttack(float attackRange, NavMeshAgent navMeshAgent, VariableTransform target)
        {
            this.attackRange = attackRange;
            this.navMeshAgent = navMeshAgent;
            this.target = target;
        }

        public override NodeState Run()
        {
            // Check for damageable object
            Collider[] targetsInViewRadius = Physics.OverlapSphere(navMeshAgent.transform.position, attackRange);
            if(targetsInViewRadius.Length == 0) return NodeState.failure;

            // Get those that have IDamagable
            List<Transform> targets = new List<Transform>();
            foreach(Collider item in targetsInViewRadius)
            {
                if(item.GetComponent<IDamageable>() != null) targets.Add(item.transform);
            }
            if(targets.Count == 0) return NodeState.failure;

            // Get the closest
            Transform closest = targets[0];
            float distance = Vector3.Distance(closest.position, navMeshAgent.transform.position);
            for(int i = 1; i < targets.Count; i++)
            {
                if(Vector3.Distance(navMeshAgent.transform.position, targets[i].position) < distance)
                {
                    closest = targets[i];
                    distance = Vector3.Distance(navMeshAgent.transform.position, targets[i].position);
                }
            }

            // Attack closest
            Debug.Log("Attack");
            target.Value = null;
            closest.GetComponent<IDamageable>().TakeDamage(navMeshAgent.gameObject, 1);
            return NodeState.success;
        }
    }
}