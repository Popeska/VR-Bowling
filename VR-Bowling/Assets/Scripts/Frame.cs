using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    private int firstRoll = -1; // -1 indicates the roll hasn't been set yet
    private int secondRoll = -1;
    private int total = 0;

    public int FirstRoll
    {
        get { return firstRoll; }
        set { firstRoll = value; }
    }

    public int SecondRoll
    {
        get { return secondRoll; }
        set { secondRoll = value; }
    }

    public int Total
    {
        get { return total; }
        set { total = value; }
    }

    public bool IsStrike()
    {
        return firstRoll == 10;
    }

    public bool IsSpare()
    {
        return firstRoll + secondRoll == 10 && firstRoll != 10;
    }
}
