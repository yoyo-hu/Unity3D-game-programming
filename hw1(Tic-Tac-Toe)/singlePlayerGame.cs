using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class singlePlayerGame : MonoBehaviour {
    //定义棋盘变量empty表示空的，you表示你下的，pc表示电脑下的，用于填在checkerboard中
    private const int empty=0;
    private const int you=1;
    private const int pc=2;
    //背景图片
    public Texture2D img;
    //游戏正在进行
    private bool playing = true;
    //当前轮到you或者pc
    private int turn = you;
    private int [,] checkerboard=new int[3,3];
    //pc该下的棋的坐标
    private int PC_x = -1;
    private int PC_y = -1;
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
    /*进入游戏清空棋盘并做一些初始化*/
    void Start() {
        Reset();
    }
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
            if(winner == you)
                GUI.Label(new Rect(resultMsgX, resultMsgY, 100, 100), "你赢了" , resultMsgStyle);
            else
                GUI.Label(new Rect(resultMsgX, resultMsgY, 100, 100), "你输了" , resultMsgStyle);
            playing = !playing;
            GUI.enabled = false;
        }
        else{
            GUI.Label(new Rect(resultMsgX, resultMsgY, 100, 100), "目前没有赢家" , resultMsgStyle);
        }
    }
    /*PC端寻找最优的步骤：优先让自己赢其次不让对手赢，最后是在有空位的情况下随机下棋*/
    private void searchForGoodStep(){
        if(makePCWin()){
        }
        else if (blockYou()) {
        }
        else if(randomStep()){
        }
        else return;
        checkerboard[PC_x, PC_y] = pc;
    }
    /*搜索一个空位使得PC能赢*/
    private bool makePCWin() {
        
        for(int i=0;i<3;i++){
            //占有斜行
            if(checkerboard[i,i]==empty){
                //如果斜右下方向其他空格被pc占有
                if(checkerboard[(i+1)%3,(i+1)%3]==pc&&checkerboard[(i+2)%3,(i+2)%3]==pc){
                    PC_x=i;
                    PC_y=i;
                    return true;
                }
            }
            if(checkerboard[i,2-i]==empty){
                //如果斜右上方向其他空格被pc占有
                if(checkerboard[(i+1)%3,2-(i+1)%3]==pc&&checkerboard[(i+2)%3,2-(i+2)%3]==pc){
                    PC_x=i;
                    PC_y=2-i;
                    return true;
                }

            }
            //占有横纵行
            for(int j=0;j<3;j++){
                if(checkerboard[i,j]==empty){
                    //占有横行
                    if(checkerboard[i,(j+1)%3]==pc&&checkerboard[i,(j+2)%3]==pc){
                        PC_x=i;
                        PC_y=j;
                        return true;
                    }
                    //占有纵行
                    if(checkerboard[(i+1)%3,j]==pc&&checkerboard[(i+2)%3,j]==pc){
                        PC_x=i;
                        PC_y=j;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    /*搜索使you赢的空位，占有它，从而阻塞you*/
    private bool blockYou() {
        for(int i=0;i<3;i++){
            //阻塞斜行
            if(checkerboard[i,i]==empty){
                //如果斜右下方向其他空格被you占有
                if(checkerboard[(i+1)%3,(i+1)%3]==you&&checkerboard[(i+2)%3,(i+2)%3]==you){
                    PC_x=i;
                    PC_y=i;
                    return true;
                }
            }
            if(checkerboard[i,2-i]==empty){
                //如果斜右上方向其他空格被you占有
                if(checkerboard[(i+1)%3,2-(i+1)%3]==you&&checkerboard[(i+2)%3,2-(i+2)%3]==you){
                    PC_x=i;
                    PC_y=2-i;
                    return true;
                }

            }
            //阻塞横纵行
            for(int j=0;j<3;j++){
                if(checkerboard[i,j]==empty){
                    //阻塞横行
                    if(checkerboard[i,(j+1)%3]==you&&checkerboard[i,(j+2)%3]==you){
                        PC_x=i;
                        PC_y=j;
                        return true;
                    }
                    //阻塞纵行
                    if(checkerboard[(i+1)%3,j]==you&&checkerboard[(i+2)%3,j]==you){
                        PC_x=i;
                        PC_y=j;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    //PC随机下在空位上
    private class Coordinate{
        public int x;
        public int y;
    }
    private bool randomStep() {
        //emptySpace存储所有空位，随机返回空位
        List<Coordinate> emptySpace = new List<Coordinate>();
        //List<int> col = new List<int>();
        int count = 0;
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                if (checkerboard[i,j] == empty) {
                    Coordinate newSpace=new Coordinate();
                    newSpace.x=i;
                    newSpace.y=j;
                    emptySpace.Add(newSpace);
                    count++;
                }
            }
        }
        //index存储随机角标，用来返回随机空位
        if (count != 0) {
            System.Random ran = new System.Random();
            int index = ran.Next(0, count);
            PC_x = emptySpace[index].x;
            PC_y = emptySpace[index].y;
            return true;
        }
        return false;
    }
    //展示棋盘按钮并实现下棋的功能
    private void showCheckerboard(int firstGridX,int firstGridY){
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                if (checkerboard[i, j] == you) {//you下的棋用“X”表示，PC端下的棋用“O"表示
                    GUI.Button(new Rect(firstGridX + i * buttonWidth, firstGridY + j * buttonHeight, buttonWidth, buttonHeight), "X");
                } else if (checkerboard[i, j] == pc) {
                    GUI.Button(new Rect(firstGridX + i * buttonWidth, firstGridY + j * buttonHeight, buttonWidth, buttonHeight), "O");
                } else {
                    if (playing) {//当轮到玩家下棋的时候，玩家点击空的棋格，该棋格被玩家占有并显示"X"
                        if (turn==you) {
                            if (GUI.Button(new Rect(firstGridX + i * buttonWidth, firstGridY + j * buttonHeight, buttonWidth, buttonHeight), "")) {
                                checkerboard[i, j] = you;
                                turn = pc;
                            }
                        } else {//当轮到电脑下棋的时候，电脑按照最优测量选择空的棋格，该棋格被电脑占有并显示"O"
                            GUI.Button(new Rect(firstGridX + i * buttonWidth, firstGridY + j * buttonHeight, buttonWidth, buttonHeight), "");
                            searchForGoodStep();
                            turn = you;
                        }
                    }
                }
            }
        }
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
        //结果提示字体风格
        resultMsgStyle = new GUIStyle {
            fontSize = 20,
            fontStyle = FontStyle.Bold
        };
        //按键字体风格
        ButtonStyle = new GUIStyle("button");
        ButtonStyle.fontSize = 20;
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
        showCheckerboard(firstGridX,firstGridY);
        GUI.enabled = true;
    }
    
    //重置
    private void Reset() {
        playing = true;
        turn = you;
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                checkerboard[i, j] = empty;
            }
        }
        GUI.enabled = true;
    }
}
