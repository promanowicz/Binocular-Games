using UnityEngine;
using System.Collections;
public enum Eye { Left, Right};
public enum Direction { Left, Right, Up, Down, };

public class Settings : MonoBehaviour {
    public static string WHICH_EYE = "WHICH_EYE";
    public static string CONTRAST = "CONTRAST";
    public static string BLUE_DOTS_NUMBER = "BLUE_DOTS_NUMBER";
    public static string RED_DOTS_NUMBER = "RED_DOTS_NUMBER";
    public Eye ambylopicEye = Eye.Left;
    private float contrast = 0;
    private int redDotsNumber = 50;
    public float signalMoveDistance = 2;
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
    void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt(WHICH_EYE, (int)ambylopicEye);
        PlayerPrefs.SetFloat(CONTRAST, contrast);
        PlayerPrefs.SetInt(BLUE_DOTS_NUMBER, blueDotsNumber);
        PlayerPrefs.SetInt(RED_DOTS_NUMBER, redDotsNumber);
    }
    void RestorePrefs()
    {
        if (PlayerPrefs.HasKey(WHICH_EYE))
        {
            ambylopicEye = (Eye)PlayerPrefs.GetInt(WHICH_EYE);
        }
        if (PlayerPrefs.HasKey(CONTRAST))
        {
            contrast = PlayerPrefs.GetFloat(CONTRAST);
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

