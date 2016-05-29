using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnSinglePlayer : MonoBehaviour {
	public Loading loadObject;

	// Use this for initialization
	void Start () {
	
	}

	public void OnMouseDown () {
		loadObject.LoadingText.gameObject.SetActive (true);
		loadObject.LoadingBackg.gameObject.SetActive (true);
		GameSceneManager.selection = "1 player";
		GameSceneManager.choice = 0;
		SceneManager.LoadScene("Mancala");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
