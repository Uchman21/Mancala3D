using UnityEngine;
using System.Collections;

public class OnOptions : MonoBehaviour {

	// Use this for initialization
	private movehand1 hand;
	private Playerhand phand;
	private OnPause pauseMenu;
	SetUp setup;
	private int choice;
	void Start () {
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		phand = FindObjectOfType (typeof(Playerhand))as Playerhand;
		pauseMenu = FindObjectOfType (typeof(OnPause))as OnPause;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;

	}
	
	public void OnMouseDown () {
		//Application.LoadLevelAdditive("settingsmenu");
		choice = GameSceneManager.choice;
		GameSceneManager.isOnOption = true;
		setup.exit[choice].gameObject.SetActive (true);
		setup.restart[choice].gameObject.SetActive (true);
		setup.resume[choice].SetActive (true);
		setup.background[choice].SetActive (true);
		setup.BackgroundTitleText.text = "OPTIONS";
		setup.BackgroundTitleText.gameObject.SetActive (true);
		phand.handpoly.enabled = false;
		hand.handpoly.enabled = false;
		pauseMenu.gameObject.SetActive (false);
		Time.timeScale = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
