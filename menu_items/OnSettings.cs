using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void OnMouseDown () {
		SceneManager.LoadScene ("settingsmenu");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
