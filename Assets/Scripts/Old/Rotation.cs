using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
    Vector3 currV, orthoV, orthoA, initV;
    public float speed = 10f;
    public float radius = 5f;
	// Use this for initialization
	void Start () {
        initV = new Vector3(speed, 0, 0);
        GetComponent<Rigidbody>().velocity = initV;
        
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Cos(Time.time), .1f*radius, Mathf.Sin(Time.time)) * radius;
        EdTrip();
	}

    void EdTrip()
    {
        currV = GetComponent<Rigidbody>().velocity;
        if (Random.Range(0, 2) == 0)
        {
            orthoV = Vector3.Cross(currV, Vector3.up);
        }
        else
            orthoV = Vector3.Cross(currV, Vector3.left);
        orthoA = Vector3.Scale(orthoV, orthoV);
        orthoA = orthoA / radius;
        GetComponent<Rigidbody>().AddForce(orthoV, ForceMode.Acceleration);

    }
}
