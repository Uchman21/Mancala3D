using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;


public class Holes : MonoBehaviour {
	public GameObject[] Seed_num_backg;
	public Text Seed_num;
	// Use this for initialization
	void Start () {
		Seed_num_backg [0].SetActive (false);
		Seed_num_backg [1].SetActive (false);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver() {
		if (!GameSceneManager.GameOver && !GameSceneManager.isPaused && !GameSceneManager.isOnOption){
			int num = int.Parse(Regex.Replace (tag, @"\D", ""));
			if (GameSceneManager.selection == "1 Player") {
				Seed_num.text = GameSelectionOnePlayer.A [num - 1].ToString();
			} else {
				Seed_num.text = GameSelection.A [num - 1].ToString();
			}
			Seed_num.gameObject.SetActive (true);
			Seed_num_backg[GameSceneManager.choice].SetActive (true);
			Seed_num_backg[GameSceneManager.choice].transform.position = transform.position + (Vector3.up * 1f);
			Vector3 ViewportPosition = Camera.main.WorldToViewportPoint(Seed_num_backg[GameSceneManager.choice].transform.position);
			Seed_num.rectTransform.anchorMin = ViewportPosition + (Vector3.right * 0.045f) + (Vector3.up * 0.05f);
			Seed_num.rectTransform.anchorMax = ViewportPosition + (Vector3.right * 0.045f) + (Vector3.up * 0.05f);
			//Seed_num.gameObject.transform.position = Seed_num_backg.transform.position;
		}
	}

	void OnMouseExit() {
		Seed_num_backg[GameSceneManager.choice].transform.position = new Vector3 (100, 100, 100);
		Seed_num_backg[GameSceneManager.choice].SetActive (false);
		Seed_num.gameObject.SetActive (false);
		Vector3 ViewportPosition = Camera.main.WorldToViewportPoint(Seed_num_backg[GameSceneManager.choice].transform.position);
		Seed_num.rectTransform.anchorMin = ViewportPosition + (Vector3.right * 0.045f);
		Seed_num.rectTransform.anchorMax = ViewportPosition + (Vector3.right * 0.045f);
	}
}
