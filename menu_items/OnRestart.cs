using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnRestart : MonoBehaviour {
	SetUp setup;
	// Use this for initialization
	void Start () {
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		SceneManager.LoadScene ("Mancala");
		if (GameSceneManager.GameOver == true) {
			GameSceneManager.GameOver = false;
			GameSceneManager.isPaused = false;
			GameSceneManager.isOnOption = false;

			setup.background[GameSceneManager.choice].SetActive (false);
			setup.BackgroundTitleText.gameObject.SetActive (false);
			setup.Outcome.gameObject.SetActive (false);
			setup.exit [GameSceneManager.choice].SetActive (false);
			setup.restart [GameSceneManager.choice].SetActive (false);
			setup.pause[GameSceneManager.choice].SetActive (true);
			setup.options[GameSceneManager.choice].SetActive (true);
		}
		Time.timeScale = 1;
	}
}
