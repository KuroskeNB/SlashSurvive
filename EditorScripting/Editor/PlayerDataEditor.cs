using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerData),true)]
[CanEditMultipleObjects]
public class PlayerDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Player Data Editor".ToUpper());
        // before
        base.OnInspectorGUI();
        // after
         PlayerData data = (PlayerData)target;
         if(GUILayout.Button("Reset Data"))
        {
            data.RebuildData();
        }
        
    }
}
