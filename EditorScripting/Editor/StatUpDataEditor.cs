using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(StatsUpData))]
public class StatUpDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        StatsUpData data = (StatsUpData)target;
        base.OnInspectorGUI();
        // after
        if(GUILayout.Button("Reset Prices"))
        {
            data.ResetPrices();
        }

    }
}
