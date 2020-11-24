using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TAB.BehaviorTree;

/// <summary>
/// Creates a window where a behavior tree of an object can be edited
/// </summary>
public class EditorBehaviorTree : EditorWindow
{
    private Object tree;

    [MenuItem("Tools/TAB/Editor Behavior Tree")]
    public static void CreateWindow()
    {
        EditorWindow w = EditorWindow.GetWindow<EditorBehaviorTree>();
        w.titleContent = new GUIContent("Behavior Tree Editor");
        w.Show();
    }

    public void OnGUI()
    {
        tree = EditorGUILayout.ObjectField(tree, typeof(Node), true);
    }

    /* Todo
     select object in scene,
    edit tree with nodes
    save object
     
     */
}
