using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //分数变化
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;
    //游戏结束
    public delegate void GameoverEvent();
    public static event GameoverEvent GameoverChange;
    //金矿数量
    public delegate void GoldmineEvent();
    public static event GoldmineEvent GoldmineChange;

    //分数变化
    public void PlayerEscape()
    {
        if (ScoreChange != null)
        {
            ScoreChange();
        }
    }
    //游戏结束
    public void PlayerGameover()
    {
        if (GameoverChange != null)
        {
            GameoverChange();
        }
    }
    //金矿数量
    public void ReduceGoldmineNum()
    {
        if (GoldmineChange != null)
        {
            GoldmineChange();
        }
    }
}
