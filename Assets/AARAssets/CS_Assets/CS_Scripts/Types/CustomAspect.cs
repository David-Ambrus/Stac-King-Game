using UnityEngine;
using System.Collections;

namespace AAR.Types
{
	/// <summary>
	/// Represents the settings for a custom aspect item in the inspector.
	/// </summary>
	[System.Serializable]
	public sealed class CustomAspect
	{
		public string  name                  = "Default";
		public Vector2 aspect                = new Vector2(16, 9);
		public float   cameraSizeChange      = 0.0f;
		public Vector3 backgroundScaleChange = new Vector3();
		public float   offsetUI              = 0.0f;
	}
}