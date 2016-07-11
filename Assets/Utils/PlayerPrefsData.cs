using UnityEngine;

/// <summary>
/// PlayerPrefsに保存したいデータのベースクラス。
/// Json文字列化して保存する。
/// 保存キー = 型名になるので、1クラス1データ
/// </summary>
[System.Serializable]
public abstract class PlayerPrefsData<T> where T : PlayerPrefsData<T>, new()
{
	/// <summary>
	/// 保存キー取得
	/// </summary>
	static string GetKey ()
	{
		return typeof(T).Name;
	}

	/// <summary>
	/// すでに保存されているか
	/// </summary>
	public static bool isSaved
	{
		get { return PlayerPrefs.HasKey (GetKey ()); }
	}

	/// <summary>
	/// 保存データの読み込み（データが無い場合は新規データを生成し保存する）
	/// </summary>
	public static T Load ()
	{
		T loaded = new T();
		if (!isSaved)
			loaded.Save ();//新規データ保存
		else
		{
			var json = PlayerPrefs.GetString (GetKey ());
			JsonUtility.FromJsonOverwrite (json, loaded);
		}
		return loaded;
	}

	/// <summary>
	/// データの保存
	/// </summary>
	public void Save ()
	{
		var json = JsonUtility.ToJson (this);
		PlayerPrefs.SetString (GetKey (), json);
		PlayerPrefs.Save ();
	}
}
