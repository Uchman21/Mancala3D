using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;


public class Holes : MonoBehaviour {
	public GameObject[] Seed_num_backg;
	private GameObject Seed_num;
	private float lastClickTime;
	private GameSelectionOnePlayer OnePlayer;
	public GameSelection TwoPlayers;
	public static bool connectioned = false;
	GameObject seed_num_backg_prefab;

	private float catchTime = 0.25f;
	// Use this for initialization
	void Start () {
		OnePlayer= FindObjectOfType(typeof(GameSelectionOnePlayer))as GameSelectionOnePlayer;
		GameSelection [] players = FindObjectsOfType(typeof(GameSelection))as GameSelection[];
		foreach (GameSelection player in players) {
			if (player.isLocalPlayer ) {
				TwoPlayers = player;
				break;
			}
		}
		Seed_num = GameObject.FindWithTag ("number") as GameObject;
	}
		

	void Update ()
	{
		if (tag == "not_local") {
			return;
		}
		if (Application.platform != RuntimePlatform.Android) {
			if (Input.GetButtonDown ("Fire1")) {
				RaycastHit hit;
				Ray ray;
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);              // get mouse position

				if (Physics.Raycast (ray, out hit, 100f)) {
					if (hit.rigidbody == gameObject.GetComponent<Rigidbody> ()) {
						if (Time.time - lastClickTime < catchTime) {
							if (GameSceneManager.selection == "1 Player") {
								OnePlayer.isDoubleClick = true;
							} else {
								TwoPlayers.isDoubleClick = true;
							}
							MouseIsOver ();  
						} else {
							Debug.Log (connectioned);
							Debug.Log (GameSceneManager.whichplayer);
							Debug.Log ("numcon: " + GameSceneManager.Numconnection);
							if (GameSceneManager.selection == "1 Player") {
								OnePlayer.isDoubleClick = false;
								StartCoroutine (OnePlayer.handleClick (tag, catchTime));
							} else if (GameSceneManager.selection == "2 Players" && connectioned == true) {
								TwoPlayers.isDoubleClick = false;
								StartCoroutine (TwoPlayers.handleClick (tag, catchTime));
							}


						}
					}
				}
				lastClickTime = Time.time;
			}
		} else {

			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
				if (Physics.Raycast (ray, out hit, 100f)) {
					if (hit.rigidbody == gameObject.GetComponent<Rigidbody> ()) {
						if (Input.GetTouch (0).tapCount > 1) {
							MouseIsOver (); 
						} else {
							if (GameSceneManager.selection == "1 Player") {
								OnePlayer.isDoubleClick = false;
								StartCoroutine (OnePlayer.handleClick (tag, catchTime));
							} else if (GameSceneManager.selection == "2 Players" && connectioned == true) {
								TwoPlayers.isDoubleClick = false;
								StartCoroutine (TwoPlayers.handleClick (tag, catchTime));
							}
						}
					}
				}
			}
		}
	}

	void MouseIsOver() {
		if (!GameSceneManager.GameOver && !GameSceneManager.isPaused && !GameSceneManager.isOnOption){
			int num = int.Parse(Regex.Replace (tag, @"\D", ""));
			if (GameSceneManager.selection == "1 Player") {
				Seed_num.GetComponent<Text>().text = GameSelectionOnePlayer.A [num - 1].ToString();
			} else {
				Seed_num.GetComponent<Text>().text = GameSelection.A [num - 1].ToString();
			}
			Seed_num.gameObject.SetActive (true);
			Seed_num_backg[GameSceneManager.choice].SetActive (true);
			Seed_num_backg[GameSceneManager.choice].transform.position = this.transform.position + (Vector3.up * 3.5f);
			seed_num_backg_prefab = Instantiate (Seed_num_backg[GameSceneManager.choice], this.transform.position + (Vector3.up * 3.5f), Seed_num_backg[GameSceneManager.choice].transform.rotation)as GameObject;
			Vector3 ViewportPosition = Camera.main.WorldToViewportPoint(Seed_num_backg[GameSceneManager.choice].transform.position);
			Seed_num.GetComponent<Text>().rectTransform.anchorMin = ViewportPosition + (Vector3.right * 0.045f) + (Vector3.up * 0.05f);
			Seed_num.GetComponent<Text>().rectTransform.anchorMax = ViewportPosition + (Vector3.right * 0.045f) + (Vector3.up * 0.05f);
		}
	}

	void OnMouseExit() {
		if (seed_num_backg_prefab) {
			Destroy (seed_num_backg_prefab);
			Seed_num.SetActive (false);
		}
	}


}
