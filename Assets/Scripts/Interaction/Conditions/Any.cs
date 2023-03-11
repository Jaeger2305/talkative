using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Any : Condition
{
	[SerializeField]
	private Condition[] conditions;

	public override bool IsFullfilled => this.conditions.Any(condition => condition.IsFullfilled);
}
