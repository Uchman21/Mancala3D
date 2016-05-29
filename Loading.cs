using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loading : MonoBehaviour {
	public GameObject LoadingBackg;
	public Text LoadingText;
	// Use this for initialization
	void Start () {
		LoadingBackg.gameObject.SetActive (false);
		LoadingText.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
