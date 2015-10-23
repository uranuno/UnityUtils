using UnityEngine;
using UnityEngine.UI;

public class AssetGroupExample : MonoBehaviour {

	[SerializeField]
	AssetGroup items;

	[SerializeField]
	Button buttonPrefab;

	[SerializeField]
	Transform buttonContainer;

	// Use this for initialization
	void Start () {
		foreach (var item in items)
			CreateItemButton (item);
	}

	void CreateItemButton (AssetReference item) {

		Button button = (Button)Instantiate (buttonPrefab);
		button.GetComponentInChildren<Text>().text = "Create " + item.name;
		button.transform.SetParent(buttonContainer, false);

		button.onClick.AddListener (() => {
			GameObject go = (GameObject)Instantiate(item.asset, transform.position, transform.rotation);
			Destroy (go, 2f);
		});
	}
}
