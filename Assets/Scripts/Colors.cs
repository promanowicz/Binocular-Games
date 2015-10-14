using UnityEngine;
using System.Collections;

public static class Colors : MonoBehaviour {
    private static float mRed = 0.5f;
    private static float mGreen = 0.5f;
    private static float mBlue = 0.5f;
    public static Color BACKGROUDND = new Color(mRed-0.03f, mGreen, mBlue);
    public static Color RED = new Color(mRed, 0, 0,mRed);
    public static Color BLUE = new Color(0, mGreen, mBlue);
}
