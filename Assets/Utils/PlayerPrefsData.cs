using UnityEngine;

/// <summary>
/// PlayerPrefsにオブジェクトを保存するためのヘルパー。
/// Json文字列化して保存する。
/// 保存キー = 型名になるので、1クラス1データ
/// </summary>
public static class PlayerPrefsData<T>
{
	/// <summary>
	/// 保存キー = 型名
	/// </summary>
	private static readonly string m_Key = typeof(T).Name;

	/// <summary>
	/// すでに保存されているか
	/// </summary>
	public static bool IsSaved ()
	{
		return PlayerPrefs.HasKey (m_Key);
	}

	/// <summary>
	/// データの読み込み（データがなければ引数で渡したデータが返る）
	/// </summary>
	public static T Load (T defaultData = default(T))
	{
		var json = PlayerPrefs.GetString (
			m_Key,
			JsonUtility.ToJson (defaultData)
		);
		var data = JsonUtility.FromJson<T> (json);
		return data;
	}

	/// <summary>
	/// データの保存
	/// </summary>
	public static void Save (T data)
	{
		var json = JsonUtility.ToJson (data);
		PlayerPrefs.SetString (m_Key, json);
		PlayerPrefs.Save ();
	}

	/// <summary>
	/// データの削除
	/// </summary>
	public static void Delete ()
	{
		PlayerPrefs.DeleteKey (m_Key);
	}
}
