using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private IUserAction action;
    private GUIStyle score_num_style = new GUIStyle();
    private GUIStyle score_text_style = new GUIStyle();
    private GUIStyle role_text_style = new GUIStyle();
    private GUIStyle role_title_style = new GUIStyle();
    private GUIStyle titile_style = new GUIStyle();
    public  int show_time = 10;                         //展示提示的时间长度
    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        role_text_style.normal.textColor = new Color(0, 0, 0, 1);
        role_title_style.fontSize = 20;
        role_title_style.normal.textColor = new Color(1,0.92f,0.016f,1);
        role_text_style.fontSize = 20;
        score_num_style.normal.textColor = new Color(1,0.92f,0.016f,1);
        score_num_style.fontSize = 30;
        score_text_style.normal.textColor = new Color(0, 0, 0, 1);
        score_text_style.fontSize = 30;
        titile_style.normal.textColor = new Color(1,0,0,1);
        titile_style.fontSize = 40;
        //展示提示
        StartCoroutine(ShowTip());
    }

    void Update()
    {
        //获取方向键的偏移量
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        //移动玩家
        action.MovePlayer(translationX, translationZ);
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", score_text_style);
        GUI.Label(new Rect(90, 5, 200, 50), action.GetScore().ToString(), score_num_style);
        GUI.Label(new Rect(Screen.width - 225, 5, 50, 50), "剩余金矿数:", score_text_style);
        GUI.Label(new Rect(Screen.width - 60, 5, 50, 50), action.GetGoldmineNumber().ToString(), score_num_style);
        if(action.GetGameover() && action.GetGoldmineNumber() != 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 100, 100, 100), "游戏结束！", titile_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 15, Screen.height / 2 , 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }
        else if(action.GetGoldmineNumber() == 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 100, 100, 100), "恭喜胜利！", titile_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 15, Screen.height / 2 , 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }
        if(show_time > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 10 ,10, 100, 200), "游戏规则：", role_title_style);
            GUI.Label(new Rect(Screen.width / 2 - 140 ,40, 100, 200), "按WSAD或方向键移动,成功躲避巡逻兵追捕加1分", role_text_style);
            GUI.Label(new Rect(Screen.width / 2 - 60, 70, 100, 200), "采集完所有的金矿即可获胜", role_text_style);
            GUI.Label(new Rect(Screen.width / 2 - 145, 100, 100, 200), "滚轮实现镜头缩进和拉远,按着鼠标右键实现视角转动", role_text_style);
        }
    }

    public IEnumerator ShowTip()
    {
        while (show_time >= 0)
        {
            yield return new WaitForSeconds(1);
            show_time--;
        }
    }
}
