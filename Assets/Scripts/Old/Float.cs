using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {
    public float speed = 10f;
    Vector3 currV;
    bool isFloat = true;
	// Use this for initialization
	void Start () {
        currV = new Vector3(0, speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e")  )
            rigidbody.AddForce(currV, ForceMode.Acceleration );
        /*else
            rigidbody.velocity = Vector3.zero;
        */
	}
}
