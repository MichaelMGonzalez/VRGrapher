using UnityEngine;
using System.Collections;

public class HostController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        bool useNat = !Network.HavePublicAddress();
        Network.InitializeServer(2, 8002, useNat);
        MasterServer.RegisterHost("MathVRSimulator", "Test Zone");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
