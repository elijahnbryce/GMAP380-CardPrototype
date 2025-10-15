using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(FieldZone), true)]
public class FieldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FieldZone field = (FieldZone)target;

        DrawDefaultInspector();

        if (GUI.changed)
            EditorUtility.SetDirty(field);

        if (Application.isPlaying)
            Repaint();
    }
}
