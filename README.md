# MyUnityUtils

## Tag and Layer Attribute
```csharp
[Tag]
public string targetTag;

[Layer]
public int targetLayer;
```

![Tag And Layer Attribute](http://uranuno.github.io/MyUnityUtils/tagandlayer.png)

![Tag And Layer Attribute - Tag](http://uranuno.github.io/MyUnityUtils/tagandlayer-tag.png)
![Tag And Layer Attribute - Layer](http://uranuno.github.io/MyUnityUtils/tagandlayer-layer.png)


## Min Max
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

![Min Max Range Attribute](http://uranuno.github.io/MyUnityUtils/minmaxrange.gif)