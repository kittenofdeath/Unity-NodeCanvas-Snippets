#if UNITY_EDITOR
using NodeCanvas.Editor;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Blackboard))]
public class BlackboardInspector : Editor
{
    private string playerPrefsSaveKey = "";
    private Blackboard blackboard
    {
        get { return (Blackboard)target; }
    }
    public override void OnInspectorGUI()
    {
        BlackboardEditor.ShowVariables(blackboard, blackboard);
        EditorUtils.EndOfInspector();
        if (Application.isPlaying || Event.current.isMouse)
        {
            Repaint();
        }
        
        playerPrefsSaveKey = EditorGUILayout.TextField("Player Prefs Save Key", playerPrefsSaveKey);

        DrawSaveButton(blackboard);
        DrawLoadButton(blackboard);
    }

    private void DrawSaveButton(Blackboard blackboard)
    {
        if (GUILayout.Button("Save BlackBoard"))
        {
            if (playerPrefsSaveKey != "")
            {
                blackboard.Save(playerPrefsSaveKey);
                Debug.Log("Tried saving blackboard data from key: " + playerPrefsSaveKey);
            }
            else
            {
                Debug.LogError("No playerPrefsSaveKey");
            }
        }
    }
    private void DrawLoadButton(Blackboard blackboard)
    {
        if (GUILayout.Button("Load BlackBoard"))
        {
            if (playerPrefsSaveKey != "")
            {
                if (blackboard.Load(playerPrefsSaveKey))
                {
                    Debug.Log("Success at loading blackboard data from key: " + playerPrefsSaveKey);
                }
                else
                {
                    Debug.LogError("No data for that key" + playerPrefsSaveKey);
                }
            }
            else
            {
                Debug.LogError("playerPrefsSaveKey is empty");
            }
        }
    }
}
#endif
