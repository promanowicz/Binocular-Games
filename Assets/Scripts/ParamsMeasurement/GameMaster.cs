using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum GameType { TresholdMeasurement,ContrastMeasurement}

public class GameMaster : MonoBehaviour {
    public GameType gameType;
    public GameObject dotPrefab;
    public List<GameObject> redDots = new List<GameObject>();
    public List<GameObject> blueDots = new List<GameObject>();
    private List<int> measurementsRed = new List<int>();
    public GameObject[] BackGround;
    public int redDotsNubmer =50;
    public int blueDotsNumber=50;
    private int positiveAnswers = 0;
    public float contrastIncFactor;
    public int tresholdIncFactor;
    public float timer = 300;
    public float timerResetValue = 180;
    public float resetTime = 10;
	// Use this for initialization
	void Start () {
        if (gameType == GameType.ContrastMeasurement)
        {
            RestoreValues();
        }
        setUpGame();
	}

    bool isAdded = false;
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!isAdded) {
                isAdded = true;
                 measurementsRed.Add(redDots.Count);
            }
            if (measurementsRed.Count >= 5 )
            {
                Settings.instance.RedDotsNumber = average(measurementsRed);
                Settings.instance.SavePlayerPrefs();
                Debug.Log("NEW SCENE");
                //TODO: load new scene
            }
            DeleteAllDots();
            redDotsNubmer = 50;
            blueDotsNumber = 50;
            //monit o resetowaniu gry
            if (timer < -resetTime) {
                isAdded = false;
               setUpGame();
               timer = timerResetValue;
            }
        }
    }

    int average(List<int> list)
    {
        int tmp = 0;
        foreach (int x in list) tmp += x;
        tmp /= list.Count;
        return tmp;
    }

    void RestoreValues()
    {

    }

    void setUpGame()
    {
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
            setRedsAsSignal();
            if (gameType == GameType.ContrastMeasurement)
            {
                SetBluesColor(Colors.BACKGROUDND);
            }
        }
        if (Settings.instance.ambylopicEye == Eye.Left)
        {
            setBluesAsSignal();
            if (gameType == GameType.ContrastMeasurement)
            {
                SetRedsColor(Colors.BACKGROUDND);
            }
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
                for (int i = 0; i < tresholdIncFactor;i++)
                    IncreaseDifficuly();
            }
        }
        else
        {
            positiveAnswers = 0;
            for (int i = 0; i < tresholdIncFactor; i++)
            DecreaseDifficuly();
        }
        Settings.instance.RandDir();
        updateNumbers();
    }

    private void IncreaseDifficuly()
    {
        if (Settings.instance.ambylopicEye == Eye.Right)
        {
            if (gameType == GameType.ContrastMeasurement)
            {
                SetBluesColor(Colors.IncreaseDotLuminance(blueDots[0].GetComponent<SpriteRenderer>().color, contrastIncFactor, false));
            }
            else
            {
                redDotToBlues(false); 
            }
        }
        if (Settings.instance.ambylopicEye == Eye.Left)
        {
            if (gameType == GameType.ContrastMeasurement)
            {
                SetRedsColor(Colors.IncreaseDotLuminance(redDots[0].GetComponent<SpriteRenderer>().color, contrastIncFactor, true));
            }
            else
            {
                blueDotToReds(false);
            }
        }
    }

    private void DecreaseDifficuly()
    {
        if (Settings.instance.ambylopicEye == Eye.Right)
        {
            if (gameType == GameType.ContrastMeasurement)
            {
                SetBluesColor(Colors.DecreaseDotLuminance(blueDots[0].GetComponent<SpriteRenderer>().color, contrastIncFactor, false));
            }
            else
            {
                redDotToBlues(true);
            }     
        }
        if (Settings.instance.ambylopicEye == Eye.Left)
        {
            if (gameType == GameType.ContrastMeasurement)
            {
                SetRedsColor(Colors.DecreaseDotLuminance(redDots[0].GetComponent<SpriteRenderer>().color, contrastIncFactor, true));
            }
            else
            {
                blueDotToReds(true);
            }
        }
    }

    private void redDotToBlues(bool willBeSignalDot)
    {
        switchDot(redDots, blueDots, willBeSignalDot);
    }
    private void blueDotToReds(bool willBeSignalDot)
    {
        switchDot(blueDots, redDots, willBeSignalDot);
    }

    private void switchDot(List<GameObject> from, List<GameObject> to, bool willBeSignalDot)
    {
        GameObject movingDot = from[0];
        from.RemoveAt(0);
        to.Add(movingDot);
       // movingDot.GetComponent<SpriteRenderer>().color = to[0].GetComponent<SpriteRenderer>().color;
        movingDot.GetComponent<RandomMovement>().isSignalDot = willBeSignalDot;
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

    void DeleteAllDots()
    {
            foreach (GameObject tmp in redDots)
            {
                Destroy(tmp);
            }
            foreach (GameObject tmp in blueDots)
            {
                Destroy(tmp);
            }

            redDots.Clear();
            blueDots.Clear();
    }
}

