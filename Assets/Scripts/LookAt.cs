using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float maxDistance;
	[SerializeField]
	private float maxRotationSpeedDegreePerSecond = 15;

	void Update()
	{
		if (this.target != null)
		{
			var toTarget = this.target.position - this.transform.position;
			toTarget.y = 0;
			if (toTarget.sqrMagnitude <= this.maxDistance * this.maxDistance)
			{
				var topDownForward = this.transform.forward;
				topDownForward.y = 0;
				var angle = Vector3.SignedAngle(topDownForward, toTarget, Vector3.up);
				var maxStep = this.maxRotationSpeedDegreePerSecond * Time.deltaTime;
				angle = Mathf.Clamp(angle, -maxStep, maxStep);
				this.transform.Rotate(Vector3.up, angle);
			}
		}
	}
}
