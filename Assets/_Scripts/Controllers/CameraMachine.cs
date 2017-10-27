using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMachine : MonoBehaviour {
#region Singleton
    public static CameraMachine instance;

    private void Awake()
    {
        instance = this;
    }
#endregion
    public Transform target;
    public Transform defaultTarget;
    float distance;
    float startTime;
    float speed = 20f;

    
    public float defaultCameraAngle = 61.187f;
    public float adjustDistance = 1f;
    public float cameraAngle;
    public bool isPlayerMovement = true;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        target = defaultTarget;
        defaultCameraAngle = 61.187f;
        adjustDistance = 1f;
        isPlayerMovement = true;
}
	
	// Update is called once per frame
	void Update () {
        CameraDontRotate();
        
    }

    public void CameraDontRotate()
    {
        distance = target.position.y * .49f * adjustDistance ;
        Vector3 movePosition = new Vector3(target.position.x, target.position.y, target.position.z - distance);
        float journeyLength = Vector3.Distance(transform.position, movePosition);
        float distCovered = (Time.time - startTime) * 1f;
        float fracJourney = distCovered / journeyLength;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraAngle, 0, 0),  .85f);
        if (isPlayerMovement)
        {
            transform.position = Vector3.Lerp(transform.position, movePosition, fracJourney);
        }
        else if (!isPlayerMovement)
        {
            transform.position = Vector3.Lerp(transform.position, movePosition, .85f);
        }

    }

    


}
