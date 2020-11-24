using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB.BehaviorTree
{
    /// <summary>
    /// Selector node. Checks child node, if it fails it gos to the next one to check if that one succeeds. Returns success if a child returns success.
    /// </summary>
    public class Selector : Node
    {
        /// <summary>
        /// The child nodes that this selector contains
        /// </summary>
        public List<Node> childNodes = new List<Node>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="childNodes">The nodes that need to be in this selector</param>
        public Selector(List<Node> childNodes)
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
                        nodeState = NodeState.success;
                        return nodeState;
                    case NodeState.failure:
                        // Nothing, evaluate next child
                        break;
                    default:
                        break;
                }
            }
            // All child nodes where a failure
            nodeState = NodeState.failure;
            return nodeState;
        }
    }
}
