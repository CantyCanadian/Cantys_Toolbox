﻿///====================================================================================================
///
///     ReadOnlyAttribute by
///     - CantyCanadian
///
///====================================================================================================

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace Canty
{
    /// <summary>
    /// Display a serialized property in Unity's editor window in a non-modifiable way.
    /// </summary>
    public class ReadOnlyAttribute : PropertyAttribute { }
    
#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

#endif
}