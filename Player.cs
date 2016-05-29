using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	private GameObject[] Arrays;
	private GameObject[] Cstones;
	private GameObject pholes;

	//[SyncVar]

	private GUIS info;
	private int getarea;
	private int DrawCount =1; 
	public float timer=0f;
	private int levels;
	private int boxmax=0;

	//[SyncVar]
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
	//private int choice;
	//public Color color;


	// Use this for initialization
	void Start() {
//		WonHoles= FindObjectsOfType(typeof(WonStones))as WonStones[];
//		Fill= FindObjectsOfType(typeof(Win1))as Win1[];
		info = FindObjectOfType (typeof(GUIS))as GUIS;
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		phand = FindObjectOfType (typeof(Playerhand))as Playerhand;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
		p1winhole = GameObject.FindWithTag ("Player1");
		p2winhole = GameObject.FindWithTag ("Player2");
		hand1ani = phand.rb2D.GetComponent<Animator>();
		hand2ani = hand.rb2D.GetComponent<Animator> ();
		//choice = SceneManager.choice;
	
	}

	/*IEnumerator waiter()
	{
		while(//waiting==false){yield return new WaitForSeconds(2.0f);}
	}*/

	//int Max(int [,]B,int []C,int player,int rec,int levels);// Max player

	public void NewGame(ref int []A,ref int []SeedsWon)                //new game - put 4 pearls in each hole 2 x 6 
	{
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
			hand1ani.SetTrigger("grab");
		} else {
			yield return StartCoroutine( hand.JMove (pholes.transform.position.x, pholes.transform.position.y, pholes.transform.position.z));
			hand2ani.SetTrigger("grab");
		}
		while(i<Cstones.Length){    //clear the hole
			Destroy(Cstones[i].gameObject);
			i+=1;
		}
		yield return null;
		//wait=true;
	}

	


	IEnumerator ShowMessage (string message, float delay) {
		info.GetComponent<GUIText>().text = message;
		info.GetComponent<GUIText>().enabled = true;
		yield return new WaitForSeconds(delay);
		info.GetComponent<GUIText>().enabled = false;
	}

	
	IEnumerator Addhole(int Gbox,int player)
	{
		pholes = GameObject.FindGameObjectWithTag("points"+(Gbox+1));
		if (player == 0) {
			hand1ani.SetTrigger("share");
			yield return StartCoroutine( phand.JMove (pholes.transform.position.x, pholes.transform.position.y, pholes.transform.position.z));
			phand.shoot (1f, (Gbox+1));
		} else {
			hand2ani.SetTrigger("share");
			yield return StartCoroutine(hand.JMove (pholes.transform.position.x, pholes.transform.position.y, pholes.transform.position.z));
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
			//phand.handpoly.enabled = true;
			yield return StartCoroutine (phand.JMove (winpos.x, winpos.y, winpos.z));
			yield return StartCoroutine (shooter(1, 13));
			addrequests--;
			if(addrequests < 1){
				hand1ani.SetTrigger("open");
				yield return StartCoroutine (phand.JMove (Playerhand.orig.x, Playerhand.orig.y, Playerhand.orig.z));
				//phand.handpoly.enabled =false;
			}
		} else {
			//hand.handpoly.enabled=true;
			yield return StartCoroutine (hand.JMove (winpos.x, winpos.y, winpos.z));
			yield return StartCoroutine (shooter(0, 14));
			addrequests--;
			if(addrequests < 1){
				hand2ani.SetTrigger("open");
				yield return StartCoroutine (hand.JMove (movehand1.orig.x, movehand1.orig.y, movehand1.orig.z));
				//hand.handpoly.enabled=false;
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


	IEnumerator EmptyHoles(int []A, int hand)                //remove all seeds
	{
		for(int i=0; i<12;i++)
		{
			if(A[i]>0){
				yield return StartCoroutine(clearhole(i,hand));
			}
			
			A[i]=0;
		}
		//	wait=true;
		yield return null;
		
	}

	bool AnalizeD(int []GameHoles,int []SeedsWon,int SIB,int owner, int box, int fseed)   //check the seeds in the box after a play. if up to four, the owner of the area takes it. SIB - Seeds In the Box
	{
		if(SIB == 4 && fseed!= 1)
		{
			SeedsWon[owner] +=4;
			GameHoles[box]=0;
			if(AddGame(GameHoles)==4)  // If only 8 seeds are remaining on the board, take all
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
	IEnumerator Analize(int [] A, int []SeedsWon,int SIB,int owner, int box,int fseed,int hand)   //check the seeds in the box after a play. if up to four, the owner of the area takes it. SIB - Seeds In the Box
	{
		if(SIB == 4 && fseed != 1)
		{
			SeedsWon[owner] +=4;
			yield return new WaitForSeconds(0.1f);
			//updateWonhole=true;
			yield return StartCoroutine(clearhole(box,hand));
			A[box]=0;
			StartCoroutine(AddWonStones(hand,owner));  // Add the stones won
			updateScore(ref owner,ref SeedsWon);
			if(AddGame(A)==4)  // If only 8 seeds are remaining on the board, take all
			{
				SeedsWon[owner] +=4;
				updateScore(ref owner,ref SeedsWon);
				yield return StartCoroutine(EmptyHoles(A,hand));
				yield return StartCoroutine(AddWonStones(hand,owner));
				FRound=true;
				//EmptyHoles(A);
			}
		}
	//	wait=true;
		yield return null;

	}


void GamePlayD(int player, int box,ref int []GameHoles,ref int []SeedsWon, ref int[] OwnedHoles)   // for decision without actually playing  
		
	{
		//Debug.Break();
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
			if(AnalizeD(GameHoles,SeedsWon,GameHoles[box],owner,box,seeds)==true)goto end1;
			seeds--;
			if(seeds == 0 && (GameHoles[box] > 1 && GameHoles[box] !=4))
			{
				cl++;
				if (cl>40)goto end1;
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

	end1:
		cl=0;
}


public IEnumerator GamePlay(int player, int box, int []GameHoles, int []SeedsWon, int []OwnedHoles)     
		
	{
		//Debug.Break();
		finalwait=false;
		int seeds,owner;
		seeds = GameHoles[box];
		GameHoles[box]=0;
		yield return StartCoroutine(clearhole(box,player));
		while(true)
		{
			box++;
			if (box > 11){
				box = 0;
			}
			GameHoles[box] +=1;
			yield return StartCoroutine( Addhole(box,player));
			if(box < OwnedHoles[0]){
				owner = 0;
			}else{
				owner = 1;
			}
			yield return StartCoroutine( Analize(GameHoles,SeedsWon,GameHoles[box],owner,box,seeds,(player+1)%2));
			if (FRound == true)goto end1;
			FRound=false;
			seeds--;
			if(seeds == 0 && (GameHoles[box] > 1 && GameHoles[box] !=4))
			{
				cl++;
				if (cl>40)goto end1;
				seeds = GameHoles[box];
				GameHoles[box] =0;
				yield return StartCoroutine(clearhole(box,player));
			}else if(GameHoles[box]==4){
				SeedsWon[player] +=4;
				updateScore(ref player,ref SeedsWon);
				GameHoles[box]=0;
				yield return StartCoroutine(clearhole(box,player));
				yield return StartCoroutine(AddWonStones(player,player));
				if(AddGame(GameHoles)==4)  // If only 8 seeds are remaining on the board, take all
				{
					SeedsWon[player] +=4;
					updateScore(ref player,ref SeedsWon);
					yield return StartCoroutine(EmptyHoles(GameHoles,player));
					yield return StartCoroutine(AddWonStones(player,player));
				}
				break;
			}else if (seeds == 0){
				break;
			}
		}

	end1:
			cl=0;
			finalwait=true;
			if (player == 0) {
				hand1ani.SetTrigger("open");
			yield return StartCoroutine( phand.JMove (Playerhand.orig.x, Playerhand.orig.y, Playerhand.orig.z));

			} else {
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
				if (DrawCount >= 10) return 10;
				
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

//	int Min(int [,]B,int []C, int me, int opp,int min,int rec,int levels,int mlevel) //Min player
//	{
//		int [,]B1=new int[2,6];
//		int []C1=new int[2];
//		
//		int min1=100;
//		int mini2=100;
//		int oppSeed=0;
//		//cout<<"2";
//		if(rec<6 && levels>=0)
//		{
//			copyB(B,B1);
//			copyC(C,C1);
//			oppSeed=C1[opp];
//			min1=Min(B,C1,me,opp,min,(rec+1),levels,mlevel);
//			if(Possible(opp,rec,B1)==true){
//				//cout<<endl;
//				//Show(B1);
//				//cout<<"ga";
//				GamePlayD(opp,rec,B1,C1);
//				//cout<<"me";
//				//Show(B1);
//				if((C1[0]+C1[1])<48){
//					if(levels>0&&(C1[0]+C1[1])<48)mini2=Max(B1,C1,me,0,(levels-1),mlevel);
//					gcls++;
//					if(levels==0){mini2=hf(me,opp,C1,oppSeed);}
//					if(min1>mini2){
//						min1=mini2;
//						//boxmin = rec;
//						
//					}
//				}else min1=100;	}
//			//cout<<endl;
//			//cout<<gcls<<endl;
//		}
//		//	cout<<rec<<" "<<levels<<" "<<"2";
//		return min1;
//	}

	int EndCheck(int x, int y)
	{

		if((x+y)>=40){
			 if ((x-y)>=0)return -40;
		}
		return x-y;
		
	}

	int hf(int me,int opp,ref int []C)
	{
		/*int mini2=C1[(me+1)%2];
		mini2=mini2+EndCheck(C1[(me+1)%2],C1[(opp+1)%2]);
		//mini2=mini2-(C1[opp]-oppSeed);
		//M=M+EndCheck(C1[me],C1[opp]);
		if(C1[(opp+1)%2]>oppSeed)mini2=mini2-(C1[(opp+1)%2]-oppSeed);
		return mini2;*/
		return EndCheck(C[me],C[opp]);
	}



//	int Max(int [,]B,int []C,int player,int rec,int levels,int mlevel)// Max player
//	{	
//		int me=player;
//		int opp;
//
//		//int n=0;
//		int min=0;
//		int max=-100,M=-100;
//		int [,]B1=new int[2,6];
//		int []C1=new int[2];
//		//int levels1=levels;
//		if (me==0)opp=1;
//		else opp=0;
//		//int oppSeed;
//		//n=n+1;
//		
//		if(rec<6 && levels>=0)
//		{
//			copyB(B,B1);
//			copyC(C,C1);
//		//	oppSeed=C1[opp];
//			max=Max(B1,C1,me,(rec+1),levels,mlevel);
//			if(Possible(me,rec,B1)==true){
//				GamePlayD(me,rec,B1,C1);
//				//	Show(B1);
//				//if(Possible(me,rec,B1)==true){
//				M=Min(B1,C1,me,opp,min,0,(levels),mlevel);
//				//M=M+EndCheck(C1[me],C1[opp]);
//				//if(C1[opp]>oppSeed)M=M-(C1[opp]-oppSeed);
//				gcls++;
//				if(me==0){
//					if(max<M){max=M;if(levels==mlevel)boxmax=rec;}}
//				else
//				{if(max<=M){max=M;if(levels==mlevel)boxmax=rec;}}
//				//	}
//			}
//			/*else {box=boxmin;}*/
//			
//		}
//		
//		return max;
//	}
//	
//	void MinMax(int [,]A, int []C,int player,int level)// Minimax setup
//	{
//		int [,]B=new int [2,6];
//		for(int i=0; i<2;i++)
//		{
//			for (int j=0; j<6; j++)
//			{
//				B[i,j]=A[i,j];
//			}
//		}
//		
//		Max(B,C,player,0,level,level);
//
//	}
//
//	int Heauristic(int [,]B,int []C,int player,int rec)// heauristic player
//	{	
//		//int box=0;
//		//int n=0;
//		//int min=0;
//		int h=-100,h1=-100;
//		int [,]B1=new int[2,6];
//		int []C1=new int[2];
//		int opp=(1+player)%2;
//		int oppSeed=0;
//		//n=n+1;
//		
//		if(rec<6)
//		{
//			copyB(B,B1);
//			copyC(C,C1);
//			oppSeed=C1[opp];
//			h=Heauristic(B1,C1,player,(rec+1));
//			if(Possible(player,rec,B1)==true){
//				GamePlay(player,rec,B1,C1);
//				h1=hf(player,opp,C1,oppSeed);
//				
//				if(h<h1){h=h1;boxmax=rec;}
//				
//			}
//			/*else {box=boxmin;}*/
//			
//		}
//		
//		return h;
//	}

	int Betaplay(ref int []GameHoles,ref int []WonSeeds,int player,int levels, int mlevel, int alpha, int beta, int[] OwnedHoles){
		if (levels < 0 || ((WonSeeds [0] + WonSeeds [1]) >= 44)) {
			return WonSeeds [player];
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
		for (int i=0; i < OwnedHoles[(player+1)%2]; i++) {
			if (Possible((oppstarthouse + i), GameHoles) && beta > alpha){
				GamePlayD((player+1)%2,oppstarthouse + i,ref GameHoles, ref WonSeeds,ref OwnedHoles);
				beta = Mathf.Min(beta,Betaplay(ref GameHoles, ref WonSeeds,player,levels,mlevel,alpha,beta,OwnedHoles));
			}
			copyGameHoles (ref tempGameHoles,ref GameHoles);
			copyWonSeeds (ref tempWonSeeds, ref WonSeeds);
		}
		return beta;
	}

	int Alphaplay(ref int []GameHoles,ref int []WonSeeds,int player,int levels, int mlevel,int alpha, int beta, int[] OwnedHoles, ref int starthouse){
		if (levels < 0 || ((WonSeeds [0] + WonSeeds [1]) >= 44)) {
			return WonSeeds [player];
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
		copyGameHoles (ref GameHoles,ref tempGameHoles);
		copyWonSeeds (ref WonSeeds, ref tempWonSeeds);
		int starthouse = OwnedHoles[0];
		for (int i=0; i < OwnedHoles[1]; i++) {
			M = alpha;
			if (Possible((starthouse + i),GameHoles) && beta > alpha){
				GamePlayD(player,(starthouse + i),ref GameHoles,ref WonSeeds,ref OwnedHoles);
				alpha = Mathf.Max(alpha,Alphaplay(ref GameHoles, ref WonSeeds,player,levels,mlevel,alpha,beta,OwnedHoles,ref starthouse));
				if(levels == mlevel){
					if (alpha > M){
						boxmax = starthouse + i;
					}
				}
			}
			copyGameHoles (ref tempGameHoles,ref GameHoles);
			copyWonSeeds (ref tempWonSeeds, ref WonSeeds);
		}
		return alpha;
	}
	
	void AlphaBeta(ref int []GameHoles,ref  int []WonSeeds,int player,int level,int[] OwnedHoles)// Minimax setup
	{
		
		int [] tempGameHoles = new int[12];
		int [] tempWonSeeds = new int[2];
		copyGameHoles (ref GameHoles, ref tempGameHoles);
		copyWonSeeds (ref WonSeeds,ref  tempWonSeeds);
//		int []B=new int[12];
//		for(int i=0; i<12;i++)
//		{
//			B[i]=A[i];
//		}
		//poper(level,MinL,MaxL);
		//poper(level,MinL,MaxL);
		Alpha(tempGameHoles,tempWonSeeds,player,level,level,-100,100,OwnedHoles);

	}

  public IEnumerator Player1( int []A, int []Seeds, int []OwnedHoles)
	{
		//while(finalwait==false){yield return new WaitForSeconds(0.3f);}
		//finalwait=false;

	//	int ranc=0;
	/*random:	int box = Random.Range(0,5);
		if(ranc ==10)goto giveup1;
		if(Possible(0,box,A)==false){ranc++; goto random;}*/
		AlphaBeta(ref A,ref Seeds,0,3,OwnedHoles);
		//Heauristic (A, Seeds, 0, 0);
		//MinMax (A, Seeds, 0, 3);
//		setc.getShole (boxmax + 1);
		yield return StartCoroutine(GamePlay(0,boxmax, A,Seeds, OwnedHoles));
		boxmax=0;
	//giveup1: 
			yield return null;
	}
	
	public IEnumerator Player2( int []A, int []Seeds,  int []OwnedHoles)
	{

		/*int ranc=0;
	random:	int box = Random.Range(0,5);
		if(ranc ==10)goto giveup2;
		if(Possible(1,box,A)==false){ranc++; goto random;}
		yield return StartCoroutine(GamePlay( 1,box,A,Seeds));
	giveup2: */
       AlphaBeta (ref A, ref Seeds, 1, 5,OwnedHoles);
			//yield return null;
//		setc.getShole(boxmax + 1);
		yield return StartCoroutine(GamePlay(1,boxmax, A, Seeds, OwnedHoles));
		boxmax=0;

		yield return null;
	}


	

	// Update is called once per frame
	void Update ()
	{
//		if (!isLocalPlayer)
//			return;
	}
}
