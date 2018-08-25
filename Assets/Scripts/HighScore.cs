using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : IComparable<HighScore> {

    public string playerName;
    public float score;
    public System.DateTime time;

    public int CompareTo(HighScore other)
    {
        return this.score.CompareTo(other.score);
    }
}
