using UnityEngine;
using System.Collections;

public class Setcolor : MonoBehaviour {
	//private player1 play;
	private Player plays;
	private GameSelection comp;
	private GameObject prev;
	// Use this for initialization
	void Start () {
	//	play= FindObjectOfType(typeof(player1))as player1;
		plays= FindObjectOfType(typeof(Player))as Player;
		comp = FindObjectOfType (typeof(GameSelection))as GameSelection;
		prev = null;
	}
	public void getShole(int h)
	{
		if (GameSceneManager.selection == "1 player") {
			Debug.Log(plays.selection);
						plays.previous = plays.current;
						plays.current = "P2h" + h;
						prev = GameObject.FindGameObjectWithTag (plays.current);
						prev.GetComponent<Renderer>().material.color = Color.magenta; 
						Debug.Log(plays.previous);
						Debug.Log(plays.current);
						if (plays.previous != null) {
								prev = GameObject.FindGameObjectWithTag (plays.previous); 
								prev.GetComponent<Renderer>().material.color = plays.color; // change color of old selected hole}
						}
				}
		if (GameSceneManager.selection == "2 computers") {
						plays.previous = plays.current;
						if (comp.Complayer == 1) {
								plays.current = "P1h" + h;
								prev = GameObject.FindGameObjectWithTag (plays.current);
								prev.GetComponent<Renderer>().material.color = Color.magenta;
						} else {
								plays.current = "P2h" + h;
								prev = GameObject.FindGameObjectWithTag (plays.current);
								prev.GetComponent<Renderer>().material.color = Color.magenta;
						}
						if (plays.previous != null) {
								prev = GameObject.FindGameObjectWithTag (plays.previous); 
								prev.GetComponent<Renderer>().material.color = plays.color; // change color of old selected hole}
						}
				}
		}
	// Update is called once per frame
	void Update () {
	
	}
}
