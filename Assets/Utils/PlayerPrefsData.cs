using UnityEngine;

/// <summary>
/// PlayerPrefsにオブジェクトを保存するためのヘルパー。
/// Json文字列化して保存する。
/// 保存キー = 型名になるので、1クラス1データ
/// </summary>
public static class PlayerPrefsData
{
	/// <summary>
	/// 保存キー取得
	/// </summary>
	private static string GetKey<T> ()
	{
		return typeof(T).Name;
	}

	/// <summary>
	/// すでに保存されているか
	/// </summary>
	public static bool IsSaved<T> ()
	{
		return PlayerPrefs.HasKey (GetKey<T> ());
	}

	/// <summary>
	/// データの読み込み（データがなければ引数で渡したデータが返る）
	/// </summary>
	public static T Load<T> (T defaultData = default(T))
	{
		var json = PlayerPrefs.GetString (
			GetKey<T> (),
			JsonUtility.ToJson (defaultData)
		);
		var data = JsonUtility.FromJson<T> (json);
		return data;
	}

	/// <summary>
	/// データの保存
	/// </summary>
	public static void Save<T> (T data)
	{
		var json = JsonUtility.ToJson (data);
		PlayerPrefs.SetString (GetKey<T> (), json);
		PlayerPrefs.Save ();
	}

	/// <summary>
	/// データの削除
	/// </summary>
	public static void Delete<T> ()
	{
		PlayerPrefs.DeleteKey (GetKey<T> ());
	}
}
