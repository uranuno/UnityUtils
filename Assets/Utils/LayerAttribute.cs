using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LayerAttribute : PropertyAttribute {}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LayerAttribute))]
public class LayerDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {

		EditorGUI.BeginProperty (position, label, prop);

		prop.intValue = EditorGUI.LayerField(position, label, prop.intValue);

		EditorGUI.EndProperty ();
	}
}
#endif