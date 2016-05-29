using UnityEngine;
using System.Collections;

public class OnResume : MonoBehaviour
{
	private movehand1 hand;
	public OnOptions optionsMenu;
	public OnPause pauseMenu;
	private Playerhand phand;
	private int choice;
	SetUp setup;
	void Start () {
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		phand = FindObjectOfType (typeof(Playerhand))as Playerhand;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
	}

	void OnMouseDown(){
		//Resources.UnloadUnusedAssets ();
		choice = GameSceneManager.choice;
		setup.graphics.gameObject.SetActive (false);
		setup.sound.gameObject.SetActive (false);
		setup.exit[choice].gameObject.SetActive (false);
		setup.restart[choice].gameObject.SetActive (false);
		setup.resume[choice].SetActive (false);
		setup.background[choice].SetActive (false);
		phand.handpoly.enabled = true;
		hand.handpoly.enabled = true;
		Time.timeScale = 1;
		optionsMenu.gameObject.SetActive (true);
		pauseMenu.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

