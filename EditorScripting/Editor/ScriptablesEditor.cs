using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableObject),true)]
[CanEditMultipleObjects]
public class ScriptablesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Fill all of the elements!!!".ToUpper());
        // before
        base.OnInspectorGUI();
        // after
        if(target is StatsUpData)
        {
         StatsUpData data = (StatsUpData)target;
         if(GUILayout.Button("Reset Prices"))
        {
            data.ResetPrices();
        }
        }
        if(target is DataSaver)
        {
            DataSaver rewdata = (DataSaver)target;
          if(GUILayout.Button("Save info"))
          {
            rewdata.Save();
          }
          if(GUILayout.Button("Load info"))
          {
            rewdata.Load();
          }
          if(GUILayout.Button("Save for build"))
          {
            rewdata.SaveForBuild();
          }
        }
    }
}
