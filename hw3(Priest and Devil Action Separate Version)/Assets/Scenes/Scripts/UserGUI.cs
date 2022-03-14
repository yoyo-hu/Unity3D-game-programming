using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mygame{

    public class UserGUI : MonoBehaviour {

        
        private Controllor action;
        public string timeStr = string.Empty;
        void Start(){
            action = SSDirector.GetInstance().CurrentScenceController as Controllor;
        }
        
        void OnGUI(){

            /*设置计时器*/
            timeStr = string.Format("用时：{0:D2}:{1:D2}:{2:D2}", action.getTimer().hour, action.getTimer().minute, action.getTimer().second);
            GUI.Label(new Rect(500, 10, 100, 200), timeStr);

            /*定义字体风格*/
            GUIStyle text_style;
            GUIStyle button_style;
            
            text_style = new GUIStyle()
            {
                fontSize = 30
            };
            button_style = new GUIStyle("button")
            {
                fontSize = 15
            };
            /*游戏规则按钮*/
            if (GUI.Button(new Rect(10, 10, 100, 30), "game rules", button_style)){
            /*点一下打开游戏规则提示，再点一下关闭游戏规则提示*/
                action.setIsShowRules(!action.getIsShowRules());
            }

            /*展示游戏规则*/
            if(action.getIsShowRules()){
                GUI.Label(new Rect(Screen.width / 2 - 150, 50, 300, 50), "Win: all priests and demons cross the river");
                GUI.Label(new Rect(Screen.width / 2 - 150, 70, 400, 50), "Lose: there are more demons than priests on either side");
                GUI.Label(new Rect(Screen.width / 2 - 150, 90, 300, 50), "Tap priest, demon, ship to move");
            }

            /*返回菜单按钮*/
            if (GUI.Button(new Rect(120, 10, 100, 30), "return menu", button_style)) {
                SceneManager.LoadScene("startMenu", LoadSceneMode.Single);;//切换到"startMenu"界面并销毁本界面
            }

            /*重新开始按钮*/
            if (GUI.Button(new Rect(230, 10, 100, 30), "restart", button_style)) {
                action.setGameState(0);
                action.Restart();
            }

            /*游戏暂停按钮*/
            if(GUI.Button(new Rect(340, 10, 100, 30), "pause", button_style)){
                if (action.isPlaying()) { //游戏正在进行
                    action.setGameState(3); //游戏的状态发生改变
                    action.getTimer().StopTiming(); //事件暂停
                }
            }

            if(action.isPause()){//游戏暂停，打印暂停并显示“Return Game”按钮提示玩家可以点击回到游戏中
                GUI.Label(new Rect(Screen.width / 2-100, Screen.height / 2-120, 100, 50), "Game pause!", text_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 120, 50), "Return Game", button_style)){
                    action.setGameState(0);
                    action.getTimer().beginTiming();
                }
            }

            if (action.isLose()){//游戏失败，打印失败并显示“Try again”按钮提示玩家再进行尝试
                GUI.Label(new Rect(Screen.width / 2-90, Screen.height / 2-120, 100, 50), "Gameover!", text_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 100, 50), "Try again", button_style))
                {
                    action.setGameState(0);
                    action.Restart(); 
                }
            }
            else if (action.isWin()){//游戏成功，打印成功并显示“Play again”按钮提示玩家再玩一局
                GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 120, 100, 50), "You win!", text_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 100, 50), "Play again", button_style)){
                    action.setGameState(0);
                    action.Restart();
                }
            }
        }
    }
}