using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetDiscover : NetworkDiscovery
{

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		GameSceneManager.Numconnection = 3;
		GameNetworkManager.singleton.networkAddress = fromAddress;
		GameNetworkManager.singleton.StartClient();
		GameSceneManager.whichplayer = "Player2";
		StopBroadcast ();
	}

}