using UnityEngine;
using System.Collections;
public enum Eye { Left, Right };
public enum Direction { Left, Right, Up, Down, };

public class Settings : MonoBehaviour {
    public static string WHICH_EYE = "WHICH_EYE";
    public static string CONTRAST = "CONTRAST";
    public Eye ambylopicEye = Eye.Left;
    private float contrast = 0;
    public static Settings instance = null;
    public  Transform leftGuard;
    public  Transform rightGuard;
    public  Transform topGuard;
    public  Transform bottomGuard;
 //   public int movementDistanceFactor;
    public float signalMoveDistance=2;
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
    public float GetXlen()
    {
        return Mathf.Abs(rightGuard.transform.position.x - leftGuard.transform.position.x);
    }
    public float GetYlen()
    {
        return Mathf.Abs(topGuard.transform.position.y - bottomGuard.transform.position.y);
    }
    public Vector3 GetValidPosition()
    {
        float xlen = GetXlen();
        float ylen = GetYlen();
        float x = Random.value * xlen - xlen / 2;
        float y = Random.value*ylen-ylen/2;
        return new Vector3(x+transform.position.x, y+transform.position.y, 0);
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
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
        if (tmp)
        {
            Debug.Log("Kierunek: " + RandDir().ToString());
        }
	}
}

