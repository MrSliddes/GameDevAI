using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB.BehaviorTree
{
    /// <summary>
    /// Invertor node, changes nodeState running to running, success to failure and failure to success.
    /// </summary>
    public class Invertor : Node
    {
        /// <summary>
        /// The nodes that this invertor needs to invert
        /// </summary>
        protected Node childNode;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="childNode">The node to be inverted</param>
        public Invertor(Node childNode)
        {
            this.childNode = childNode;
        }

        /// <summary>
        /// Invert the childNode nodeState
        /// </summary>
        /// <returns></returns>
        public override NodeState Run()
        {
            switch(childNode.Run())
            {
                case NodeState.running:
                    // Stays the same
                    nodeState = NodeState.running;
                    break;
                case NodeState.success:
                    // Invert
                    nodeState = NodeState.failure;
                    break;
                case NodeState.failure:
                    // Invert
                    nodeState = NodeState.success;
                    break;
                default:
                    break;
            }
            return nodeState;
        }
    }
}

