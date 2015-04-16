using UnityEngine;

public class MinMaxRangeExample : MonoBehaviour {

	[Range(0,10f)]
	public float otherValue;

	[SerializeField, MinMaxRange(0,10f)]
	Vector2 randomDelayRange;
	
	float randomDelay { get { return Random.Range(randomDelayRange.x, randomDelayRange.y); } }
	
	float delay;
	float accum;
	
	void Update () {
		
		accum += Time.deltaTime;

		if (accum >= delay) {
			Debug.Log ("Fire!");
			accum = 0;
			delay = randomDelay;
		}
	}
}
