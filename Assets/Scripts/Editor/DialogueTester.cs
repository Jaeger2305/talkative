using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueManager))]
public class DialogueTester : Editor
{
	[SerializeField]
	private string speakerName;
	[SerializeField]
	private string dialogueText;

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();

		EditorGUILayout.Space();

		this.speakerName = EditorGUILayout.TextField("Speaker", this.speakerName);
		EditorGUILayout.PrefixLabel("Dialogue");
		this.dialogueText = EditorGUILayout.TextArea(this.dialogueText, GUILayout.Height(EditorGUIUtility.singleLineHeight * 3));

		using (new EditorGUI.DisabledScope(!Application.isPlaying))
		{
			if (GUILayout.Button("Open Dialoge"))
			{
				(this.target as DialogueManager).ShowText(this.dialogueText, this.speakerName);
			}
			using (new EditorGUI.DisabledScope(!(this.target as DialogueManager).IsDialogueOpen))
			{
				if (GUILayout.Button("Close Dialoge"))
				{
					(this.target as DialogueManager).FinishCurrentDilogue();
				}
			}
		}
	}
}
