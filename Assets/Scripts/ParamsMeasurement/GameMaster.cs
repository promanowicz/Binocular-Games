using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum GameType { TresholdMeasurement,ContrastMeasurement}

public class GameMaster : MonoBehaviour {
    public GameType gameType;
    public GameObject dotPrefab;
    public List<GameObject> redDots = new List<GameObject>();
    public List<GameObject> blueDots = new List<GameObject>();
    public GameObject[] BackGround;
    public int redDotsNubmer;
    public int blueDotsNumber;
    private int positiveAnswers = 0;
	// Use this for initialization
	void Start () {
        InstantiateRedDots(redDotsNubmer);
        InstantiateBlueDots(blueDotsNumber);
        SetBackgroundColor(Colors.BACKGROUDND);
        if (gameType == GameType.ContrastMeasurement)
        {
            SetRedsColor(Colors.RED);
            SetBluesColor(Colors.BLUE);
            
        }
        if (gameType == GameType.TresholdMeasurement)
        {
            SetRedsColor(Colors.GREY);
            SetBluesColor(Colors.GREY);
        }
        if (Settings.instance.ambylopicEye == Eye.Right)
        {
            setBluesAsSignal();
        }
        if (Settings.instance.ambylopicEye == Eye.Left)
        {
            setBluesAsSignal();
        }
	}

    public void dirButtonClicked(int dir)
    {
        Direction choosenDir = (Direction)dir;
        if (choosenDir == Settings.instance.GetDirection())
        {
            positiveAnswers++;
            if (positiveAnswers % 3 == 0)
            {
                positiveAnswers = 0;
                IncreaseDifficuly();
            }
        }
        else
        {
            positiveAnswers = 0;
            DecreaseDifficuly();
        }
        Settings.instance.RandDir();
        updateNumbers();
    }
    private void IncreaseDifficuly()
    {
        if (Settings.instance.ambylopicEye == Eye.Right)
        {
            blueDotToReds();
        }
        if (Settings.instance.ambylopicEye == Eye.Left)
        {
            redDotToBlues();
        }
    }

    private void DecreaseDifficuly()
    {
        if (Settings.instance.ambylopicEye == Eye.Right)
        {
            redDotToBlues();
        }
        if (Settings.instance.ambylopicEye == Eye.Left)
        {
            blueDotToReds();
        }
    }
    private void redDotToBlues()
    {
        GameObject movingDot = redDots[0];
        redDots.RemoveAt(0);
        blueDots.Add(movingDot);
        movingDot.GetComponent<SpriteRenderer>().color = redDots[0].GetComponent<SpriteRenderer>().color;
    }
    private void blueDotToReds()
    {
        GameObject movingDot = blueDots[0];
        blueDots.RemoveAt(0);
        redDots.Add(movingDot);
        movingDot.GetComponent<SpriteRenderer>().color = blueDots[0].GetComponent<SpriteRenderer>().color;
    }
    private void updateNumbers()
    {
        redDotsNubmer = redDots.Count;
        blueDotsNumber = blueDots.Count;
    }
    void InstantiateRedDots(int no)
    {
        for (int i = 0; i < no; i++)
        {
            GameObject tmp = CreateDot();
            redDots.Add(tmp);
        }
    }

    public void randDirection()
    {
        Settings.instance.RandDir();
    }

    void InstantiateBlueDots(int no)
    {
        for (int i = 0; i < no; i++)
        {
            GameObject tmp = CreateDot();
            blueDots.Add(tmp);
        }
    }

    GameObject CreateDot()
    {
       return (GameObject)Instantiate(dotPrefab, Settings.instance.GetValidPosition(), transform.rotation);
    }

    void setRedsAsSignal()
    {
        foreach (GameObject dot in redDots)
            dot.GetComponent<RandomMovement>().isSignalDot = true;
    }

    void setBluesAsSignal()
    {
        foreach (GameObject dot in blueDots)
            dot.GetComponent<RandomMovement>().isSignalDot = true;
    }

    void SetRedsColor(Color col)
    {
        foreach (GameObject x in redDots)
        {
            x.GetComponent<SpriteRenderer>().color = col;
        }
    }

    void SetBluesColor(Color col)
    {
        foreach (GameObject x in blueDots)
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

