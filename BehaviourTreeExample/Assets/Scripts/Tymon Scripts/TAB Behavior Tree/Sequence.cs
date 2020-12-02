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
        public Node[] childNodes;// = new List<Node>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="childNodes">The nodes that need to be in this sequene</param>
        public Sequence(params Node[] childNodes)
        {
            this.childNodes = childNodes;
        }

        private int index;

        /// <summary>
        /// Iterate through all the child nodes and call the run funtion in them //index
        /// </summary>
        /// <returns></returns>
        public override NodeState Run()
        {
            for(int i = index; i < childNodes.Length; i++)
            {
                switch(childNodes[i].Run())
                {
                    case NodeState.running:
                        nodeState = NodeState.running;
                        index = i;
                        return nodeState;
                    case NodeState.success:
                        //index = 0;
                        continue;
                    case NodeState.failure:
                        nodeState = NodeState.failure;
                        index = 0;
                        return nodeState;
                    default:
                        break;
                }
            }

            //foreach(var node in childNodes)
            //{
            //    switch(node.Run())
            //    {
            //        case NodeState.running:
            //            nodeState = NodeState.running;
            //            return nodeState;
            //        case NodeState.success:
            //            continue;
            //        case NodeState.failure:
            //            nodeState = NodeState.failure;
            //            return nodeState;
            //        default:
            //            break;
            //    }
            //}
            // Check if sequence is success
            index = 0;
            nodeState = NodeState.success;
            return nodeState;
        }
    }
}
