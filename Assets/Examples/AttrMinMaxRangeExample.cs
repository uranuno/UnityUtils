using UnityEngine;

public class AttrMinMaxRangeExample : MonoBehaviour {

	[Range(0,10f)]
	public float otherValue;

	[SerializeField, MinMaxRange(0,10f)]
	MinMax delayMinMax;
	
	float delay;
	float accum;
	
	void Update () {
		
		accum += Time.deltaTime;

		if (accum >= delay) {
			Debug.Log ("Fire!");
			accum = 0;
			delay = delayMinMax.randomValue;
		}
	}
}
