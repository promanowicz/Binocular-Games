using UnityEngine;
using System.Collections;
public enum Eye { Left, Right };
public enum Direction { Left, Right, Up, Down, };

public class Settings : MonoBehaviour {
    public static string WHICH_EYE = "WHICH_EYE";
    public static string CONTRAST = "CONTRAST";
    public Eye ambylopicEye = Eye.Left;
    private float contrast = 0;
    public Camera camera;
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
        Vector3 vec2 = camera.ViewportToWorldPoint(new Vector3(Random.value,Random.value,0));
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

