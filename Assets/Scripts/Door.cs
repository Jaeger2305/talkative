using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField]
	private GameObject openDoor;
	[SerializeField]
	private GameObject closedDoor;

	public void Open()
	{
		this.SetOpen(true);
	}
	public void Close()
	{
		this.SetOpen(false);
	}
	public void SetOpen(bool open)
	{
		this.openDoor.SetActive(open);
		this.closedDoor.SetActive(!open);
	}
}
