using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

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
	public GameObject[] restart ;
	public GameObject[] pause;
	public GameObject[] options;
	public GameObject[] seed1;
	public GameObject[] seed2;
	public GameObject[] turnposition;
	public GameObject player;
	public Text player1Seed;
	public Text player2Seed; 
	public Camera[] mainCamera;
	public SpriteRenderer loadimage;
	public Text load;


	public GameNetworkManager gameNetworkManager;


	// Use this for initialization
	void Start ()
	{
//		GameSceneManager.selection = "2 players";
//		GameSceneManager.choice = 0;
		//gameNetworkManager = FindObjectOfType (typeof(GameNetworkManager))as GameNetworkManager;
		StartCoroutine (NetworkPlay ());

	}

public void SetupObjects(int choice){
		load.gameObject.SetActive (false);
		loadimage.enabled = false;
		mainCamera [choice ].gameObject.SetActive (true);
		mainCamera [(choice + 1) %2 ].gameObject.SetActive (false);
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
		if (GameSceneManager.selection == "2 players") {
			gameNetworkManager.startServer ();
			while (gameNetworkManager.waiting == true) {
				yield return new WaitForSeconds (0.3f);
			}
			gameNetworkManager.waiting = true;
			SetupObjects (GameSceneManager.choice);
		} else {
			SetupObjects (0);
			player.gameObject.SetActive (true);
			yield return null;
		}
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}

