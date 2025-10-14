using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(CardSlot), true)]
public class SlotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CardSlot deck = (CardSlot)target;

        DrawDefaultInspector();

        if (GUI.changed)
            EditorUtility.SetDirty(deck);

        if (Application.isPlaying)
            Repaint();
    }
}
