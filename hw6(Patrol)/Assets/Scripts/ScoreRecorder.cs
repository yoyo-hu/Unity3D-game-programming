using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public FirstSceneController sceneController;
    public int score = 0;                            //分数
    public int goldmine_number = 12;                  //金矿数量

    // Use this for initialization
    void Start()
    {
        sceneController = (FirstSceneController)SSDirector.GetInstance().CurrentScenceController;
        sceneController.recorder = this;
    }
    public int GetScore()
    {
        return score;
    }
    public void AddScore()
    {
        score++;
    }
    public int GetGoldmineNumber()
    {
        return goldmine_number;
    }
    public void ReduceGoldmine()
    {
        goldmine_number--;
    }
}

