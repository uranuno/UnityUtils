using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Array = System.Array;
#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
using System.IO;
using System.Xml.Linq;
#endif

[System.Serializable]
public class AssetReference {
	public string name;
	public Object asset;
	
	public AssetReference (Object asset) {
		this.name = asset.name;
		this.asset = asset;
	}
}

public class AssetGroup : ScriptableObject, IEnumerable<AssetReference> {

	public System.StringComparison comparisonType;

	[SerializeField]
	AssetReference[] assetRefs;

	public IEnumerator<AssetReference> GetEnumerator () {
		foreach (var assetRef in assetRefs) {
			yield return assetRef;
		}
	}

	IEnumerator IEnumerable.GetEnumerator () {
		return GetEnumerator();
	}

	/// <summary>
	/// Assetの参照を取得する
	/// </summary>
	/// <param name="index">AssetのIndex</param>
	public Object GetAsset (int index) {
		return (index < 0 || index > assetRefs.Length-1) ? null : assetRefs[index].asset;
	}

	/// <summary>
	/// Assetの参照を取得する
	/// </summary>
	/// <param name="name">Assetの名前</param>
	public Object GetAsset (string name) {
		return GetAsset (Array.FindIndex(assetRefs, assetRef => assetRef.name.Equals(name, comparisonType)));
	}

	public T GetAsset<T> (int index) where T : Object { return (T)GetAsset (0); }
	public T GetAsset<T> (string name) where T : Object { return (T)GetAsset (name); }

	#region ファイル名と参照名が違う時
	[System.Serializable]
	class AlternativeAssetName {
		public string assetName = "";
		public string referenceName = "";

		public AlternativeAssetName (string assetName, string referenceName) {
			this.assetName = assetName;
			this.referenceName = referenceName;
		}
	}
	
	[SerializeField]
	AlternativeAssetName[] altAssetNames;
	#endregion

	#if UNITY_EDITOR
	const string PREFS_KEY = "AltAssetNames";

	[MenuItem("Assets/Create Asset Group")]
	static void Create () {
		var assetGroup = CreateInstance<AssetGroup> ();
		assetGroup.assetRefs = Selection.GetFiltered(typeof(Object), SelectionMode.Assets)
			.Select(o => new AssetReference(o))
			.ToArray();

		// .assetとして保存
		if (assetGroup.assetRefs.Length > 0) {
			// ソートしとく
			assetGroup.SortAssetRefs();

			var objPath = AssetDatabase.GetAssetPath(assetGroup.GetAsset(0));
			var groupPath = Directory.GetParent(objPath) + "/AssetGroup.asset";
			
			AssetDatabase.CreateAsset (assetGroup, groupPath);
			AssetDatabase.SaveAssets();

			Debug.Log (groupPath + " Created!");
		} else {
			Debug.LogWarning ("Please select assets! (Creating assetGroup with folder is impossible.)");
		}
	}

	/// <summary>
	/// 並び替える
	/// </summary>
	[ContextMenu("Sort Asset Refs By Name")]
	void SortAssetRefs () {
		assetRefs = assetRefs.OrderBy (assetRef => assetRef.name).ToArray();
	}

	/// <summary>
	/// 代替名が使われているファイル名を本当の名前で置換する
	/// </summary>
	[ContextMenu("Apply Alt Asset Names To Asset Refs")]
	void ApplyAltAssetNames () {
		foreach (var alt in altAssetNames) {
			var assetRef = Array.Find (assetRefs, aRef => aRef.name == alt.assetName);
			if (assetRef != null) {
				assetRef.name = alt.referenceName;
			}
		}
		SortAssetRefs();
	}

	/// <summary>
	/// Alt Asset Names をEditorPrefs にコピーする
	/// </summary>
	[ContextMenu("Copy Alt Asset Names")]
	void CopyAltAssetNames () {
		XElement elms = new XElement("altNames");
		foreach (var alt in altAssetNames) {
			elms.Add(new XElement("altName",
			                     new XElement("assetName",alt.assetName),
			                     new XElement("referenceName",alt.referenceName)
			                     ));
		}
		EditorPrefs.SetString(PREFS_KEY, elms.ToString());
	}

	/// <summary>
	/// EditorPrefs にコピーされたAlt Asset Names を反映させる
	/// </summary>
	[ContextMenu("Paste Alt Asset Names")]
	void PasteAltAssetNames () {
		if (!EditorPrefs.HasKey(PREFS_KEY)) {
			Debug.LogWarning("No Data...");
			return;
		}

		XElement elms = XElement.Parse (EditorPrefs.GetString(PREFS_KEY));
		altAssetNames = elms.Elements("altName")
			.Select(elm => new AlternativeAssetName(
				elm.Element("assetName").Value,
				elm.Element("referenceName").Value
				))
			.ToArray();
	}
	#endif
}
