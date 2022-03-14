using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    //加载场景资源
    void LoadResources();
}

public interface IUserAction                          
{
    //移动玩家
    void MovePlayer(float translationX, float translationZ);
    //得到分数
    int GetScore();
    //得到金矿数量
    int GetGoldmineNumber();
    //得到游戏结束标志
    bool GetGameover();
    //重新开始
    void Restart();
}

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source,int intParam = 0,GameObject objectParam = null);
}
