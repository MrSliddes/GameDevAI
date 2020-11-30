using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAB.BehaviorTree;
using UnityEditor;

/// <summary>
/// Displays the current node that is running
/// </summary>
public class ShowNodeTreeStatus : MonoBehaviour
{
    /// <summary>
    /// The behavior tree tree.
    /// </summary>
    private Selector tree;
    /// <summary>
    /// What transform is using this class
    /// </summary>
    private Transform origin;

    public void AddConstructor(Transform origin, Selector tree)
    {
        this.origin = origin;
        this.tree = tree;
    }

    private void OnDrawGizmos()
    {
        string info = "";
        //Get the node with nodeStatus running
        List<Node> nodes = new List<Node>(tree.childNodes);
        for(int i = 0; i < nodes.Count; i++)
        {
            if(nodes[i].GetType().IsEquivalentTo(typeof(Sequence)))
            {
                Sequence s = (Sequence)nodes[i];
                nodes.AddRange(s.childNodes);
            }
            else if(nodes[i].GetType().IsEquivalentTo(typeof(Selector)))
            {
                Selector s = (Selector)nodes[i];
                nodes.AddRange(s.childNodes);
            }

            info += "\n" + nodes[i].GetType().Name + ": " + nodes[i].NodeState;
        }
        GUI.color = Color.black;
        Handles.Label(origin.position + Vector3.up * 4, info);
    }
}
