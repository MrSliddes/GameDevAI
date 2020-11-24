using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TAB.FOV
{
    /// <summary>
    /// Editor script, only used in Unity Editor. Displays FieldOfView values.
    /// </summary>
    [CustomEditor(typeof(FieldOfView))]
    public class EditorFieldOfView : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            // Get the FieldOfView script from selected object
            FieldOfView fov = (FieldOfView)target;

            // Draw view radius
            Handles.color = Color.blue;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

            // Draw view angle
            Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
            Vector3 viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

            // Draw a line to visable targets
            Handles.color = Color.red;
            foreach(Transform visibleTarget in fov.visibleTargets)
            {
                Handles.DrawLine(fov.transform.position, visibleTarget.position);
            }
        }
    }
}
