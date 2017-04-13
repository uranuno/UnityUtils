using UnityEngine;
using UnityEngine.UI;

// 端末に保存したいデータのクラス（例: 音量の設定）
public class AudioSettings
{
	public float bgmVolume = 1f;
	public float seVolume  = 1f;
}

// 使い方
public class PlayerPrefsDataExample : MonoBehaviour
{
	[SerializeField] Slider m_BgmVolumeSlider;
	[SerializeField] Slider m_SeVolumeSlider;
	[SerializeField] Button m_ResetButton;

	void Start ()
	{
		m_ResetButton.onClick.AddListener (ResetData);
	}

	void OnEnable  () { LoadData (); }
	void OnDisable () { SaveData (); }
	void OnApplicationPause (bool pause)
	{
		if (pause) SaveData ();
	}

	void LoadData ()
	{
		// データの読込
		var data = PlayerPrefsData<AudioSettings>.Load (
			//データが未保存の場合のデフォルト値
			new AudioSettings ()
		);
		// UIに反映
		m_BgmVolumeSlider.value = data.bgmVolume;
		m_SeVolumeSlider.value = data.seVolume;
	}

	void SaveData ()
	{
		// 新規データを作成
		var data = new AudioSettings ();
		// UIから値を取得
		data.bgmVolume = m_BgmVolumeSlider.value;
		data.seVolume = m_SeVolumeSlider.value;
		// データの保存
		PlayerPrefsData<AudioSettings>.Save (data);
	}

	// データのリセット
	void ResetData ()
	{
		// データの削除
		PlayerPrefsData<AudioSettings>.Delete ();
		// データの再読み込み（デフォルト値になる）
		LoadData ();
	}
}
