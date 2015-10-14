using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public bool change = false;
    public float a = 0.5f;
	// Use this for initiasdssation
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (change)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
            Debug.Log("a:" + a);
        }
	}
}
