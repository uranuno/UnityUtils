using UnityEditor;
using UnityEngine;

public static class Utils
{
	[MenuItem ("Utils/Capture")]
	public static void Capture ()
	{
		var fileName = EditorUtility.SaveFilePanel (
			"Choose Location of the Capture",
			Application.dataPath,
			System.DateTime.Now.ToString ("yyyyMMdd-HHmmss-fff"),
			"png"
		);
		if (fileName.Length == 0)
			return;

		Application.CaptureScreenshot (fileName);
	}
}
