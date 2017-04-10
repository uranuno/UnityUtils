# MyUnityUtils

## Tag and Layer Attribute
[Assets/Utils/TagAttribute.cs](Assets/Utils/TagAttribute.cs)  
[Assets/Utils/LayerAttribute.cs](Assets/Utils/LayerAttribute.cs)

```csharp
[Tag]
public string targetTag;

[Layer]
public int targetLayer;
```

![Tag And Layer Attribute](https://uranuno.github.io/MyUnityUtils/tagandlayer.png)

![Tag And Layer Attribute - Tag](https://uranuno.github.io/MyUnityUtils/tagandlayer-tag.png)
![Tag And Layer Attribute - Layer](https://uranuno.github.io/MyUnityUtils/tagandlayer-layer.png)


## Min Max
[Assets/Utils/MinMax.cs](Assets/Utils/MinMax.cs)

```csharp
// [Range(0,10f)]
// public float otherValue;

[SerializeField, MinMaxRange(0,10f)]
MinMax randomDelayRange;

float delay;
float accum;

void Update () {
	accum += Time.deltaTime;

	if (accum >= delay) {
		Debug.Log ("Fire!");
		accum = 0;
		delay = randomDelayRange.randomValue;
	}
}
```

![Min Max Range Attribute](https://uranuno.github.io/MyUnityUtils/minmaxrange.gif)


## PlayerPrefs Data
[Assets/Utils/PlayerPrefsData.cs](Assets/Utils/PlayerPrefsData.cs)

Helper for saving objects with PlayerPrefs.

```csharp
// Custom Class to save with PlayerPrefs
public class AudioSettings
{
	public float bgmVolume = 1f;
	public float seVolume  = 1f;
}
```

```csharp
void LoadData ()
{
	var data = PlayerPrefsData.Load<AudioSettings> (
		// Default Value
		new AudioSettings ()
	);

	m_BgmVolumeSlider.value = data.bgmVolume;
	m_SeVolumeSlider.value = data.seVolume;
}

void SaveData ()
{
	var data = new AudioSettings ();
	data.bgmVolume = m_BgmVolumeSlider.value;
	data.seVolume = m_SeVolumeSlider.value;

	PlayerPrefsData.Save<AudioSettings> (data);
}

void ResetData ()
{
	PlayerPrefsData.Delete<AudioSettings> ();
	// Reload
	LoadData ();
}
```

![PlayerPrefs Data](https://uranuno.github.io/MyUnityUtils/playerprefsdata.png)
