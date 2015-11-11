using UnityEngine;
using System.Collections;
public enum Eye { Left, Right};
public enum Direction { Left, Right, Up, Down, };

public class Settings : MonoBehaviour {
    public static readonly string WHICH_EYE = "WHICH_EYE";
    public static readonly string RED_DOT_COLOR = "RED_DOT_COLOR";
    public static readonly string BLUE_DOT_COLOR = "BLUE_DOT_COLOR";
    public static readonly string BLUE_DOTS_NUMBER = "BLUE_DOTS_NUMBER";
    public static readonly string RED_DOTS_NUMBER = "RED_DOTS_NUMBER";
    public Eye ambylopicEye = Eye.Left;
    private float contrast = 0;
    public float signalMoveDistance = 2;

    private int redDotsNumber = 50;
    public int RedDotsNumber
    {
        get { return redDotsNumber; }
        set { redDotsNumber = value; }
    }
    private int blueDotsNumber = 50;
    public int BlueDotsNumber
    {
        get { return blueDotsNumber; }
        set { blueDotsNumber = value; }
    }
    private float redDotColor;
    public float RedDotColor
    {
        get { return redDotColor; }
        set { redDotColor = value; }
    }
    private float blueDotColor;
    public float BlueDotColor
    {
        get { return blueDotColor; }
        set { blueDotColor = value; }
    }

    public Camera camera;
    public static Settings instance = null;


    private Direction direction;
    public bool tmp;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.Log("Settings object already exist");
        Random.seed = (int)System.DateTime.Now.Ticks;
        RandDir();
    }
    public Direction RandDir()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        direction = (Direction)Random.Range(0, 2);
        return direction;
    }
    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt(WHICH_EYE, (int)ambylopicEye);
        PlayerPrefs.SetFloat(BLUE_DOT_COLOR, blueDotColor);
        PlayerPrefs.SetFloat(RED_DOT_COLOR, redDotColor);
        PlayerPrefs.SetInt(BLUE_DOTS_NUMBER, blueDotsNumber);
        PlayerPrefs.SetInt(RED_DOTS_NUMBER, redDotsNumber);
        PlayerPrefs.Save();
    }
    public void RestorePrefs()
    {
        if (PlayerPrefs.HasKey(WHICH_EYE))
        {
            ambylopicEye = (Eye)PlayerPrefs.GetInt(WHICH_EYE);
        }
        if (PlayerPrefs.HasKey(RED_DOT_COLOR))
        {
            redDotColor = PlayerPrefs.GetFloat(RED_DOT_COLOR);
        }
        if (PlayerPrefs.HasKey(BLUE_DOT_COLOR))
        {
            blueDotColor = PlayerPrefs.GetFloat(BLUE_DOT_COLOR);
        }
        if (PlayerPrefs.HasKey(RED_DOTS_NUMBER))
        {
           RedDotsNumber = PlayerPrefs.GetInt(RED_DOTS_NUMBER);
        }
        if (PlayerPrefs.HasKey(BLUE_DOT_COLOR))
        {
            BlueDotsNumber = PlayerPrefs.GetInt(BLUE_DOT_COLOR);
        }
    }
    public Vector3 GetValidPosition()
    {
        float x = Random.Range(0.05f, 0.95f);
        float y = Random.Range(0.05f, 0.95f);
        Vector3 vec2 = camera.ViewportToWorldPoint(new Vector3(x,y));
        vec2 = new Vector3(vec2.x, vec2.y, 0);
        return vec2;
    }

    public Vector3 GetShorterValidPosition()
    {
        float x = Random.Range(0.15f, 0.85f);
        float y = Random.Range(0.15f, 0.85f);
        Vector3 vec2 = camera.ViewportToWorldPoint(new Vector3(x, y));
        vec2 = new Vector3(vec2.x, vec2.y, 0);
        return vec2;
    }

    public Eye GetEye()
    {
        return ambylopicEye;
    }
    public Direction GetDirection()
    {
        return direction;
    }
    public float GetContrast()
    {
        return contrast;
    }
}

