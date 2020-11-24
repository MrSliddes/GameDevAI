using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB.BehaviorTree
{
    /// <summary>
    /// The basic node for a behavior system
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Get the current node state (running, success or failure)
        /// </summary>
        public NodeState NodeState { get { return nodeState; } }

        /// <summary>
        /// The current state of the node, it is protected as you cannot directly modify the state
        /// </summary>
        protected NodeState nodeState;

        /// <summary>
        /// The workings of the node
        /// </summary>
        /// <returns>NodeState containing running, success or failing</returns>
        public abstract NodeState Run();
    }

    /// <summary>
    /// The states a node can have
    /// </summary>
    public enum NodeState
    {
        running,
        success,
        failure
    }    
}