using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class GameSelectionOnePlayer :  MonoBehaviour {
	private Player plays;
	//private GameNetworkManager gameNetworkManager;
	private int Winner;
	int steps=0;
	public int Complayer;
	//[SyncVar]
	public static int []A=new int[12];
	public static int []SeedsWon=new int[2];
	public static int[]phouse = new int[2];
	//[SyncVar]
	public bool mouseclicked = false;
	//[SyncVar]
	public static int turns= 1;
	public Vector3 hitpoint;

	public bool VisibleHand;
	public int realbox;
	private movehand1 hand;
	private Playerhand phand;
	string posnumber;
	//[SyncVar]
	private GameObject[] Cstones;
	public int choice = 0;
	public Camera[] mainCamera;
	public bool engage=false;
	private GUIS info;
	SetUp setup;
	Vector3 temp;
	//NetworkView networkView;

	IEnumerator reset(int NumHouseP1, int NumHouseP2){
		hand.h = 1;
		hand.i = 0;
		hand.j = 5;
		engage = true;
		plays.NewGame(ref A,ref SeedsWon);
		hand.movement (true);

		phouse [0] = NumHouseP1;
		phouse [1] = NumHouseP2;
		engage = false;
		yield return null;
	}
	// Use this for initialization
	void Start() {
		plays= FindObjectOfType(typeof(Player))as Player;
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		phand = FindObjectOfType (typeof(Playerhand))as Playerhand;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
		StartCoroutine (reset (6,6));
		info = FindObjectOfType (typeof(GUIS))as GUIS;
		StartCoroutine (gamer1player ());
	}



	IEnumerator ShowMessage (string message, float delay) {
		info.GetComponent<GUIText>().text = message;
		info.GetComponent<GUIText>().enabled = true;
		yield return new WaitForSeconds(delay);
		info.GetComponent<GUIText>().enabled = false;
	}

	IEnumerator WhoPlaysFirst1Player(int player){
		if (player == 0) {
			if(plays.CanPlay(0,ref A, ref phouse) == true){
				steps += 1;
				turns = 1;
				setup.turn.text = "You";
				if (plays.AddGame (A) != 0) {
					while (mouseclicked==false) {
						yield return new WaitForSeconds (0.3f);
					}
					mouseclicked = false;
					turns = 2;
					engage =  true;
					yield return StartCoroutine (plays.GamePlay (0, int.Parse (posnumber) - 1, A, SeedsWon, phouse));
					engage = false;
				}
			}
			if(plays.CanPlay(1,ref A, ref phouse) == true){
				Complayer = 2;
				//splaying=true;
				setup.turn.text = "Computer";
				if (plays.AddGame (A) != 0) {
					while (plays.addrequests>0) {
						yield return new WaitForSeconds (0.3f);
					}
					engage = true;
					yield return StartCoroutine (plays.Player2 (A, SeedsWon, phouse));
					engage =  false;
				}
			}
		} else {
			if(plays.CanPlay(1,ref A, ref phouse) == true){
				Complayer = 2;
				//splaying=true;
				setup.turn.text = "Computer";
				if (plays.AddGame (A) != 0) {
					while (plays.addrequests>0) {
						yield return new WaitForSeconds (0.3f);
					}
					engage = true;
					yield return StartCoroutine (plays.Player2 (A, SeedsWon, phouse));
					engage = false;
				}
			}
			if(plays.CanPlay(0,ref A, ref phouse) == true){
				steps += 1;
				turns = 1;
				setup.turn.text = "You";
				if (plays.AddGame (A) != 0) {
					while (mouseclicked==false) {
						yield return new WaitForSeconds (0.3f);
					}
					mouseclicked = false;
					turns = 2;
					engage = true;
					yield return StartCoroutine (plays.GamePlay (0, int.Parse (posnumber) - 1, A, SeedsWon, phouse));
					engage = false;
				}
			}
		}
	}



	public IEnumerator gamer1player()
	{

		int whoplaysfirst = 0;
		int rounds = 1;
		int i=0;
		setup.round.text = "Round: "+rounds.ToString ();
		setup.player1house.text = "(" + phouse [0].ToString () + ")";
		setup.player2house.text = "(" + phouse [1].ToString () + ")";
		while(phouse[0]!= 0 && phouse[1] != 0)
		{
			yield return StartCoroutine (WhoPlaysFirst1Player(whoplaysfirst));
			if(plays.CheckGame(0,ref SeedsWon) !=2){
				if (plays.CheckGame(0,ref SeedsWon) == 10){
					SeedsWon[0] += 4;
					SeedsWon[1] += 4;
				}
				int houseplayer1 = SeedsWon[0]/4;
				i = 0;
				Cstones = GameObject.FindGameObjectsWithTag("stone"+(13));
				while(i<Cstones.Length){    //clear the hole
					Destroy(Cstones[i].gameObject);
					i+=1;
				}
				i=0;
				Cstones = GameObject.FindGameObjectsWithTag("stone"+(14));
				while(i<Cstones.Length){    //clear the hole
					Destroy(Cstones[i].gameObject);
					i+=1;
				}
				yield return StartCoroutine (reset(houseplayer1, 12 - houseplayer1));
				rounds++;
				setup.player1Seed.text = "X 00";
				setup.player2Seed.text = "X 00";
				setup.round.text = "Round: "+rounds.ToString ();
				setup.player1house.text = "(" + phouse [0].ToString () + ")";
				setup.player2house.text = "(" + phouse [1].ToString () + ")";
				turns = (whoplaysfirst + 1) % 2 + 1;
				whoplaysfirst = (whoplaysfirst+1)%2;
			}

		}

		if(plays.CheckGame(0,ref SeedsWon) == 1)setup.turn.text = "Win!";
		else if(plays.CheckGame(0,ref SeedsWon) == 0) setup.turn.text = "Lose!";
		else setup.turn.text = "Draw!";
		Debug.Log ("finish");

	}



	public void getnearest(){
		GameObject closeobj = null;
		for(int i=0;i<12;i++){
			phand.nobj =  GameObject.FindGameObjectWithTag("points"+(i+1)) as GameObject;
			if(!closeobj){
				closeobj = phand.nobj;
			}
			if(Vector3.Distance(phand.targetPosition, phand.nobj.transform.position) <= Vector3.Distance(phand.targetPosition, closeobj.transform.position)){
				closeobj = phand.nobj;
			}
		}

		string tagposition = closeobj.tag;
		int hole = int.Parse (tagposition.Replace ("points", ""));
		Debug.Log (turns);
		if (turns == 1) {
			if (hole <= phouse [0] && A[hole - 1] != 0) {
				mouseclicked = true;
			}
		} else if (turns == 2 ) {
			if (hole > phouse [0] && A[hole - 1] != 0) {
				mouseclicked = true;
			}
		}
		posnumber = Regex.Replace(tagposition, @"\D", "");
	}

	void OnMouseDown(){
		if (engage == false) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //Convert mouse position to raycast
			RaycastHit hit; //Create a RaycastHit variable to store the hit data into
			if (Physics.Raycast (ray, out hit, 1000f)) { //If the user clicks the left mouse button and we hit an object with our raycast then
				phand.targetPosition = hit.point; //Store the hit position into the clicked position variable
			}
			getnearest ();

		} 


	}



}
