using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Texture2D img;//背景图片
    public GUISkin skin;//界面皮肤

    private void OnGUI() {
        GUI.skin=skin;//设置皮肤
        
        //定义数值参数
        float height = Screen.height * 0.5f;
        float width = Screen.width * 0.5f;
        int ButtonHeight = 50;
        int ButtonWidth = 150;
        int TitleHeight = 80;
        int TitleWidth = 200;

        //定义字体风格
        GUIStyle tStyle1 = new GUIStyle {
            fontSize = 40,
            fontStyle = FontStyle.Bold,
        };
        GUIStyle tStyle2 = new GUIStyle {
            fontSize = 30,
            fontStyle = FontStyle.Bold,
        };

        //设置背景图片
        GUIStyle BackgroundStyle = new GUIStyle();
        BackgroundStyle.normal.background = img;

        //设置按钮字体风格
        GUIStyle ButtonStyle = new GUIStyle("button");
        ButtonStyle.fontSize = 20;

        //放置标题
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", BackgroundStyle);
        GUI.Label(new Rect(width - TitleWidth / 3, height - TitleHeight * 2, TitleWidth, TitleHeight), "井字棋", tStyle1);
        GUI.Label(new Rect(width - TitleWidth / 3, height - TitleHeight , TitleWidth, TitleHeight), "开始菜单", tStyle2);

        //放置模式选择按钮
        if (GUI.Button(new Rect(width - ButtonWidth / 2 - 100, height + ButtonHeight / 4, ButtonWidth, ButtonHeight), "单人模式", ButtonStyle)) {
            SceneManager.LoadScene("singlePlayerGame", LoadSceneMode.Single);//切换到"OnePlayerMode"界面并销毁开始菜单界面
        }

        if (GUI.Button(new Rect(width - ButtonWidth / 2 + 100, height + ButtonHeight / 4, ButtonWidth, ButtonHeight), "双人模式", ButtonStyle)) {
            SceneManager.LoadScene("doublePlayerGame", LoadSceneMode.Single);;//切换到"TwoPlayersMode"界面并销毁开始菜单界面
        }
    }
}
