﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/*Congo Drummer sound from http://soundbible.com/122-Congo-Drummer.html*/
/*Afron Congas sound by user rasputin1963 from http://www.looperman.com*/

public class SetUp : MonoBehaviour
{
	public Text turn;
	public Text round;
	public Text player1house;
	public Text player2house;

	public Dropdown sound;
	public Dropdown graphics;
	public GameObject[] resume;
	public GameObject[] background = new GameObject[2] ;
	public GameObject[] exit ;
	public GameObject[] Dlight ;
	public GameObject[] restart ;
	public GameObject[] pause;
	public GameObject[] options;
	public GameObject[] seed1;
	public GameObject[] seed2;
	public GameObject hand;
	public Text Outcome;
	public Text BackgroundTitleText;
	public Text Timer;
	public GameObject[] turnposition;
	Vector3[,] endGamePosition = new Vector3[2,2];
	Vector3[,] restartGamePosition = new Vector3[2,2];
	Vector3[,] endGamePositionInit = new Vector3[1,2];
	Vector3[,] restartGamePositionInit = new Vector3[1,2];
	public GameObject player;
	public Text player1Seed;
	public Text player2Seed; 
	public Camera[] mainCamera;
	public SpriteRenderer[] loadimage = new SpriteRenderer[2];
	public Text load;
	public static bool disconnected = false;
	public static string message;


	public GameNetworkManager gameNetworkManager;


	// Use this for initialization
	void Start ()
	{
		
//		GameSceneManager.selection = "2 players";
//		GameSceneManager.choice = 0;
		//gameNetworkManager = FindObjectOfType (typeof(GameNetworkManager))as GameNetworkManager;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		GameSceneManager.GameOver = false;
		GameSceneManager.isPaused = false;
		GameSceneManager.isOnOption = false;
		endGamePosition[0,0] = new Vector3(-7.15f,-7.77f,39.9f);
		endGamePosition [0, 1] = new Vector3 (3f, 3.1f, 1f);
		endGamePosition[1,0] = new Vector3 (-6.8f, -7.37f, 39.9f);
		endGamePosition [1, 1] = new Vector3 (3f, 3.1f, 1f);

		restartGamePosition[0,0] = new Vector3(8.07f,-7.77f,39.9f);
		restartGamePosition [0, 1] = new Vector3 (4f, 4f, 1f);
		restartGamePosition[1,0] = new Vector3 (9.06f, -7.37f, 39.9f);
		restartGamePosition [1, 1] = new Vector3 (4f, 4f, 1f);
		StartCoroutine (NetworkPlay ());

	}

public void SetupObjects(int choice){
		load.gameObject.SetActive (false);
		loadimage[0].enabled = false;
		loadimage[1].enabled = false;
		hand.SetActive (true);
		mainCamera [choice ].gameObject.SetActive (true);
		mainCamera [(choice + 1) %2 ].gameObject.SetActive (false);
		Dlight [choice].SetActive (true);
		Dlight [(choice + 1) % 2].SetActive (false);
		turn.text = "Wait";
		graphics.gameObject.SetActive (false);
		sound.gameObject.SetActive (false);
		exit[choice].gameObject.SetActive (false);
		restart[choice].gameObject.SetActive (false);
		resume[choice].SetActive (false);
		background[choice].SetActive (false);
		exit[(choice + 1) %2].gameObject.SetActive (false);
		restart[(choice + 1) %2].gameObject.SetActive (false);
		resume[(choice + 1) %2].SetActive (false);
		background[(choice + 1) %2].SetActive (false);
		pause[(choice + 1) %2].SetActive (false);
		options[(choice + 1) %2].SetActive (false);
		BackgroundTitleText.gameObject.SetActive (false);
		//Outcome.gameObject.SetActive (false);
		position (choice);
}


	public	void position(int choice){
	Vector3 ViewportPosition1=Camera.main.WorldToViewportPoint(seed1[choice].transform.position);
	Vector3 ViewportPosition2=Camera.main.WorldToViewportPoint(seed2[choice].transform.position);
	Vector3 ViewportPositionturn=Camera.main.WorldToViewportPoint(turnposition[choice].transform.position);
	turn.rectTransform.anchorMin = ViewportPositionturn;
	turn.rectTransform.anchorMax = ViewportPositionturn;
	round.rectTransform.anchorMin = ViewportPosition2 + (Vector3.right * 0.2f);
	round.rectTransform.anchorMax = ViewportPosition2 + (Vector3.right * 0.2f);
	player1house.rectTransform.anchorMin = ViewportPosition1 + (Vector3.right * 0.07f);
	player1house.rectTransform.anchorMax = ViewportPosition1 + (Vector3.right * 0.07f);
	player2house.rectTransform.anchorMin = ViewportPosition2 + (Vector3.right * 0.07f);
	player2house.rectTransform.anchorMax = ViewportPosition2 + (Vector3.right * 0.07f);
	player1Seed.rectTransform.anchorMin = ViewportPosition1 + (Vector3.right * 0.03f);
	player1Seed.rectTransform.anchorMax = ViewportPosition1 + (Vector3.right * 0.03f);
	player2Seed.rectTransform.anchorMin = ViewportPosition2 + (Vector3.right * 0.03f);
	player2Seed.rectTransform.anchorMax = ViewportPosition2 + (Vector3.right * 0.03f);
	
}


	public IEnumerator NetworkPlay(){
		if (GameSceneManager.selection == "2 Players") {
			gameNetworkManager.startServer ();
			while (gameNetworkManager.waiting == true) {
				yield return new WaitForSeconds (0.3f);
			}
			gameNetworkManager.waiting = true;
			SetupObjects (GameSceneManager.choice);
		} else {
			GameSceneManager.choice = 0;
			GameSceneManager.selection = "1 Player";
			SetupObjects (GameSceneManager.choice);
			player.gameObject.SetActive (true);
			yield return null;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				GameSceneManager.isOnOption = true;
				exit [GameSceneManager.choice].gameObject.SetActive (true);
				restart [GameSceneManager.choice].gameObject.SetActive (true);
				resume [GameSceneManager.choice].SetActive (true);
				background [GameSceneManager.choice].SetActive (true);
				BackgroundTitleText.text = "OPTIONS";
				BackgroundTitleText.gameObject.SetActive (true);
				Time.timeScale = 0;
			}
		}
		if (disconnected == true) {
			StartCoroutine (ShowMessage(1.0f));
		}
	}

	//Game end
	public void EndGame(string outcome, int choice){
		GameSceneManager.GameOver = true;
		background[choice].SetActive (true);
		BackgroundTitleText.text = "GameOver";
		BackgroundTitleText.gameObject.SetActive (true);
		Outcome.text = outcome;
		Outcome.gameObject.SetActive (true);

		endGamePositionInit[0,0] = exit [choice].transform.localPosition;
		endGamePositionInit [0, 1] = exit [choice].transform.localScale;

		restartGamePositionInit[0,0] = restart [choice].transform.localPosition;
		restartGamePositionInit [0, 1] = restart [choice].transform.localScale;

		exit [choice].transform.localPosition = endGamePosition [choice, 0];
		exit [choice].transform.localScale = endGamePosition [choice, 1];
		exit [choice].SetActive (true);
		restart [choice].transform.localPosition = restartGamePosition [choice, 0];
		restart [choice].transform.localScale = restartGamePosition [choice, 1];
		restart [choice].SetActive (true);
		pause[choice].SetActive (false);
		options[choice].SetActive (false);
	}

	IEnumerator ShowMessage (float delay) {
		disconnected = false;
		GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
		load.text = message;
		load.gameObject.SetActive (true);
		loadimage[GameSceneManager.choice].enabled = true;
		turn.gameObject.SetActive (false);
		round.gameObject.SetActive (false);
		player1house.gameObject.SetActive (false);
		player2house.gameObject.SetActive (false);
		player1Seed.gameObject.SetActive (false);
		player2Seed.gameObject.SetActive (false);
		yield return new WaitForSeconds(delay);
		load.gameObject.SetActive (false);
		loadimage[GameSceneManager.choice].enabled = false;
		load.text = "Setting up the Environment............";
		foreach(GameObject Objs in GameObjects)
		{
			Destroy(Objs);
		}
		SceneManager.LoadScene("main menu");
	}
}

