using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Master : MonoBehaviour {


   // public int signalNoiseRatio;
    public GameObject dotPrefab;
    public List<GameObject> Reds = new List<GameObject>();
    public List<GameObject> Blues = new List<GameObject>();
    public GameObject[] BackGround;
	// Use this for initialization
	void Start () {
        InstantiateDots();
        SetColors();
	}

    //red is left, blue is right
    void InstantiateDots()
    {
        for (int i = 0; i < Settings.instance.redDots + Settings.instance.blueDots; i++)
        {
            GameObject tmp = (GameObject)Instantiate(dotPrefab, Settings.instance.GetValidPosition(), transform.rotation);
            if (i < Settings.instance.redDots)
            {
                Reds.Add(tmp);
                if (Settings.instance.GetEye() == Eye.Right)
                {
                    setUpDotParams(tmp, true);
                }
            }
            else
            {
                Blues.Add(tmp);
                if (Settings.instance.GetEye() == Eye.Left)
                {
                    setUpDotParams(tmp, true);
                }
            }
            
        }
    }

    void setUpDotParams(GameObject dot, bool sigDot)
    {
        dot.transform.position = new Vector3(dot.transform.position.x, dot.transform.position.y, 3);
        dot.GetComponent<RandomMovement>().isSignalDot = sigDot;
    }

    void SetColors()
    {
        foreach (GameObject x in Reds)
        {
            x.GetComponent<SpriteRenderer>().color = Colors.RED;
        }
        foreach (GameObject x in Blues)
        {
            x.GetComponent<SpriteRenderer>().color = Colors.BLUE;
        }
        foreach (GameObject x in BackGround)
        {
            x.GetComponent<SpriteRenderer>().color = Colors.BACKGROUDND;
        }
    }
}

