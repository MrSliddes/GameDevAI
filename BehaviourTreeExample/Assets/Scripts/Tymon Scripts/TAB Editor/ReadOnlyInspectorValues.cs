using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html

namespace TAB.Editor
{
    public class ShowOnlyAttribute : PropertyAttribute
    {

    }

    [CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        /// <summary>
        /// Override the GUI
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}