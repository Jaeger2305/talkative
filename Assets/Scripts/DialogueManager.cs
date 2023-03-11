using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class DialogueManager : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI dialogueTextElement;
	[SerializeField]
	private TextMeshProUGUI speakerTextElement;
	[SerializeField]
	private GameObject speakerTextContainer;

	private TaskCompletionSource<bool> completionSource;

	public bool IsDialogueOpen => this.completionSource != null;
	private int openFrame;


	void Awake()
	{
		if (this.completionSource == null)
		{
			this.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && Time.frameCount != this.openFrame)
		{
			this.FinishCurrentDilogue(true);
		}
	}


	public Task<bool> ShowText(string dialogueText, string speakerText = null)
	{
		this.CancelPreviousDialogue();
		var speakerPresent = !string.IsNullOrEmpty(speakerText);
		this.speakerTextContainer.SetActive(speakerPresent);
		if (speakerPresent)
		{
			this.speakerTextElement.text = speakerText;
		}
		this.dialogueTextElement.text = dialogueText;
		this.gameObject.SetActive(true);
		this.openFrame = Time.frameCount;

		this.completionSource = new TaskCompletionSource<bool>();
		return this.completionSource.Task;
	}

	private void CancelPreviousDialogue()
	{
		if (this.completionSource != null)
		{
			this.gameObject.SetActive(false);
			this.completionSource.SetCanceled();
			this.completionSource = null;
		}
	}

	public void FinishCurrentDilogue(bool result = true)
	{
		if (this.completionSource != null)
		{
			this.gameObject.SetActive(false);
			this.completionSource.SetResult(result);
			this.completionSource = null;
		}
	}
}
