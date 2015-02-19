using UnityEngine;
using System.Collections;

public class ClientController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MasterServer.RequestHostList("MathVRSimulator");
         

	}
	
	// Update is called once per frame
	void Update () {
        if (MasterServer.PollHostList().Length != 0)
        {
            HostData[] hostData = MasterServer.PollHostList();
            int i = 0;
            while (i < hostData.Length)
            {
                Debug.Log("Game name: " + hostData[i].gameName);
                i++;
            }
            MasterServer.ClearHostList();
            Network.Connect(hostData[0].ip, hostData[0].port);
        }
         
	}
}
