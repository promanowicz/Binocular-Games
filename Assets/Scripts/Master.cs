using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Master : MonoBehaviour {

    //max l. kropek 100;
   // public int signalNoiseRatio;
    public GameObject dotPrefab;
    public List<GameObject> Reds = new List<GameObject>();
    public List<GameObject> Blues = new List<GameObject>();
    public GameObject[] BackGround;
    public int redDots;
    public int blueDots;


	// Use this for initialization
	void Start () {
        InstantiateRedDots(redDots);
        InstantiateBlueDots(blueDots);
       SetRedsColor(Colors.RED);
       SetBluesColor(Colors.BLUE);
       SetBackgroundColor(Colors.BACKGROUDND);
       setBluesAsSignal();
	}
    //red is left, blue is right
    void InstantiateRedDots(int no)
    {
        for (int i = 0; i < no; i++)
        {
            GameObject tmp = CreateDot();
            Reds.Add(tmp);                 
        }
    }

    void InstantiateBlueDots(int no)
    {
        for (int i = 0; i < no; i++)
        {
            GameObject tmp = CreateDot();
            Blues.Add(tmp);
        }
    }

    GameObject CreateDot()
    {
        return (GameObject)Instantiate(dotPrefab, Settings.instance.GetValidPosition(), transform.rotation);
    }

    void setRedsAsSignal()
    {
        foreach(GameObject dot in Reds)
        dot.GetComponent<RandomMovement>().isSignalDot = true;
    }

    void setBluesAsSignal()
    {
        foreach (GameObject dot in Blues)
            dot.GetComponent<RandomMovement>().isSignalDot = true;
    }

    void SetRedsColor(Color col)
    {
        foreach (GameObject x in Reds)
        {
            x.GetComponent<SpriteRenderer>().color = col;
        }
    }

    void SetBluesColor(Color col)
    {
        foreach (GameObject x in Blues)
        {
            x.GetComponent<SpriteRenderer>().color = col;
        }
    }
    void SetBackgroundColor(Color col)
    {
        foreach (GameObject x in BackGround)
        {
            x.GetComponent<SpriteRenderer>().color = col;
        }
    }
    void SetAllDotColor(Color col)
    {
        SetBluesColor(col);
        SetRedsColor(col);
    }
}

