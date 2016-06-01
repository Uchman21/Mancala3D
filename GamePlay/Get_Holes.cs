using UnityEngine;
using System.Collections;

public class Get_Holes : MonoBehaviour {
	public GameSelection play2;
	public Player plays;
	//Color color;
	public int realbox;
	private bool engage;
	private bool keepColor;
	private GameObject prev;
	private string current;
	//private string previous;
	//RaycastHit hit;
	// Use this for initialization
	void Start () {
		play2= FindObjectOfType(typeof(GameSelection))as GameSelection;
		plays= FindObjectOfType(typeof(Player))as Player;
		plays.color=GetComponent<Renderer>().material.color;
		prev = null;
		keepColor = false;
	}
	
	void OnMouseOver()
	{
		GetComponent<Renderer>().material.color = Color.gray;
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = plays.color;
		if (keepColor == true) {
			prev = GameObject.FindGameObjectWithTag (plays.current);
			prev.GetComponent<Renderer>().material.color = Color.magenta;  // change color of new selected hole
			if (plays.previous != null) {
				prev = GameObject.FindGameObjectWithTag (plays.previous); 
				prev.GetComponent<Renderer>().material.color = plays.color; // change color of old selected hole}
			}
		}
	}
	
	int getblock()
	{
		
//		if(hit.transform.gameObject.tag=="P2h1")return 0;
//		else if (hit.transform.gameObject.tag=="P2h2")return 1;
//		else if (hit.transform.gameObject.tag=="P2h3")return 2;
//		else if (hit.transform.gameObject.tag=="P2h4")return 3;
//		else if (hit.transform.gameObject.tag=="P2h5")return 4;
//		else if (hit.transform.gameObject.tag=="P2h6")return 5;
//		else return -1;
		return 0;
		
	}



	IEnumerator getty ()
	{
		realbox=getblock();
		yield return null;
		
	}

	
	void OnMouseDown () {
		//box=GetHashCode();
//		//Debug.Log("turn"+turn);
//		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
//		if( Physics.Raycast( ray, out hit, 100 ) )
//		{
//			Debug.Log( hit.transform.gameObject.tag );
//		}
//		
//		if(hit.transform.gameObject.tag=="2player"&&engage==false)
//		{
//			engage=true;
//			StartCoroutine(play2.gamer2players());
//		}
//		else if(play2.turns==2){
//			keepColor=true;
//			play2.turns=-1;
//			yield return StartCoroutine(getty ());
//			play2.realbox=realbox;
//			play2.mouseclicked=true;
//			Debug.Log("here2");
//
//			plays.previous=plays.current;
//			plays.current=hit.transform.gameObject.tag;
//			
//		}
//		else if( hit.transform.gameObject.tag=="1player"&&engage==true)
//		{
//			Debug.Log("Do you want to restart game?");
//		}
//		else Debug.Log("Not your turn!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
