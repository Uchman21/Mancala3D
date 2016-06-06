using UnityEngine;
using System.Collections;

public class OnPause : MonoBehaviour {
	// Use this for initialization
	private movehand1 hand;
	private OnOptions optionsMenu;
	private Playerhand phand;
	private int choice;
	SetUp setup;

	void Start () {
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		phand = FindObjectOfType (typeof(Playerhand))as Playerhand;
		optionsMenu = FindObjectOfType (typeof(OnOptions))as OnOptions;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;


	}

	public void OnMouseDown () {
		choice = GameSceneManager.choice;
//		setup.exit[choice].gameObject.SetActive (true);
//		setup.restart[choice].gameObject.SetActive (true);
		setup.resume[choice].SetActive (true);
		setup.background[choice].SetActive (true);
		setup.graphics.gameObject.SetActive (true);
		setup.sound.gameObject.SetActive (true);
		setup.BackgroundTitleText.text = "PAUSED";
		setup.BackgroundTitleText.gameObject.SetActive (true);
		phand.handpoly.enabled = false;
		hand.handpoly.enabled = false;
		GameSceneManager.isPaused = true;
		optionsMenu.gameObject.SetActive (false);
		Time.timeScale = 0;

	}
	// Update is called once per frame
	void Update () {
	
	}
}
