using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class doublePlayerGame : MonoBehaviour {
    //定义棋盘变量empty表示空的，you表示你下的，pc表示电脑下的，用于填在checkerboard中
    private const int empty=0;
    private const int player1=1;
    private const int player2=2;
    //游戏正在进行
    private bool playing = true; 
    //当前轮到player1或者player2
    private int turn = player1;
    private int [,] checkerboard=new int[3,3];//empty表示空的，player1表示玩家1下的，play2表示玩家2下的；
    //背景图片
    public Texture2D img;
    //按钮大小
    private const int buttonHeight = 80;
    private const int buttonWidth = 80;
    //文本风格
    private GUIStyle resultMsgStyle; 
    private GUIStyle ButtonStyle;
    //一些坐标位置
    int backButtonX;
    int backButtonY;
    int firstGridX;
    int firstGridY;
    int resetButtonX;
    int resetButtonY;
    int resultMsgX;
    int resultMsgY;
    //定义皮肤
    public GUISkin skin;
    /*放置回退按钮*/
    private void displayBackButton(){
        if (GUI.Button(new Rect(backButtonX, backButtonY,buttonWidth/2+buttonWidth, buttonHeight / 2), "返回菜单",ButtonStyle)) {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);//切换到"StartMenu"界面并销毁开始菜单界面
        }
    }
    /*放置清空按钮*/
    private void displayResetButton(){
        if (GUI.Button(new Rect(resetButtonX, resetButtonY,buttonWidth/2+buttonWidth, buttonHeight / 2), "重新开始",ButtonStyle)) {
            Reset();
            return;
        }
    }
    /*检查是否有赢家出现*/
    private int Check() {
        for(int i=0;i<3;i++){
            //占有斜行
            if(checkerboard[i,i]!=empty){
                //如果斜右下方向被同一角色占有
                if(checkerboard[(i+1)%3,(i+1)%3]==checkerboard[i,i]&&checkerboard[(i+2)%3,(i+2)%3]==checkerboard[i,i]) return checkerboard[i,i];
            }
            if(checkerboard[i,2-i]!=empty){
                //如果斜右上方向被同一角色占有
                if(checkerboard[(i+1)%3,2-(i+1)%3]==checkerboard[i,2-i]&&checkerboard[(i+2)%3,2-(i+2)%3]==checkerboard[i,2-i]) return checkerboard[i,2-i];
            }
            //占有横纵行
            for(int j=0;j<3;j++){
                if(checkerboard[i,j]!=empty){
                    //占有横行
                    if(checkerboard[i,(j+1)%3]==checkerboard[i,j]&&checkerboard[i,(j+2)%3]==checkerboard[i,j]) return checkerboard[i,j];
                    //占有纵行
                    if(checkerboard[(i+1)%3,j]==checkerboard[i,j]&&checkerboard[(i+2)%3,j]==checkerboard[i,j]) return checkerboard[i,j];
                }
            }
        }
        return empty;
    }
    /*展现当前结果（输赢）*/
    private void ShowResult(int winner){
        //Check if someone wins
        if (winner != empty) {
            if(winner == player1)
                GUI.Label(new Rect(resultMsgX, resultMsgY, 100, 100), "玩家1胜利" , resultMsgStyle);
            else
                GUI.Label(new Rect(resultMsgX, resultMsgY, 100, 100), "玩家2胜利" , resultMsgStyle);
            playing = !playing;
            GUI.enabled = false;
        }
        else{
            GUI.Label(new Rect(resultMsgX, resultMsgY, 100, 100), "目前没有赢家" , resultMsgStyle);
        }
    }
    //展示棋盘按钮并实现下棋的功能
    private void showCheckerboard(){
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                if (checkerboard[i, j] == player1) {//player1下的棋用“X”表示，player2下的棋用“O"表示
                    GUI.Button(new Rect(firstGridX + i * buttonWidth, firstGridY + j * buttonHeight, buttonWidth, buttonHeight), "X");
                } else if (checkerboard[i, j] == player2) {
                    GUI.Button(new Rect(firstGridX + i * buttonWidth, firstGridY + j * buttonHeight, buttonWidth, buttonHeight), "O");
                } else {
                    if (playing&&GUI.Button(new Rect(firstGridX + i * buttonWidth, firstGridY + j * buttonHeight, buttonWidth, buttonHeight), "")) {
                            if (turn==player1) {//当轮到玩家1下棋的时候，玩家1点击空的棋格，该棋格被玩家1占有并显示"X"
                                checkerboard[i, j]=player1;
                                turn = player2;
                            } else {//当轮到玩家2下棋的时候，玩家2点击空的棋格，该棋格被玩家2占有并显示"O"
                                checkerboard[i, j]=player2;
                                turn = player1;
                            }
                    }
                }
            }
        }
    }
    void Start () {
        Reset();
    }
    
    private void OnGUI() {
        GUI.skin=skin;//设置皮肤
        //界面参数设置
        backButtonX=Screen.width / 2 -buttonWidth/2-buttonWidth;
        backButtonY=Screen.height / 2 +buttonWidth+buttonWidth/2;
        firstGridX=Screen.width / 2 -buttonWidth/2-buttonWidth;
        firstGridY=Screen.height / 2 -buttonHeight/2-buttonHeight-buttonHeight/4;
        resetButtonX=Screen.width / 2 ;
        resetButtonY=Screen.height / 2 +buttonWidth+buttonWidth/2;
        resultMsgX=Screen.width / 2 -buttonWidth/2-buttonWidth;
        resultMsgY=Screen.height / 2 -2*buttonHeight-buttonHeight/4;
        //按键字体风格
        ButtonStyle = new GUIStyle("button");
        ButtonStyle.fontSize = 20;
        //结果提示字体风格
        resultMsgStyle = new GUIStyle {
            fontSize = 20,
            fontStyle = FontStyle.Bold
        };
        resultMsgStyle.normal.textColor = Color.black;
        //背景设置
        GUIStyle backgroundStyle = new GUIStyle();
        backgroundStyle.normal.background = img;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", backgroundStyle);

        //放置按钮
        //回退按钮 
        displayBackButton();
        //重新开始按钮
        displayResetButton();
        //展示结果提示（展示检查是否有胜利者的结果)
        ShowResult(Check());
        //展示棋盘实现下棋逻辑
        showCheckerboard();
        GUI.enabled = true;
    }

    //重置
    private void Reset() {
        playing = true;
        turn = player1;
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                checkerboard[i,j] =empty;
            }
        }
        GUI.enabled = true;
    }
}
