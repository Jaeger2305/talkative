using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMProTextFader : MonoBehaviour
{
	private TextMeshProUGUI textElement;
	private string fullText;
	public string Text
	{
		get => this.fullText;
		set
		{
			this.fullText = value;
			this.textElement.text = this.fullText;
			if (this.runnigFade != null)
			{
				this.StopCoroutine(this.runnigFade);
				this.runnigFade = null;
			}
			this.runnigFade = this.AnimateVertexColors();
			this.StartCoroutine(this.runnigFade);
		}
	}

	[SerializeField]
	private float fadeSpeed = 1.0F;
	[SerializeField]
	private int rolloverCharacterSpread = 10;
	[SerializeField]
	private float fadeDelay = 0;

	private float fadeStart;

	private IEnumerator runnigFade;

	void Awake()
	{
		this.textElement = this.GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{

	}

	/// <summary>
	/// Method to animate vertex colors of a TMP Text object.
	/// </summary>
	/// <returns></returns>
	IEnumerator AnimateVertexColors()
	{
		// Need to force the text object to be generated so we have valid data to work with right from the start.
		this.textElement.ForceMeshUpdate();
		TMP_TextInfo textInfo = this.textElement.textInfo;
		int currentCharacter = 0;
		int startingCharacterRange = currentCharacter;
		bool isRangeMax = false;
		int characterCount = textInfo.characterCount;

		Debug.LogFormat("clear");

		for (int i = 0; i < textInfo.characterInfo.Length; i++)
		{
			int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
			Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;
			int vertexIndex = textInfo.characterInfo[i].vertexIndex;
			// Get the current character's alpha value.
			// Set new alpha values.
			newVertexColors[vertexIndex + 0].a = 0;
			newVertexColors[vertexIndex + 1].a = 0;
			newVertexColors[vertexIndex + 2].a = 0;
			newVertexColors[vertexIndex + 3].a = 0;
			textInfo.meshInfo[materialIndex].colors32 = newVertexColors;
		}
		// Upload the changed vertex colors to the Mesh.
		this.textElement.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
		this.textElement.ForceMeshUpdate();

		yield return new WaitForSeconds(this.fadeDelay);
		Debug.LogFormat("start");
		while (!isRangeMax)
		{
			// Spread should not exceed the number of characters.
			byte fadeSteps = (byte)Mathf.Max(1, 255 / rolloverCharacterSpread);
			for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
			{
				// Skip characters that are not visible
				// if (textInfo.characterInfo[i].isVisible)
				{
					int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
					Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;
					int vertexIndex = textInfo.characterInfo[i].vertexIndex;
					// Get the current character's alpha value.
					var currentAlpha = newVertexColors[vertexIndex + 0].a;
					byte newAlpha = (byte)Mathf.Clamp(currentAlpha + 0, 0, 255);
					// Set new alpha values.
					newVertexColors[vertexIndex + 0].a = newAlpha;
					newVertexColors[vertexIndex + 1].a = newAlpha;
					newVertexColors[vertexIndex + 2].a = newAlpha;
					newVertexColors[vertexIndex + 3].a = newAlpha;
					textInfo.meshInfo[materialIndex].colors32 = newVertexColors;
					if (newAlpha == 255)
					{
						startingCharacterRange += 1;
						if (startingCharacterRange == characterCount)
						{
							// Update mesh vertex data one last time.
							this.textElement.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
							// yield return new WaitForSeconds(1.0f);
							// Reset the text object back to original state.
							this.textElement.ForceMeshUpdate();
							// yield return new WaitForSeconds(1.0f);
							// // Reset our counters.
							// currentCharacter = 0;
							// startingCharacterRange = 0;

							isRangeMax = true;
							Debug.LogFormat("done");
							break;
						}
					}
				}
			}
			// Upload the changed vertex colors to the Mesh.
			this.textElement.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
			if (currentCharacter + 1 < characterCount)
			{
				currentCharacter += 1;
			}
			yield return new WaitForSeconds((25f - fadeSpeed) * 0.01f);
		}
	}
}
