using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {

//	public bool mouseclicked = false;
//	//private Player1 auth;
//	//public bool splaying = false; // still playing
//	public int turns=-1;
//	public int realbox;
//	private Player plays;
//	//private PlayerHole1 gethole;
//	private int Winner;
//	public bool engage=false;
//	int steps=0;
//	private GUIS info;
//	public int [,]A=new int[2,6];
//	public int []SeedsWon=new int[2];
//	
//	// Use this for initialization
//	void Start () {
//		plays= FindObjectOfType(typeof(Player))as Player;
//		info = FindObjectOfType (typeof(GUIS))as GUIS;
//	}
//	
//	IEnumerator ShowMessage (string message, float delay) {
//		info.GetComponent<GUIText>().text = message;
//		info.GetComponent<GUIText>().enabled = true;
//		yield return new WaitForSeconds(delay);
//		info.GetComponent<GUIText>().enabled = false;
//	}
//	
//	
//	public IEnumerator gamer()
//	{
//		plays.selection = "2 players";
//		//StartCoroutine(wait());
//		while(plays.waiting==false){yield return new WaitForSeconds(0.3f);}
//		while(plays.CheckGame(0,SeedsWon) ==2 || plays.CheckGame(0,SeedsWon) ==10 )
//		{
//			
//			
//			steps+=1;
//			turns=1;
//			//splaying=true;
//			while(mouseclicked==false){yield return new WaitForSeconds(0.3f);}
//			mouseclicked=false;
////			yield return StartCoroutine(plays.GamePlay(0,realbox,A,SeedsWon));
//			
//			if(plays.AddGame(A) !=0){
//				turns =2;
//				while(mouseclicked==false){yield return new WaitForSeconds(0.3f);}
//				mouseclicked=false;
//				yield return StartCoroutine(plays.GamePlay(1,realbox,A,SeedsWon));
//			}
//			
//		}
//		
//		if(plays.CheckGame(0,SeedsWon) == 1)StartCoroutine(ShowMessage(" Player 1 wins!!! ",4));
//		else if(plays.CheckGame(0,SeedsWon) == -1) StartCoroutine(ShowMessage(" Player 2 wins!!! :p ",4));
//		else StartCoroutine(ShowMessage("game is a Draw! ",4));
//		
//	}
//	
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
}
