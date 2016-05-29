using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;



public class GameNetworkManager : NetworkManager
{
	//string gameName = "_giniprox_u_Mancala";
	float refreshingRequestLength = 2.0f;
	public bool waiting = true;
	public NetworkDiscovery discovery;
	int domain;
	// Use this for initialization
	void Start ()
	{
		GameSceneManager.JoinSuccess = false;
		GameSceneManager.choice = 1;
		discovery.Initialize();
		discovery.StartAsClient();
	}

	public void startServer(){
		GameSceneManager.Numconnection = 0;
		//networkAddress = "192.168.1.108";
		//StartClient ();
		GameSceneManager.choice = 1;
		//matchMaker.ListMatches (0, 20, "", OnMatchList);
		StartCoroutine( joinOrCreate ());
	}

	public IEnumerator joinOrCreate() {
		//waits for response from list matches
		yield return new WaitForSeconds (refreshingRequestLength);
		if (GameSceneManager.JoinSuccess == false) {
			//StopClient ();
			discovery.StopBroadcast();
			StartHost ();
		}
//		Debug.Log("Done waiting");
//		if (matchInfo == null) {
//			Debug.Log("Join or create?");
//			if (matches.Count == 0) {
//				Debug.Log("Create ");
//				GameSceneManager.choice = 0;
//				matchMaker.CreateMatch (gameName, matchSize, true, "", OnMatchCreate);
//			} else {
//				Debug.Log ("Joining "+ matches[0].name);
//				GameSceneManager.choice = 1;
//				matchMaker.JoinMatch (matches[0].networkId, "", OnMatchJoin);
//				waiting = false;
//				Debug.Log(GameSceneManager.choice);
//			}
		}
//		if (NetworkIdentity.isServer) {
//			Debug.Log("seerver");
//		} else {
//			Debug.Log("clienter");
//		}



	
	public override void OnStartHost()
	{
		Debug.Log ("OnPlayerConnected"+ numPlayers);
		waiting = false;
		GameSceneManager.choice = 0;
		Debug.Log(GameSceneManager.choice);
		//Debug.Log (Network.player.ipAddress);
		Debug.Log (networkPort);
		waiting = false;
		discovery.StartAsServer();

	}


	public override void OnServerConnect(NetworkConnection conn){
		Debug.Log ("Client is connected");
		GameSceneManager.Numconnection = GameSceneManager.Numconnection + 1;
		if (GameSceneManager.Numconnection > 2) {
			discovery.StopBroadcast();
		}
		//waiting = false;
	}


	public override void OnStopClient()
	{
		//discovery.StopBroadcast();
		//discovery.showGUI = true;
	}
//
//	public override void OnReceivedBroadcast (string fromAddress, string data)
//	{
//		NetworkManager.singleton.networkAddress = fromAddress;
//		NetworkManager.singleton.StartClient();
//	}
//	public void OnMatchJoin(JoinMatchResponse matchInfo)
//	{
//		OnMatchJoined (matchInfo);
//		if (matchInfo.success)
//		{
//			StartClient(new MatchInfo(matchInfo));
//			GameSceneManager.choice = 1;
//			waiting = false;
//			Debug.Log(GameSceneManager.choice);
//			//nodeid = matchInfo.nodeId;
//		}
//
//	}
//
////
//
//	public override void OnMatchCreate(CreateMatchResponse matchInfo)
//	{
//		if (matchInfo.success)
//		{
//			StartHost(new MatchInfo(matchInfo));
//			GameSceneManager.choice = 0;
//			Debug.Log(GameSceneManager.choice);
//			waiting = false;
//		//	netid = matchInfo.networkId;
//			//domain = matchInfo.d
//
//		}
//
//
//	}
//
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject player = (GameObject)Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	public override void OnClientConnect(NetworkConnection conn){

		//ClientScene.Ready (conn);
		ClientScene.AddPlayer(client.connection, 0);
		GameSceneManager.JoinSuccess = true;
		waiting = false;
	}
//
//	public override void OnServerDisconnect(NetworkConnection conn){
//		NetworkServer.DestroyPlayersForConnection(conn);
//		StopHost ();
//	}

//	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
//	{
//		var player = (GameObject)GameObject.Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
//		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
//	}
//
//	public override void OnClientDisconnect(NetworkConnection conn)
//	{
//		if (GameSceneManager.choice == 0) {
//			matchMaker.DestroyMatch (netid, NetworkMatch.DestroyMatch);
//		} else{
//			DropConnectionRequest dropReq = new DropConnectionRequest ();
//			dropReq.networkId = netid;
//			dropReq.nodeId = nodeid;
//			matchMaker.DropConnection (dropReq, NetworkMatch.OnConnectionDrop);
//
//		}
//	}
//
//	void OnClientError(NetworkConnection conn, int errorCode){
//
//	}
//
//	void OnServerError(NetworkConnection conn, int errorCode){
//
//	}
//
//	void OnFailedToConnect(){
//		
//	}
//
//	void OnServerInitialized(){
//
//	}
//
//	void OnPlayerConnected(NetworkPlayer player){
//
//	}
//
//	void OnPlayerDisconnected(NetworkPlayer player){
//		
//	}
//
//	void OnFailedToConnectToMasterServer(){
//
//	}



	// Update is called once per frame
	void Update ()
	{
	
	}
}

