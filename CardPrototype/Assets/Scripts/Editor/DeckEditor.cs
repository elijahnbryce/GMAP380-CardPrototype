using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Deck), true)]
public class DeckEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Deck deck = (Deck)target;

        DrawDefaultInspector();


        // EditorGUILayout.LabelField("Size", deck.Cards?.Count.ToString() ?? "0");
        // EditorGUILayout.LabelField("Items", EditorStyles.boldLabel);


        // for (int i = 0; i < deck.Cards.Count; i++)
        // {
        //     deck.Cards[i] = (Card)EditorGUILayout.ObjectField($"Item {i}", deck.Cards[i], typeof(Card), false);
        // }

        if (GUI.changed)
            EditorUtility.SetDirty(deck);

        if (Application.isPlaying)
            Repaint();
    }
}
