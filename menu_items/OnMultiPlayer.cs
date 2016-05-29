using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnMultiPlayer : MonoBehaviour {
	public Loading loadObject;

	// Use this for initialization
	void Start () {
	
	}

	public void OnMouseDown () {
		loadObject.LoadingText.gameObject.SetActive (true);
		loadObject.LoadingBackg.gameObject.SetActive (true);
		GameSceneManager.selection = "2 players";
		SceneManager.LoadScene("Mancala");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
