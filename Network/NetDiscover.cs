using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetDiscover : NetworkDiscovery
{

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		GameNetworkManager.singleton.networkAddress = fromAddress;
		GameNetworkManager.singleton.StartClient();
		StopBroadcast ();
	}

}