using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    public float maxScore;
    public float ratioScore;

    public int songID;
    public float noteSpeed;

    public bool Start;
    public float StartTime;

    public int combo;
    public int score;

    public int stellar_crystal;
    public int stellar;
    public int bad;
    public int dust;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
