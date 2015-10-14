using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour {

    private Vector3 lastPosition;
    private Vector3 nextPosition;
    public float speed = 1.0F;
    public bool isSignalDot;
    private float startTime;
    private float distance;
	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
        SetUpJourney(transform.position);
	}	
	// Update is called once per frame
	void Update () {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / distance;
        if (!transform.position.Equals(nextPosition))
        {
            transform.position = Vector3.Lerp(lastPosition, nextPosition, fracJourney);
        }
        else
        {
            SetUpJourney(nextPosition);
        };
	}

    void SetUpJourney(Vector3 lastpos)
    {
        if (!isSignalDot) {
        lastPosition = Settings.instance.GetValidPosition();
        nextPosition = Settings.instance.GetValidPosition();
        }
        else
        {
            transform.position = Settings.instance.GetValidPosition();
            lastPosition = transform.position;
            nextPosition = transform.position;
            switch (Settings.instance.GetDirection())
            {
                case Direction.Left:
                    nextPosition.x = nextPosition.x - Settings.instance.signalMoveDistance;
                    break;
                case Direction.Right:
                    nextPosition.x = nextPosition.x + Settings.instance.signalMoveDistance;
                    break;
            }
        }
        startTime = Time.time;
        distance = Vector3.Distance(lastPosition, nextPosition);
      //  Debug.Log("Nextpos:" + nextPosition.ToString());
    }
}

