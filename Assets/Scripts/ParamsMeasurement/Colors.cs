using UnityEngine;
using System.Collections;

public class Colors : MonoBehaviour {
    private static float mRed = 0.7f;
    private static float mGreen = 0.7f;
    private static float mBlue = 0.7f;
    public static Color BACKGROUDND = new Color(mRed-0.03f, mGreen, mBlue);
    public static Color RED = new Color(mRed, 0, 0,mRed);
    public static Color BLUE = new Color(0, mGreen, mBlue);
    public static Color GREY = new Color(mRed - 0.2f, mGreen - 0.2f, mBlue - 0.2f);

    //Zwiększenie luminancji kulki odbywa się przez zmniejszanie wartości kolorów składowych 
    //nie będących wartościami koloru właściwego (np GB dla koloru czerwonego)
    //zwiększamy kontrast względem tła
    public static Color DecreaseDotLuminance(Color baseColor, float factor, bool isRedDot)
    {
        //TODO: Jeżeli pierwszy ruch gracza jest błędny, zwiększony zostanie kontrast poprzez przekroczenie wartości RGB tła
        if (baseColor.r <= mRed && baseColor.g <= mGreen  && baseColor.b <= mBlue )
        if (isRedDot)
        {
            baseColor.g += factor;
            baseColor.b += factor;
        }
        else
        {
            baseColor.r += factor;
        }
        
        return baseColor;
    }

    public static Color IncreaseDotLuminance(Color baseColor, float factor, bool isRedDot)
    {
        if (isRedDot)
        {
            baseColor.g -= factor;
            baseColor.b -= factor;
        }
        else
        {
            baseColor.r -= factor;
        }

        return baseColor;
    }
}
