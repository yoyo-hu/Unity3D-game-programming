## 1 作业要求：

- 游戏内容： 井字棋 或 贷款计算器 或 简单计算器 等等
- 技术限制： 仅允许使用 **[IMGUI](https://docs.unity3d.com/Manual/GUIScriptingGuide.html)** 构建 UI
- 作业目的：
  - 了解 OnGUI() 事件，提升 debug 能力
  - 提升阅读 API 文档能力

## 2 资源地址

(代码地址，ps：代码中有详细注释):[https://gitee.com/hurq5/GameDesign3D/tree/master/hw1(Tic-Tac-Toe)]

(在线演示视频):[https://www.bilibili.com/video/BV1Sf4y1E7Te?spm_id_from=333.999.0.0]

## 3 界面图

### 3.1 界面设计：

![请添加图片描述](https://img-blog.csdnimg.cn/a69b44e12e7049cc9865e96a8372fce2.png)


### 3.2 界面展示：

**开始菜单**

![请添加图片描述](https://img-blog.csdnimg.cn/c5d00bedef7545f5813e8a30c7602ab8.png)


**单人模式**

![请添加图片描述](https://img-blog.csdnimg.cn/f972a6faef4c4063930bf090f35645ac.png)


**双人模式**

![请添加图片描述](https://img-blog.csdnimg.cn/ecb56d4aadb344c1b897ef4f0b279ae3.png)


## 4 配置和运行说明

![请添加图片描述](https://img-blog.csdnimg.cn/c516ed24934b4321afea6be9ed435203.png)


### 4.1 建立场景

建立3个场景分别为StartMenu（开始菜单），singlePlayGame(单人游戏)，doublePlayGame（双人游戏）

### 4.2 设置场景对应的运行脚本

分别在每个场景的文件目录下建立空的Gameobject，创建3个c#脚本文件，分别命名为StartMenu（开始菜单），singlePlayGame(单人游戏)，doublePlayGame（双人游戏），并将3个脚本文件分别拉到对应场景（同名场景）目录下的Gameobject中。

### 4.3 设置背景

脚本中定义已经好背景img，场景下的Gameobject详情页会背景详情如下：

![请添加图片描述](https://img-blog.csdnimg.cn/7b13bd22f77b4d09bf4cea7d533b26b8.png)


将选好的背景图片拉到Img方框处即可设置背景

### 4.3 定义和配置皮肤

皮肤的设置同理

按照下图找到GUI Skin对象并点击创建，将该对象拉去到上图Skin方框处即可设置皮肤

![请添加图片描述](https://img-blog.csdnimg.cn/151b9ed547564047897742d46bc35f10.png)


点击资源中的皮肤（以下是我的设置，可以随意挑选自己喜欢的风格）

点击button其下的normal选项：设置其Background参数为Background款式，按钮文字颜色设置为黑色。

![请添加图片描述](https://img-blog.csdnimg.cn/da74fe4c7dd84ac2bb12805650fd70b8.png)


设置button下的border选项的Bottom参数为1，这样按钮就会产生一定的阴影效果

设置button下的border选项的Overflow菜单下的Font Size参数值为30，调整棋盘中X，O图标的大小。

![请添加图片描述](https://img-blog.csdnimg.cn/3de44ab2be0f454fa0611a787cc989ad.png)


## 5 算法思路

### 5.1 开始菜单：

设计标题，按钮，背景图片，字体，皮肤的风格和位置，按照 **[IMGUI](https://docs.unity3d.com/Manual/GUIScriptingGuide.html)** 官网指导编写脚本，设计按下单人游戏按钮切换到单人游戏界面，设计按下双人游戏按钮切换到双人游戏界面。

### 5.2 单人游戏：

设计九宫格，返回菜单按钮，重新开始按钮位置背景图片，字体，皮肤的风格和位置，按照 **[IMGUI](https://docs.unity3d.com/Manual/GUIScriptingGuide.html)** 官网指导编写脚本，在OnGUI()函数中编写下棋界面，OnGUI()快速刷新展示页面。

在单人模式下，设置玩家先下棋子，定义turn等于玩家，当玩家点击按钮后，该位置显示“X",并将turn转给PC，电脑按照以下的最优算法寻找下棋位置：

1. 先优先搜索位置，使得电脑下完该棋后即可胜利；（搜索斜行，横行，纵行，找到一行，满足有一个空格，两个PC占有的格子）；
2. 如果没有找到满足以上要求的格子，再搜索位置，使得电脑占有该格后玩家不能够在下一步取得胜利；（搜索斜行，横行，纵行，找到一行，满足有一个空格，两个玩家占有的格子）；
3. 如果没有找到满足以上要求的格子，再随机选择空格，返回。
4. 没有空格则直接返回。

当PC端下完该步棋后，该位置显示“O",turn转为玩家，由玩家下棋，双方轮流下棋，直到棋盘满了或者有一人胜利。

在OnGUI()函数实现ShowResult(Check())函数来不断的检查是否有胜利者产生，如果有，打印胜利消息并结束该局。

### 5.3 双人游戏：

设计九宫格，返回菜单按钮，重新开始按钮位置背景图片，字体，皮肤的风格和位置，按照 **[IMGUI](https://docs.unity3d.com/Manual/GUIScriptingGuide.html)** 官网指导编写脚本，在OnGUI()函数中编写下棋界面，OnGUI()快速刷新展示页面。

在双人模式下，设置玩家1先下棋子，定义turn等于玩家1，当玩家1点击按钮后，该位置显示“X",并将turn转给玩家2，当玩家2点击按钮后，该位置显示“O",turn又转为玩家1，由玩家1下棋，双方轮流下棋，直到棋盘满了或者有一人胜利。

在OnGUI()函数实现ShowResult(Check())函数来不断的检查是否有胜利者产生，如果有，打印胜利消息并结束该局。

## 6 具体功能实现：

### 6.1 开始界面：

定义全局变量：

```c#
    public Texture2D img;//背景图片
    public GUISkin skin;//界面皮肤
```

定义位置参数：

```c#
        //定义数值参数
        float height = Screen.height * 0.5f;
        float width = Screen.width * 0.5f;
        int ButtonHeight = 50;
        int ButtonWidth = 150;
        int TitleHeight = 80;
        int TitleWidth = 200;
```

Screen.height 和 Screen.width 为当前窗口的高和宽，利用这两个参数，找到界面的中心位置height，width，其他参数ButtonHeight，ButtonWidth是用来表示按钮大小，TitleHeight，TitleWidth表示标题大小。

GUI添加控件的构造函数的参数有一个 GUIStyle 类型的参数，用来设置两个标题的样式，背景图片的样式和按钮字体的样式

```c#
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
```

居中放置标题位置：

```c#
       //放置标题
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", BackgroundStyle);
        GUI.Label(new Rect(width - Ti
        GUI.Label(new Rect(width - TitleWidth / 3, height - TitleHeight * 2, TitleWidth, TitleHeight), "井字棋", tStyle1);
        GUI.Label(new Rect(width - TitleWidth / 3, height - TitleHeight , TitleWidthtleWidth / 3, height - TitleHeight * 2, TitleWidth, TitleHeight), "井字棋", tStyle1);
        GUI.Label(new Rect(width - TitleWidth / 3, height - TitleHeight , TitleWidth, TitleHeight), "开始菜单", tStyle2);
```

设置按钮位置，设置按下按钮后，调用 SceneManager.LoadScene函数实现界面跳转，其中该函数的第二个参数为LoadSceneMode.Single，表示跳转后销毁当前界面，实现如下：

```c#
        //放置模式选择按钮
        if (GUI.Button(new Rect(width - ButtonWidth / 2 - 100, height + ButtonHeight / 4, ButtonWidth, ButtonHeight), "单人模式", ButtonStyle)) {
            SceneManager.LoadScene("singlePlayerGame", LoadSceneMode.Single);//切换到"OnePlayerMode"界面并销毁开始菜单界面
        }

        if (GUI.Button(new Rect(width - ButtonWidth / 2 + 100, height + ButtonHeight / 4, ButtonWidth, ButtonHeight), "双人模式", ButtonStyle)) {
            SceneManager.LoadScene("doublePlayerGame", LoadSceneMode.Single);;//切换到"TwoPlayersMode"界面并销毁开始菜单界面
        }
```

### 6.2 单人游戏界面：

界面的组件有结果消息，九宫格，返回菜单按钮，重新开始按钮组成，通过设置这些组件的左上角坐标来将他们放置在合适的界面位置：

```c#
    //一些坐标位置
    int backButtonX;
    int backButtonY;
    int firstGridX;
    int firstGridY;
    int resetButtonX;
    int resetButtonY;
    int resultMsgX;
    int resultMsgY;
```

棋盘逻辑表示使用int变量，其中empty表示空的，you表示你下的，pc表示电脑下的，都用于填在checkerboard中、

```c#
    private int [,] checkerboard=new int[3,3];//棋盘
    
    private const int empty=0;
    private const int you=1;
    private const int pc=2;
```

全局变量布尔变量playing表示游戏是否正在进行，进行为true，不进行为false，整数变量turn用来指定当前轮轮到谁下棋，可以在单人游戏中可以为上面代码定义的you或者pc。

OnGUi函数代码以及详细注释如下，界面组件的位置根据当前界面的高度，宽度和按钮大小设置，保证组件的居中，该函数设置字体风格，背景，以及放置组件位置，每一次刷新都调用ShowResult(Check());函数检查是否有胜利者。

```c#
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
```

其中displayBackButton()实现如下：

```
    private void displayBackButton(){
        if (GUI.Button(new Rect(backButtonX, backButtonY,buttonWidth/2+buttonWidth, buttonHeight / 2), "返回菜单",ButtonStyle)) {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);//切换到"StartMenu"界面并销毁开始菜单界面
        }
    }
```

displayResetButton()实现如下：

```
    private void displayResetButton(){
        if (GUI.Button(new Rect(resetButtonX, resetButtonY,buttonWidth/2+buttonWidth, buttonHeight / 2), "重新开始",ButtonStyle)) {
            Reset();
            return;
        }
    }
```

检查函数Check()，检查是否有胜利者产生，函数首先检查斜行，为了实现代码的简洁，使用相对坐标，通过(i+1)%3，(i+2)%3找到当前横或纵坐标的另外两个横或纵坐标，实现如下：

```c#
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
```

Check()函数返回胜利的对象，如果没有返回empty；

ShowResult函数接收Check()函数的返回结果并对应打印"你赢了"，"你输了"，或"目前没有赢家" 的结果，如下：

```c#
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
```

棋盘的展示和功能的实现函数showCheckerboard如下， GUI.Button按照设计要求在每一次刷新的时候显示：

1. 棋格逻辑值为you时，显示“X”
2. 棋格逻辑值为pc时，显示“O"
3. 棋格逻辑值为empty时，显示“"，点击后可以相应更改其逻辑值（改为turn的值，即设置为当前玩家点击后占有），在下一轮刷新时显示出来，PC通过调用searchForGoodStep()函数，选择最优的空白棋格进行填写。

```c#
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
```

按照pc端最优选择算法的设计思路，searchForGoodStep()函数应该实现如下，其中makePCWin()搜索一个空位使得PC能赢，该函数找到符合要求的位置时，返回true，否则继续调用blockYou搜索使you赢的空位，占有它，从而阻塞you，该函数找到符合要求的位置时，返回true，否则继续调用randomStep存储所有的空格并随机选择一个作为下棋空位，如果以上三个函数都找不到合适的位置，表示棋格已经占满，直接返回，否则填写选择的棋格逻辑值为pc。

```c#
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
```

和check函数的实现类似，makePCWin一次查找斜行，横纵行，寻找一个行，满足拥有一个空位，和两个逻辑值为pc的棋格，如下：

```c#
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
```

blockYou() 同理，寻找一个行，满足拥有一个空位，和两个逻辑值为you的棋格。

randomStep()函数则使用List<Coordinate> emptySpace = new List<Coordinate>();语句定义的

emptySpace 数组存放所有空格的坐标。通过System.Random获得随机角标，从而或得随机坐标。

Reset()实现棋格重置为空并重新设置turn为有，游戏状态playing为进行中。

### 6.3 双人游戏界面：

界面组件的存放和单人游戏相同，不同的是玩家从“玩家和电脑”变成了”玩家一和玩家二“，在逻辑上只需要将PC的行走策略修改成玩家二的行走策略，即//当轮到玩家2下棋的时候，玩家2点击空的棋格，该棋格被玩家2占有并显示"O"，修改showCheckerboard()函数如下：

```
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
```



