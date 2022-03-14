using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Texture2D img;//背景图片

    private void OnGUI() {
        
        /*定义数值参数*/
        float height = Screen.height * 0.5f;
        float width = Screen.width * 0.5f;
        int ButtonHeight = 50;
        int ButtonWidth = 150;
        int TitleHeight = 80;
        int TitleWidth = 200;

        /*定义字体风格*/
        GUIStyle tStyle1 = new GUIStyle {
            fontSize = 40,
            fontStyle = FontStyle.Bold,
        };
        GUIStyle tStyle2 = new GUIStyle {
            fontSize = 30,
            fontStyle = FontStyle.Bold,
        };

        /*设置背景图片*/
        GUIStyle BackgroundStyle = new GUIStyle();
        BackgroundStyle.normal.background = img;

        /*设置按钮字体风格*/
        GUIStyle ButtonStyle = new GUIStyle("button");
        ButtonStyle.fontSize = 20;

        /*放置标题*/
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", BackgroundStyle);
        GUI.Label(new Rect(width - TitleWidth / 2 - TitleWidth / 4, height - TitleHeight * 2, TitleWidth, TitleHeight), "Priests And Devils", tStyle1);
        GUI.Label(new Rect(width - ButtonWidth / 2, height - TitleHeight , TitleWidth, TitleHeight), "Start Menu", tStyle2);

        /*开始游戏按钮*/
        if (GUI.Button(new Rect(width - ButtonWidth / 2 , height + ButtonHeight / 4, ButtonWidth, ButtonHeight), "Start Game", ButtonStyle)) {
            SceneManager.LoadScene("game", LoadSceneMode.Single);//切换到"game"界面并销毁开始菜单界面
        }
    }
}
