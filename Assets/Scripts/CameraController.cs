using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float radius, speed;
    private float horizontal, vertical;
    public Transform target;
    public Vector3 offset;
    public bool isDebugging;
    private Vector3 newPosition;
    // Use this for initialization
    void Start()
    {
        horizontal = 0;
        vertical = 0;
    }

    // Update is called once per frame
    void Update()
    {
        detectKeyStrokes();
        detectMouseWheel();
        checkBounds();
        moveAlongAxis();
        rotate();
        if (isDebugging)
        {
            Debug.Log("Horizontal: " + horizontal);
            Debug.Log("Vertical: " + vertical);
        }
    }
    void moveAlongAxis()
    {
        float theta = horizontal;
        float phi = vertical;
        float r = radius;
        newPosition = new Vector3(r * Mathf.Cos(theta) * Mathf.Sin(phi) , r * Mathf.Cos(phi) , r * Mathf.Sin(theta) * Mathf.Sin(phi));
        transform.position = Vector3.Lerp(transform.position, newPosition - offset, speed);
    }
    void rotate()
    {
        float step = (1/speed) * Time.deltaTime;
        Vector3 targetDir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    void detectKeyStrokes()
    {
        if (Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
            horizontal += speed;
        if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
            horizontal -= speed;
        if (Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S))
            vertical -= speed;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            vertical += speed;
    }
    void checkBounds()
    {
        if (horizontal < 0)
            horizontal += 2*Mathf.PI;
        if (horizontal > Mathf.PI * 2)
            horizontal -= 2*Mathf.PI;
        if (vertical < 0)
            vertical += 2*Mathf.PI;
        if (vertical > 2*Mathf.PI)
            vertical -= 2*Mathf.PI;
        if( radius < 4 )
            radius = 4;
        if (Network.isServer)
            GetComponent<NetworkView>().RPC("updateRTP", RPCMode.OthersBuffered, radius, horizontal, vertical);
    }
    void detectMouseWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            radius--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            radius++;
        }
        if (Network.isServer)
            GetComponent<NetworkView>().RPC("updateRTP", RPCMode.OthersBuffered, radius, horizontal, vertical);
    }
    [RPC]
    private void updateRTP(float rad, float theta, float phi)
    {
        radius = rad;
        horizontal = theta;
        vertical = phi;
    }
}
