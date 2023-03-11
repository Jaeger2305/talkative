using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class All : Condition
{
	[SerializeField]
	private Condition[] conditions;

	public override bool IsFullfilled => this.conditions.All(condition => condition.IsFullfilled);
}
