using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB.BehaviorTree
{
    /// <summary>
    /// Sequence node, meaning every node in the sequence needs to be success or it will be a failure (similar to an "and" operation)
    /// </summary>
    public class Sequence : Node
    {
        /// <summary>
        /// The nodes that this sequence contains
        /// </summary>
        public List<Node> childNodes = new List<Node>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="childNodes">The nodes that need to be in this sequene</param>
        public Sequence(List<Node> childNodes)
        {
            this.childNodes = childNodes;
        }

        /// <summary>
        /// Iterate through all the child nodes and call the run funtion in them
        /// </summary>
        /// <returns></returns>
        public override NodeState Run()
        {
            foreach(var node in childNodes)
            {
                switch(node.Run())
                {
                    case NodeState.running:
                        nodeState = NodeState.running;
                        return nodeState;
                    case NodeState.success:
                        continue;
                    case NodeState.failure:
                        nodeState = NodeState.failure;
                        return nodeState;
                    default:
                        break;
                }
            }
            // Check if sequence is success
            nodeState = NodeState.success;
            return nodeState;
        }
    }
}
