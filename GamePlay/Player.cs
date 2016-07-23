using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	private GameObject[] Arrays;
	private GameObject[] Cstones;
	private GameObject pholes;


	private int getarea;
	private static int DrawCount; 
	public float timer=0f;
	private int levels;
	public int boxmax=0;

	public bool waiting=false;
	public bool wait=false;
	public bool finalwait=true;
	public bool FRound=false;

	public string selection=null;
	public Color color;
	private int cl=0;
	public string current=null;
	public string previous=null;
	private Playerhand phand;
	public movehand1 hand;
	private Animator hand1ani;
	private Animator hand2ani;
	private GameObject p1winhole;
	private GameObject p2winhole;
	public Vector3 originh1, originh2;
	bool addwstone = true;
	public int addrequests =0;
	SetUp setup;
	int previoustag = 0;
	AudioSource audioSource;



	// Use this for initialization
	void Start() {
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		phand = FindObjectOfType (typeof(Playerhand))as Playerhand;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
		p1winhole = GameObject.FindWithTag ("Player1");
		p2winhole = GameObject.FindWithTag ("Player2");
		hand1ani = phand.GetComponent<Animator>();
		hand2ani = hand.GetComponent<Animator> ();
		if (previoustag != 0) {
			GameObject prevhole = GameObject.FindGameObjectWithTag ("points" + previoustag);
			prevhole.GetComponent<Renderer> ().enabled = false;
		}
		audioSource = UnityEngine.Object.FindObjectOfType<AudioSource>();
		if (PlayerPrefs.GetInt ("MancalaSound", 1) == 1) {
			audioSource.UnPause();
		} else {
			audioSource.Pause();
		}
	
	}


	public void NewGame(ref int []A,ref int []SeedsWon)                //new game - put 4 pearls in each hole 2 x 6 
	{
		DrawCount =0;
		for(int i=0; i<12;i++)
		{
			A[i]=4;
		
		}
		for(int i=0; i<2;i++)
		{
			SeedsWon[i]=0;
			
		}
	}

	 IEnumerator clearhole(int Gbox, int player)
	{
		int i=0;
		Cstones = GameObject.FindGameObjectsWithTag("stone"+(Gbox+1));
		pholes = GameObject.FindGameObjectWithTag("points"+(Gbox+1));
		if (player == 0) {
			yield return StartCoroutine( phand.JMove (pholes.transform.position.x, pholes.transform.position.y, pholes.transform.position.z));
			hand1ani.ResetTrigger("open");
			hand1ani.SetTrigger("grab");
		} else {
			yield return StartCoroutine( hand.JMove (pholes.transform.position.x, pholes.transform.position.y, pholes.transform.position.z));
			hand2ani.ResetTrigger("open");
			hand2ani.SetTrigger("grab");
		}
		while(i<Cstones.Length){    //clear the hole
			Destroy(Cstones[i].gameObject);
			i+=1;
		}
		yield return null;
	}




	
	IEnumerator Addhole(int Gbox,int player)
	{
		pholes = GameObject.FindGameObjectWithTag("points"+(Gbox+1));
		if (player == 0) {
			hand1ani.ResetTrigger("open");
			hand1ani.SetTrigger("share");
			yield return StartCoroutine( phand.JMove (pholes.transform.position.x, pholes.transform.position.y + 4f, pholes.transform.position.z));
			phand.shoot (1f, (Gbox+1));
		} else {
			hand2ani.ResetTrigger("open");
			hand2ani.SetTrigger("share");
			yield return StartCoroutine(hand.JMove (pholes.transform.position.x, pholes.transform.position.y + 4f, pholes.transform.position.z));
			hand.shoot (1f, (Gbox+1));
		}
		yield return new WaitForSeconds(0.3f);
	}

	IEnumerator AddWonStones (int player, int hole) {
		addrequests++;
		while(addwstone==false){yield return new WaitForSeconds(0.3f);}
		addwstone = false;
		Vector3 winpos;
		if (hole == 0) {
			winpos = p1winhole.transform.position;
		} else {
			winpos = p2winhole.transform.position;
		}
		if (player == 0) {
			yield return StartCoroutine (phand.JMove (winpos.x, winpos.y, winpos.z));
			yield return StartCoroutine (shooter(1, 13));
			addrequests--;
			if(addrequests < 1){
				hand1ani.ResetTrigger("grab");
				hand1ani.ResetTrigger("share");
				hand1ani.SetTrigger ("open");
				yield return StartCoroutine (phand.JMove (Playerhand.orig.x, Playerhand.orig.y, Playerhand.orig.z));
			}
		} else {
			yield return StartCoroutine (hand.JMove (winpos.x, winpos.y, winpos.z));
			yield return StartCoroutine (shooter(0, 14));
			addrequests--;
			if(addrequests < 1){
				hand2ani.ResetTrigger("grab");
				hand2ani.ResetTrigger("share");
				hand2ani.SetTrigger ("open");
				yield return StartCoroutine (hand.JMove (movehand1.orig.x, movehand1.orig.y, movehand1.orig.z));
			}
		}
		addwstone = true;

	}

	IEnumerator shooter(int theHand, int label){
		int counter = 0;
		while (counter < 4) {
			if(theHand == 0){
				hand.shoot (0.0005f, label);
			}else{
				phand.shoot (0.0005f, label);
			}
			timer = 0.0f;
			counter = counter + 1;
			yield return new WaitForSeconds (0.1f);		
		}
	}

	public int AddGame(int []A)                //add game -check to  put 8 pearls in hole at last round 
	{
		int add=0;
			for(int i=0; i<12;i++)
			{
				add+=A[i];
			}
		return add;
	}

	void EmptyHolesD(ref int []GameHoles)                //remove all seeds
	{
		for(int i=0; i<12;i++)
		{

			GameHoles[i]=0;
		}
	
	}


	IEnumerator EmptyHoles( int hand)                //remove all seeds
	{
		if (GameSceneManager.selection == "1 Player") {
			for (int i = 0; i < 12; i++) {
				if (GameSelectionOnePlayer.A [i] > 0) {
					yield return StartCoroutine (clearhole (i, hand));
				}
			
				GameSelectionOnePlayer.A [i] = 0;
			}
		} else {
			for (int i = 0; i < 12; i++) {
				if (GameSelection.A [i] > 0) {
					yield return StartCoroutine (clearhole (i, hand));
				}

				GameSelection.A [i] = 0;
			}
		}
		yield return null;
		
	}

	bool AnalizeD(int []GameHoles,int []SeedsWon,int SIB,int owner, int box, int fseed)   //check the seeds in the box after a play. if up to four, the owner of the area takes it. SIB - Seeds In the Box
	{
		if(SIB == 4 && fseed!= 1)
		{
			SeedsWon[owner] +=4;
			GameHoles[box]=0;
			if((AddGame(GameHoles) + + (fseed-1))==4)  // If only 8 seeds are remaining on the board, take all
			{
				SeedsWon[owner] +=4;
				EmptyHolesD(ref GameHoles);
				return true;
			}
		}
		return false;
	}
	void updateScore(ref int player,ref int []SeedsWon){
		if (player == 0) {
			setup.player1Seed.text = "X" + SeedsWon [0].ToString ();
		} else {
			setup.player2Seed.text = "X" + SeedsWon [1].ToString ();
		}
	}
	IEnumerator Analize(int SIB,int owner, int box,int fseed,int hand)   //check the seeds in the box after a play. if up to four, the owner of the area takes it. SIB - Seeds In the Box
	{
		if (GameSceneManager.selection == "1 Player") {
			if (SIB == 4 && fseed != 1) {
				GameSelectionOnePlayer.SeedsWon [owner] += 4;
				yield return new WaitForSeconds (0.1f);
				//updateWonhole=true;
				yield return StartCoroutine (clearhole (box, hand));
				GameSelectionOnePlayer.A [box] = 0;
				StartCoroutine (AddWonStones (hand, owner));  // Add the stones won
				updateScore (ref owner, ref GameSelectionOnePlayer.SeedsWon);
				if ((AddGame (GameSelectionOnePlayer.A) + (fseed-1)) == 4) {  // If only 8 seeds are remaining on the board, take all
					GameSelectionOnePlayer.SeedsWon [owner] += 4;
					updateScore (ref owner, ref GameSelectionOnePlayer.SeedsWon);
					yield return StartCoroutine (EmptyHoles (hand));
					yield return StartCoroutine (AddWonStones (hand, owner));
					FRound = true;
					//EmptyHoles(A);
				}
			}
		} else {
			if (SIB == 4 && fseed != 1) {
				GameSelection.SeedsWon [owner] += 4;
				yield return new WaitForSeconds (0.1f);
				yield return StartCoroutine (clearhole (box, hand));
				GameSelection.A [box] = 0;
				StartCoroutine (AddWonStones (hand, owner));  // Add the stones won
				updateScore (ref owner, ref GameSelection.SeedsWon);
				if ((AddGame (GameSelection.A) + (fseed-1)) == 4) {  // If only 8 seeds are remaining on the board, take all
					GameSelection.SeedsWon [owner] += 4;
					updateScore (ref owner, ref GameSelection.SeedsWon);
					yield return StartCoroutine (EmptyHoles (hand));
					yield return StartCoroutine (AddWonStones (hand, owner));
					FRound = true;
				}
			}
		}
		yield return null;

	}


void GamePlayD(int player, int box,ref int []GameHoles,ref int []SeedsWon, ref int[] OwnedHoles)   // for decision without actually playing  
		
	{
		int seeds,owner;
		seeds = GameHoles[box];
		GameHoles[box]=0;
		while(true)
		{
				box++;
				if (box > 11){
					box = 0;
				}
				GameHoles[box] +=1;
				if(box < OwnedHoles[0]){
					owner = 0;
				}else{
					owner = 1;
				}
			if(AnalizeD(GameHoles,SeedsWon,GameHoles[box],owner,box,seeds)==true)goto end2;
			seeds--;
			if(seeds == 0 && (GameHoles[box] > 1 && GameHoles[box] !=4))
			{
				cl++;
				if (cl>40)goto end2;
				seeds = GameHoles[box];
				GameHoles[box] =0;
			}else if(GameHoles[box]==4){
				SeedsWon[player] +=4;
				GameHoles[box]=0;
				if(AddGame(GameHoles)==4)  // If only 8 seeds are remaining on the board, take all
				{
					SeedsWon[player] +=4;			
					EmptyHolesD(ref GameHoles);
				}
				break;
			}else if (seeds == 0){
				break;
			}
		}

	end2:
		cl=0;
}


public IEnumerator GamePlay(int player, int box)     
		
	{
		GameObject hole = GameObject.FindGameObjectWithTag("points"+(box+1));
		hole.GetComponent<Renderer> ().enabled = true;
		if (previoustag != 0) {
			GameObject prevhole = GameObject.FindGameObjectWithTag ("points" + previoustag);
			prevhole.GetComponent<Renderer> ().enabled = false;
		}
		previoustag = box+1;
		finalwait=false;
		int seeds,owner;
		if (GameSceneManager.selection == "1 Player") {
			seeds = GameSelectionOnePlayer.A [box];
			GameSelectionOnePlayer.A [box] = 0;
			yield return StartCoroutine (clearhole (box, player));
			while (true) {
				box++;
				if (box > 11) {
					box = 0;
				}
				GameSelectionOnePlayer.A [box] += 1;
				yield return StartCoroutine (Addhole (box, player));
				if (box < GameSelectionOnePlayer.phouse [0]) {
					owner = 0;
				} else {
					owner = 1;
				}
				yield return StartCoroutine (Analize (GameSelectionOnePlayer.A [box], owner, box, seeds, (player + 1) % 2));
				if (FRound == true)
					goto end1;
				FRound = false;
				seeds--;
				if (seeds == 0 && (GameSelectionOnePlayer.A [box] > 1 && GameSelectionOnePlayer.A [box] != 4)) {
					cl++;
					if (cl > 40)
						goto end1;
					seeds = GameSelectionOnePlayer.A [box];
					GameSelectionOnePlayer.A [box] = 0;
					yield return StartCoroutine (clearhole (box, player));
				} else if (GameSelectionOnePlayer.A [box] == 4) {
					GameSelectionOnePlayer.SeedsWon [player] += 4;
					updateScore (ref player, ref GameSelectionOnePlayer.SeedsWon);
					GameSelectionOnePlayer.A [box] = 0;
					yield return StartCoroutine (clearhole (box, player));
					yield return StartCoroutine (AddWonStones (player, player));
					if (AddGame (GameSelectionOnePlayer.A) == 4) {  // If only 8 seeds are remaining on the board, take all
						GameSelectionOnePlayer.SeedsWon [player] += 4;
						updateScore (ref player, ref GameSelectionOnePlayer.SeedsWon);
						yield return StartCoroutine (EmptyHoles (player));
						yield return StartCoroutine (AddWonStones (player, player));
					}
					break;
				} else if (seeds == 0) {
					break;
				}
			}
		} else {
			seeds = GameSelection.A [box];
			GameSelection.A [box] = 0;
			yield return StartCoroutine (clearhole (box, player));
			while (true) {
				box++;
				if (box > 11) {
					box = 0;
				}
				GameSelection.A [box] += 1;
				yield return StartCoroutine (Addhole (box, player));
				if (box < GameSelection.phouse [0]) {
					owner = 0;
				} else {
					owner = 1;
				}
				yield return StartCoroutine (Analize (GameSelection.A [box], owner, box, seeds, (player + 1) % 2));
				if (FRound == true)
					goto end1;
				FRound = false;
				seeds--;
				if (seeds == 0 && (GameSelection.A [box] > 1 && GameSelection.A [box] != 4)) {
					cl++;
					if (cl > 40)
						goto end1;
					seeds = GameSelection.A [box];
					GameSelection.A [box] = 0;
					yield return StartCoroutine (clearhole (box, player));
				} else if (GameSelection.A [box] == 4) {
					GameSelection.SeedsWon [player] += 4;
					updateScore (ref player, ref GameSelection.SeedsWon);
					GameSelection.A [box] = 0;
					yield return StartCoroutine (clearhole (box, player));
					yield return StartCoroutine (AddWonStones (player, player));
					if (AddGame (GameSelection.A) == 4) {  // If only 8 seeds are remaining on the board, take all
						GameSelection.SeedsWon [player] += 4;
						updateScore (ref player, ref GameSelection.SeedsWon);
						yield return StartCoroutine (EmptyHoles (player));
						yield return StartCoroutine (AddWonStones (player, player));
					}
					break;
				} else if (seeds == 0) {
					break;
				}
			}
		}

	end1:
			cl=0;
			FRound = false;
			finalwait=true;
			if (player == 0) {
				hand1ani.ResetTrigger("grab");
				hand1ani.ResetTrigger("share");
				hand1ani.SetTrigger("open");
				yield return StartCoroutine( phand.JMove (Playerhand.orig.x, Playerhand.orig.y, Playerhand.orig.z));

			} else {
				hand2ani.ResetTrigger("grab");
				hand2ani.ResetTrigger("share");
				hand2ani.SetTrigger("open");
			yield return StartCoroutine(hand.JMove (movehand1.orig.x, movehand1.orig.y, movehand1.orig.z));
			}
	}

	public int CheckGame(int player,ref int []SeedsWon)  // check if draw, win or lose
	{
		if((SeedsWon[0] + SeedsWon[1])>=40)
		{
			if(((SeedsWon[0] + SeedsWon[1]) == 40 ))    //Max number of times to be considered before the game ends =10
			{
				DrawCount++;
				if (DrawCount >= 100) return 10;
				
			}else if((SeedsWon[0] + SeedsWon[1]) > 40 && (SeedsWon[player] > SeedsWon[(player+1)%2]))   // If Player has higher seeds
			{
				return 1;
			}else  // If Opponent has higher seeds
			{
				return 0;
			}
		}

		return 2;
	}
	
public	bool CanPlay(int player,ref int []GameHoles,ref int []phouse)
	{
		if (player == 0) {
			for(int i = 0; i< phouse[0]; i++){
				if(GameHoles[i] != 0){
					return true;
				}
			}

		} else {
			for(int i = phouse[0]; i < 12; i++){
				if(GameHoles[i] != 0){
					return true;
				}
			}
		}
		return false;
	}

	bool Possible(int column,int []A)
	{
		if(A[column] >0)
		{
			return true;
		}else{
			return false;
		}
	}

	void copyGameHoles(ref int []from,ref int []to)
	{
		for(int i=0; i<12;i++)
		{
			to[i] = from[i];
		}
	}

	void copyWonSeeds(ref int []from,ref int []to)
	{
		for(int i=0; i<2;i++)
		{
			to[i]=from[i];
		}
	}


	int EndCheck(int x, int y)
	{

		if((x+y)>=40){
			 if ((x-y)>=0)return -40;
		}
		return x-y;
		
	}
		


	int Betaplay(ref int []GameHoles,ref int []WonSeeds,int player,int levels, int mlevel, int alpha, int beta, int[] OwnedHoles){
		if (levels < 0 || ((WonSeeds [0] + WonSeeds [1]) >= 44)) {
			return (WonSeeds [player] + levels/2);
		} else {
			int value = Alpha(GameHoles,WonSeeds,player,(levels - 1),mlevel,alpha, beta,OwnedHoles);
			return value;
		}
	}

	
	int Beta(int []GameHoles,int []WonSeeds, int player,int levels,int mlevel,int alpha, int beta, int []OwnedHoles, int starthouse) //Min player
	{
		int oppstarthouse;
		oppstarthouse = 0;
		int []tempGameHoles = new int[12];
		int []tempWonSeeds = new int[2];
		copyGameHoles (ref GameHoles,ref tempGameHoles);
		copyWonSeeds (ref WonSeeds, ref tempWonSeeds);
		if (CanPlay ((player + 1) % 2,ref GameHoles,ref OwnedHoles)) {
			for (int i = 0; i < OwnedHoles [(player + 1) % 2]; i++) {
				if (Possible ((oppstarthouse + i), GameHoles) && beta > alpha) {
					GamePlayD ((player + 1) % 2, oppstarthouse + i, ref GameHoles, ref WonSeeds, ref OwnedHoles);
					beta = Mathf.Min (beta, Betaplay (ref GameHoles, ref WonSeeds, player, levels, mlevel, alpha, beta, OwnedHoles));
				}
				copyGameHoles (ref tempGameHoles, ref GameHoles);
				copyWonSeeds (ref tempWonSeeds, ref WonSeeds);
			}
		}else if(CanPlay(player,ref GameHoles,ref OwnedHoles) && levels > 1) {
			if (beta > alpha) {
					beta = Mathf.Min (beta, Betaplay (ref GameHoles, ref WonSeeds, player, levels, mlevel, alpha, beta, OwnedHoles));
				}
			copyGameHoles (ref tempGameHoles, ref GameHoles);
			copyWonSeeds (ref tempWonSeeds, ref WonSeeds);
		}
		return beta;
	}

	int Alphaplay(ref int []GameHoles,ref int []WonSeeds,int player,int levels, int mlevel,int alpha, int beta, int[] OwnedHoles, ref int starthouse){
		if (levels < 0 || ((WonSeeds [0] + WonSeeds [1]) >= 44)) {
			return (WonSeeds [player]  + levels/2);
		} else {
			int value = Beta(GameHoles,WonSeeds,player,levels,mlevel,alpha,beta,OwnedHoles, starthouse);
			return value;
		}
	}
	
	int Alpha(int []GameHoles,int []WonSeeds,int player,int levels, int mlevel,int alpha, int beta, int[] OwnedHoles)// Max player
	{	
		int M = -100;
		int []tempGameHoles = new int[12];
		int []tempWonSeeds = new int[2];
		int opponent = (player + 1) % 2;
		copyGameHoles (ref GameHoles,ref tempGameHoles);
		copyWonSeeds (ref WonSeeds, ref tempWonSeeds);
		int starthouse = OwnedHoles[0];
		if (CanPlay (player,ref GameHoles,ref OwnedHoles)) {
			for (int i = 0; i < OwnedHoles [1]; i++) {
				M = alpha;
				if (Possible ((starthouse + i), GameHoles) && beta > alpha) {
					GamePlayD (player, (starthouse + i), ref GameHoles, ref WonSeeds, ref OwnedHoles);
					alpha = Mathf.Max (alpha, Alphaplay (ref GameHoles, ref WonSeeds, player, levels, mlevel, alpha, beta, OwnedHoles, ref starthouse));
					if (levels == mlevel) {
						if (alpha > M || (alpha == M && boxmax == 0)) {
							boxmax = starthouse + i;
						}
					}
				}
				copyGameHoles (ref tempGameHoles, ref GameHoles);
				copyWonSeeds (ref tempWonSeeds, ref WonSeeds);
			}
		} else if(CanPlay(opponent,ref GameHoles,ref OwnedHoles) && levels > 1) {
			if (beta > alpha) {
				alpha = Mathf.Max (alpha, Alphaplay (ref GameHoles, ref WonSeeds, player, levels, mlevel, alpha, beta, OwnedHoles, ref starthouse));
			}
			copyGameHoles (ref tempGameHoles, ref GameHoles);
			copyWonSeeds (ref tempWonSeeds, ref WonSeeds);
		}
		return alpha;
	}
	
	void AlphaBeta(ref int []GameHoles,int player,int level,int[] OwnedHoles)// Minimax setup
	{
		
		int [] tempGameHoles = new int[12];
		int [] tempWonSeeds = new int[2];
		copyGameHoles (ref GameHoles, ref tempGameHoles);
		Debug.Log ("level " + level);
		Alpha(tempGameHoles,tempWonSeeds,player,level,level,-100,100,OwnedHoles);

	}

  public IEnumerator Player1( int level)
	{
		AlphaBeta(ref GameSelectionOnePlayer.A,0,level,GameSelectionOnePlayer.phouse);
		yield return StartCoroutine(GamePlay(0,boxmax));
		boxmax=0;
	//giveup1: 
			yield return null;
	}
	
	public IEnumerator Player2(int level)
	{

		AlphaBeta (ref GameSelectionOnePlayer.A, 1, level,GameSelectionOnePlayer.phouse);
		yield return StartCoroutine(GamePlay(1,boxmax));
		boxmax=0;

		yield return null;
	}


	

	// Update is called once per frame
	void Update ()
	{

	}
}
