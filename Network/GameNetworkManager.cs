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
		GameSceneManager.choice = 1;
		StartCoroutine( joinOrCreate ());
	}

	public IEnumerator joinOrCreate() {
		//waits for response from list matches
		yield return new WaitForSeconds (refreshingRequestLength);
		if (GameSceneManager.JoinSuccess == false) {
			//StopClient ();
			discovery.StopBroadcast();
			StartHost ();
			GameSceneManager.whichplayer = "Player1";
		}
	}


	
	public override void OnStartHost()
	{
		Debug.Log ("OnPlayerConnected"+ numPlayers);
		waiting = false;
		GameSceneManager.choice = 0;
		waiting = false;
		discovery.StartAsServer();

	}


	public override void OnServerConnect(NetworkConnection conn){
		Debug.Log ("Client is connected");
		GameSceneManager.Numconnection = GameSceneManager.Numconnection + 1;
		if (GameSceneManager.Numconnection > 2) {
			discovery.StopBroadcast();
		}
	}
		
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject player = (GameObject)Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	public override void OnClientConnect(NetworkConnection conn){

		ClientScene.AddPlayer(client.connection, 0);
		GameSceneManager.JoinSuccess = true;
		waiting = false;
	}

	public override void OnServerDisconnect(NetworkConnection conn){
		NetworkServer.DestroyPlayersForConnection(conn);
		StopHost ();
		SetUp.message = "Disconnected";
		SetUp.disconnected = true;
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		NetworkServer.DestroyPlayersForConnection(conn);

		if (Network.isClient) {
			StopClient ();
			SetUp.message = "Disconnected";
			SetUp.disconnected = true;
		} else {
			StopHost ();
			SetUp.message = "Disconnected";
			SetUp.disconnected = true;
		}
	}


	void OnFailedToConnect(){
		
	}

	void OnPlayerDisconnected(NetworkPlayer player){
		
	}



	// Update is called once per frame
	void Update ()
	{
	
	}
}

