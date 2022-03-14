## HW2 牧师与魔鬼 实验报告

### 1  游戏要求

#### 1.1 阅读以下游戏脚本

> Priests and Devils
>
> Priests and Devils is a puzzle game in which you will help the Priests and Devils to cross the river within the time limit. There are 3 priests and 3 devils at one side of the river. They all want to get to the other side of this river, but there is only one boat and this boat can only carry two persons each time. And there must be one person steering the boat from one side to the other side. In the flash game, you can click on them to move them and click the go button to move the boat to the other direction. If the priests are out numbered by the devils on either side of the river, they get killed and the game is over. You can try it in many > ways. Keep all priests alive! Good luck!

#### 1.2 程序需要满足的要求：

- play the game ( http://www.flash-game.net/game/2535/priests-and-devils.html )
- 列出游戏中提及的事物（Objects）
- 用表格列出玩家动作表（规则表），注意，动作越少越好
- 请将游戏中对象做成预制
- 在 GenGameObjects 中创建 长方形、正方形、球 及其色彩代表游戏中的对象。
- 使用 C# 集合类型 有效组织对象
- 整个游戏仅 主摄像机 和 一个 Empty 对象， **其他对象必须代码动态生成！！！** 。 整个游戏不许出现 Find 游戏对象， SendMessage 这类突破程序结构的 通讯耦合 语句。 **违背本条准则，不给分**
- 请使用课件架构图编程，**不接受非 MVC 结构程序**
- 注意细节，例如：船未靠岸，牧师与魔鬼上下船运动中，均不能接受用户事件！

### 2 项目资源

[游戏演示视频](https://www.bilibili.com/video/BV1E341117pa/)

[项目代码](https://gitee.com/hurq5/GameDesign3D/tree/master/hw2(priests%20and%20devils))

### 3 游戏截图

开始页面:
![1](https://img-blog.csdnimg.cn/2a57c9e061fc4ede8bdca68b94b8a988.png)
游戏页面：

![2](https://img-blog.csdnimg.cn/f52a1d7912264a3cb0628077d03e6543.png)


查看规则：

![请添加图片描述](https://img-blog.csdnimg.cn/9484355587934ae5aede8a6f9cc3cb99.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA56eD5aS05pm05bey5LiK57q_,size_20,color_FFFFFF,t_70,g_se,x_16)


游戏暂停：

![请添加图片描述](https://img-blog.csdnimg.cn/fb7a7e61ce684c74a31805fffac5ec01.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA56eD5aS05pm05bey5LiK57q_,size_20,color_FFFFFF,t_70,g_se,x_16)


游戏成功：
![请添加图片描述](https://img-blog.csdnimg.cn/64337d8ee97d487ab7da86f3f20de785.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA56eD5aS05pm05bey5LiK57q_,size_20,color_FFFFFF,t_70,g_se,x_16)


游戏失败：

![请添加图片描述](https://img-blog.csdnimg.cn/bf1a2bf2e14343f3a96840e5638e356c.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA56eD5aS05pm05bey5LiK57q_,size_20,color_FFFFFF,t_70,g_se,x_16)


###  4 要求满足

#### 4.1 列出游戏中提及的事物（Objects）

魔鬼，牧师，船，河，两岸

#### 4.2 用表格列出玩家动作表（规则表），注意，动作越少越好

基本动作表：

| 动作                | 执行条件                                                     | 执行结果                |
| ------------------- | ------------------------------------------------------------ | ----------------------- |
| 点击船上的牧师/魔鬼 | 1. 游戏进行中（未结束，未暂停）<br />2. 角色在船上<br />3. 船在岸边 | 牧师/魔鬼跳上船靠近的岸 |
| 点击岸上的牧师/魔鬼 | 1.  游戏进行中（未结束，未暂停）<br />2. 角色在岸上<br />3. 船在角色所在的岸边 | 牧师/魔鬼跳上船         |
| 点击船              | 1. 游戏进行中（未结束，未暂停）<br />2. 船不为空<br />3. 船在岸边 | 船移动到对岸            |

附加功能：

| 动作                  | 执行条件                     | 执行结果                                                     |
| --------------------- | ---------------------------- | ------------------------------------------------------------ |
| 点击"game rules"按钮  | 没有rules显示                | 显示rules                                                    |
| 点击"game rules"按钮  | 有rules显示                  | 隐藏rules                                                    |
| 点击"return menu"按钮 | 无                           | 返回菜单页面，当前游戏页面销毁                               |
| 点击"restart"按钮     | 无                           | 游戏重新开始，对象回到起点，计时清零                         |
| 点击"pause"按钮       | 游戏进行中（未结束，未暂停） | 1. 游戏暂停，计时停止，<br />2.出现"Return Game"按钮，点击返回游戏<br />3.界面不能操作 |

#### 4.3 游戏中对象被做成预制

所有对象都在`Models.cs`文件中，以类的形式预制好，等待`controller`类调用，动态创建对象实例。

#### 4.4 在 GenGameObjects 中创建 长方形、正方形、球 及其色彩代表游戏中的对象

在`controllor.cs`文件中的`LoadResources`函数实现长方形、正方形、球 及其色彩代表游戏中的对象的创建。

#### 4.5 使用 C# 集合类型 有效组织对象

在船上，陆地上等使用`RoleModel[] roles`来存储角色集合，使用`Vector3[] positions`来存储位置集合等。

#### 4.6 整个游戏仅主摄像机和一个 Empty 对象

**其他对象必须代码动态生成！！！** 。 整个游戏不许出现 Find 游戏对象， SendMessage 这类突破程序结构的 通讯耦合 语句。 **违背本条准则，不给分**

游戏开始前：

![请添加图片描述](https://img-blog.csdnimg.cn/b6bbba5965b84a6fa7f9dedfca6b7db4.png)


游戏开始后：

![请添加图片描述](https://img-blog.csdnimg.cn/9cb907a61dea49439ca4906bf27275c2.png)


#### 4.7 请使用课件架构图编程，**不接受非 MVC 结构程序**

项目使用MVC结构：

Model（模型层）：（在Models.cs中实现）
在这一层主要就是存放用户的数据，UI的数据，静态字段，数据存储，以及模型贴图资源的存储

View（视图层）：（在UserGUI.cs中实现）
在这一层主要是放一些UI参数，获取UI数据，获取按钮事件等

Controller（控制层）（在Controllor.cs中实现）
这一层就是去实现业务逻辑功能，获取Model的数据，通知View层更新数据，承上启下的功能

#### 4.8 注意细节，例如：船未靠岸，牧师与魔鬼上下船运动中，均不能接受用户事件！

在进行动作表中的动作时，先判断是否满足执行条件。

### 5 项目配置

1. 创建新项目；

2. 使用链接[]中的assets文件夹覆盖新项目中的assets文件夹；

3. 选择startMenu，开始游戏

4. 当点击startMenu开始游戏是，会出现页面跳转的报错，按照警告处理，需要将两个场景按照以下步骤添加到项目场景列表：

   说明：Scenes In Build 面板会显示被 Unity 包含在构建中的项目场景列表。如果在此面板中看不到任何场景，请选择 **Add Open Scenes**，将所有当前打开的场景添加到构建中。也可以将场景资源从 Project 窗口拖入该窗口

   ![配置](https://img-blog.csdnimg.cn/9b66a31a2117448aad4a3bb9376412b0.png)

5. 添加完毕，重新选择startMenu，开始游戏

### 6 模块介绍

#### 6.1 SSDirector

利用单例模式创建导演，继承于System.Object，保持导演类一直存在，不被Unity内存管理而管理，导演类指挥安排场景，场景切换等。

```c#
    /*导演类：掌控着场景的加载、切换等*/
    public class SSDirector : System.Object{
        private static SSDirector _instance;              //导演类的实例
        public ISceneController CurrentScenceController { get; set; }
        public static SSDirector GetInstance(){
            if (_instance == null){
                _instance = new SSDirector();
            }
            return _instance;
        }
    }
```

#### 6.2 ISceneController

场景控制器的接口，是导演与场景控制器沟通的接口，利用这个接口，得知当前场景是由哪个控制，然后向场景控制器传达要求等。

```c#
    public interface ISceneController{
        void LoadResources();
    }
```

#### 6.3 IUserAction

用户进行操作后与游戏中发生响应的接口，用户通过键盘、鼠标等对游戏发出指令，这个指令会触发游戏中的一些行为，如角色移动)。

```c#
    /*用户进行操作后与游戏中发生响应的接口*/
    public interface IUserAction{ 
        void MoveBoat();                                   //移动船
        Timer getTimer();                                  //用户界面活得计时器
        void Restart();                                    //重新开始
        void MoveRole(RoleModel role);                     //移动角色
        int Check();                                       //检测游戏结束
    }
```

#### 6.4 Controllor 

控制器，对场景中的具体对象进行创建操作，控制器继承了接口类并实现了它们的方法。

代码框架如下：(只列出成员函数的声明，具体实现见完整代码)

```c#
public class Controllor : MonoBehaviour, ISceneController, IUserAction{
    public LandModel start_land; //开始陆地
    public LandModel destination; //目标陆地
    public BoatModel boat; //船
    private RoleModel[] roles; //角色
    UserGUI game_GUI; //用户界面
    public Timer timer; //计时器
    
    void Start (){
        SSDirector.GetInstance().CurrentScenceController = this;//设置导演类的控制器
        game_GUI = gameObject.AddComponent<UserGUI>() as UserGUI;//添加用户界面组件
        timer = gameObject.AddComponent(typeof(Timer)) as Timer;//添加导演类组件
        LoadResources();//加载界面资源
    }
    /*定义该函数，便于用户界面获得计时器以及其中的时钟变量*/
    public Timer getTimer();
    /*创建资源，并将其定义在特定的位置*/
    public void LoadResources;
    /*移动船*/
    public void MoveBoat();
    /*移动角色*/
    public void MoveRole(RoleModel role);
    /*重新开始游戏*/
    public void Restart();
    /*检测游戏是否结束*/
    public int Check();
```

#### 6.5 UserGUI

建立用户的交互界面，其中使用sign变量来表示游戏进行的状态，=0表示游戏在进行,=1表示游戏失败，=2表示游戏胜利，=3表示游戏停止。

该模块实现了计时器的设置，查看游戏按钮，返回菜单按钮，重新开始按钮的放置和触发事件，以及游戏结局的展示。

```c#
public class UserGUI : MonoBehaviour {

    
    private IUserAction action;
    public int sign = 0;//=0表示游戏在进行,=1表示游戏失败，=2表示游戏胜利，=3表示游戏停止
    bool isShowRules = false;
    public string timeStr = string.Empty;
    void Start(){
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
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
            isShowRules = !isShowRules;

        }

        /*展示游戏规则*/
        if(isShowRules){
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
            sign = 0;
            action.Restart();
        }

        /*游戏暂停按钮*/
        if(GUI.Button(new Rect(340, 10, 100, 30), "pause", button_style)){
            if(sign==0){//游戏正在进行
                sign = 3;//游戏的状态发生改变
                action.getTimer().StopTiming();//事件暂停
            }
        }

        /*游戏暂停展示*/
        if(sign == 3){//游戏暂停，打印暂停并显示“Return Game”按钮提示玩家可以点击回到游戏中
            GUI.Label(new Rect(Screen.width / 2-100, Screen.height / 2-120, 100, 50), "Game pause!", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 120, 50), "Return Game", button_style)){
                //回到游戏，游戏状态发生改变，时间开始计时
                sign = 0;
                action.getTimer().beginTiming();
            }
        }

        /*游戏结局展示*/
        if (sign == 1){//游戏失败，打印失败并显示“Try again”按钮提示玩家再进行尝试
            GUI.Label(new Rect(Screen.width / 2-90, Screen.height / 2-120, 100, 50), "Gameover!", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 100, 50), "Try again", button_style))
            {
                sign = 0;
                action.Restart(); 
            }
        }
        else if (sign == 2){//游戏成功，打印成狗并显示“Play again”按钮提示玩家再玩一局
            GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 120, 100, 50), "You win!", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 100, 50), "Play again", button_style)){
                sign = 0;
                action.Restart();
            }
        }
    }
}
```

####  6.6 StartMenu

开始菜单，主要设置背景照片，标题，以及开始游戏按钮，按下开始按钮后，程序调用`SceneManager.LoadScene`实现页面的跳转。

```c#
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
            SceneManager.LoadScene("game", LoadSceneMode.Single);;//切换到"game"界面并销毁开始菜单界面
        }
    }
}

```

#### 6.7 Mygame游戏模型

游戏场景中可交互的游戏模型，包括角色模型，船模型，陆地模型。


##### 6.7.1 LandModel(陆地模型)

1. 模型的成员变量如下，其中land_sign标志位用来来记录实例为开始陆地还是到达陆地。

   ```c#
       GameObject land;                        //陆地对象
       Vector3[] positions;                    //保存每个角色放在陆地上的位置
       int land_sign;                          //到达陆地标志为-1，开始陆地标志为1
       RoleModel[] roles = new RoleModel[6];   //陆地上有的角色
   ```

   

2. 模型的成员函数：根据需求设计函数，包括：活得该陆地上牧师和魔鬼各自的数量，从陆地上移除角色，给该陆地添加角色，得到空位，重置等。

   具体函数声明如下：

   ```c
           /*构造函数*/
           public LandModel(string land_type_string){
               //设置角色放在陆地上的位置
               positions = new Vector3[] {new Vector3(5.3F,-0.3F,0), new Vector3(6.1F,-0.3F,0), new Vector3(6.9F,-0.3F,0),
                   new Vector3(7.7F,-0.3F,0), new Vector3(8.5F,-0.3F,0), new Vector3(9.3F,-0.3F,0)};
               
               if (land_type_string == "start"){
                   //设置起点
                   land = Object.Instantiate(Resources.Load("Perfabs/Stone", typeof(GameObject)), new Vector3(8, -1.5F, 0), Quaternion.identity) as GameObject;
                   land_type = 1;
               }
               else if (land_type_string == "end"){
                   //设置终点
                   land = Object.Instantiate(Resources.Load("Perfabs/Stone", typeof(GameObject)), new Vector3(-8, -1.5F, 0), Quaternion.identity) as GameObject;
                   land_type = -1;
               }
           }
   
           /*返回空位角标*/
           public int GetEmptyIndex(){
               for (int i = 0; i < roles.Length; ++i){
                   if (roles[i] == null)
                       return i;
               }
               return -1;          //没有则返回-1
           }
   
           /*成员变量LandType的get函数*/
           public int GetLandType() { return land_type; }
           
           /*返回空位*/
           public Vector3 GetEmpty(){
               Vector3 pos = positions[GetEmptyIndex()];
               pos.x = land_type * pos.x;          //利用两岸的x坐标对称
               return pos;
           }
   
           /*添加角色在该陆地上*/
           public void AddRoleOnLand(RoleModel role){
               roles[GetEmptyIndex()] = role;
           }
   
           /*删除该陆地上的指定角色*/
           public RoleModel DeleteRoleByName(string role_name) { 
               for (int i = 0; i < roles.Length; ++i){
                   if (roles[i] != null && roles[i].GetName() == role_name){
                       RoleModel role = roles[i];
                       roles[i] = null;
                       return role;
                   }
               }
               return null;
           }
   
           /*得到角色数量*/
           public int[] GetRoleNum(){
               int[] count = { 0, 0 };                    //count[0]是牧师数，count[1]是魔鬼数
               for (int i = 0; i < roles.Length; ++i){
                   if (roles[i] != null){
                       if (roles[i].GetSign() == 0)
                           count[0]++;
                       else
                           count[1]++;
                   }
               }
               return count;
           }
   
           /*重置*/
           public void Reset(){
               roles = new RoleModel[6];
           }
   ```

   巧妙之处：根据陆地的摆放是按照z轴对称的，可以通过x坐标乘陆地的标志得到陆地空位置。

```c
        /*返回空位*/
        public Vector3 GetEmpty(){
            Vector3 pos = positions[GetEmptyIndex()];
            pos.x = land_type * pos.x;          //利用两岸的x坐标对称
            return pos;
        }
```

##### 6.7.2 BoatModel(船模型)

1. 模型的成员变量如下：包括：船在起点/终点的位置，船上载有的角色，船在起点/终点的标记变量。

   ```c
           GameObject boat;                                          
           Vector3[] start_vacancy;                //船在起点的空位
           Vector3[] end_vacancy;                  //船在终点的空位
           Move move;                                                    
           Click click;
           int boat_sign = 1;                      //船在起点还是终点,1为起点，-1为终点
           RoleModel[] roles = new RoleModel[2];   //在船上的角色
   ```

2. 模型的成员函数

   ```c
           /*构造函数，完成一些简单的初始化*/
           public BoatModel(){
               boat = Object.Instantiate(Resources.Load("Perfabs/Boat", typeof(GameObject)), new Vector3(4, -1.5F, 0), Quaternion.identity) as GameObject;//复制实例化对象，指定其位置并返回
               boat.name = "boat";
               move = boat.AddComponent(typeof(Move)) as Move;
               click = boat.AddComponent(typeof(Click)) as Click;
               click.SetBoat(this);
               start_vacancy = new Vector3[] { new Vector3(3.5F, -1, 0), new Vector3(4.5F, -1, 0) };
               end_vacancy = new Vector3[] { new Vector3(-4.5F, -1, 0), new Vector3(-3.5F, -1, 0) };
           }
   
           /*判断船是否是空的，空船不能移动*/
           public bool IsEmpty(){
               for (int i = 0; i < roles.Length; ++i){
                   if (roles[i] != null)
                       return false;
               }
               return true;
           }
   
           /*根据所在位置，确定移动方向*/
           public void BoatMove(){
               if (boat_sign == -1){
                   
                   move.MovePosition(new Vector3(4, -1.5F, 0));
                   boat_sign = 1;
               }
               else{
                   move.MovePosition(new Vector3(-4, -1.5F, 0));
                   boat_sign = -1;
               }
           }
   
           /*放回当前位置的标志*/
           public int GetBoatSign(){ return boat_sign;}
   
           public RoleModel DeleteRoleByName(string role_name){
               for (int i = 0; i < roles.Length; ++i){
                   if (roles[i] != null && roles[i].GetName() == role_name){
                       RoleModel role = roles[i];
                       roles[i] = null;
                       return role;
                   }
               }
               return null;
           }
           
           /*返回空位的角标*/
           public int GetEmptyIndex(){
               for (int i = 0; i < roles.Length; ++i){
                   if (roles[i] == null){
                       return i;
                   }
               }
               return -1;
           }
   
           /*返回空位*/
           public Vector3 GetEmpty(){
               Vector3 pos;
               if(boat_sign == 1)
                   pos = start_vacancy[GetEmptyIndex()];
               else
                   pos = end_vacancy[GetEmptyIndex()];
               return pos;
           }
   
           /*在船上添加角色*/
           public void AddRoleOnBoat(RoleModel role){
               roles[GetEmptyIndex()] = role;
           }
   
           /*返回船*/
           public GameObject GetBoat(){ return boat; }
   
           /*重置*/
           public void Reset(){
               if (boat_sign == -1)
                   BoatMove();
               roles = new RoleModel[2];
           }
   
           /*得到船上角色的数量*/
           public int[] GetRoleNumber(){
               int[] count = { 0, 0 };
               for (int i = 0; i < roles.Length; ++i){
                   if (roles[i] == null)
                       continue;
                   if (roles[i].GetSign() == 0)
                       count[0]++;
                   else
                       count[1]++;
               }
               return count;
           }
   ```

##### 6.7.3 RoleModel(角色模型)

1. 模型的成员变量如下：包括：船在起点/终点的位置，船上载有的角色，船在起点/终点的标记变量。

   ```c
           GameObject role;           //角色对象
           int role_sign;             //角色类别标记，0为牧师，1为恶魔
           Click click;
           Move move;
           bool on_boat;              //是否在船上 
   		LandModel land_model = (SSDirector.GetInstance().CurrentScenceController as Controllor).start_land;
   ```

2. 模型的成员函数:

   ```c
           /*构造函数初始化*/
           public RoleModel(string role_name){
               if (role_name == "priest"){
                   role = Object.Instantiate(Resources.Load("Perfabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -8, 0)) as GameObject;
                   role_sign = 0;
               }
               else{
                   role = Object.Instantiate(Resources.Load("Perfabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -8, 0)) as GameObject;
                   role_sign = 1;
               }
               move = role.AddComponent(typeof(Move)) as Move;
               click = role.AddComponent(typeof(Click)) as Click;
   
               click.SetRole(this);
           }
   
           /*get,set函数*/
           public int GetSign() { return role_sign;}
           public LandModel GetLandModel(){return land_model;}
           public string GetName() { return role.name; }
           public bool IsOnBoat() { return on_boat; }
           public void SetName(string name) { role.name = name; }
           public void SetPosition(Vector3 pos) { role.transform.position = pos; }
   
           /*角色移动*/
           public void Move(Vector3 vec){
               move.MovePosition(vec);
           }
   
           public void MoveToLand(LandModel land){  
               role.transform.parent = null;
               land_model = land;
               on_boat = false;
           }
   
           public void MoveToBoat(BoatModel boat){
               role.transform.parent = boat.GetBoat().transform;
               land_model = null;          
               on_boat = true;
           }
   
           /*重置*/
           public void Reset(){
               land_model = (SSDirector.GetInstance().CurrentScenceController as Controllor).start_land;
               MoveToLand(land_model);
               SetPosition(land_model.GetEmpty());
               land_model.AddRoleOnLand(this);
           }
   ```



#### 6.8 Mygame脚本

一些脚本，包括计时器脚本，移动脚本，点击脚本。

预制体通过使用`AddComponent`函数的方式将`Move`脚本，`chick`脚本挂在到其上。

控制器通过使用`AddComponent`函数的方式将`Timer`计时器脚本挂在到其上，方便控制器对时间进行操作。

##### 6.8.1 Timer脚本

设置基本成员变量时`hour`，钟`minute`，秒`second`，总时间`time`，以及控制时间暂停的标志位`timeStop`,

在Start函数和reset函数中设置变量的初始值

```c
            hour = 0;    //小时
            minute = 0;  //分钟
            second = 0;  //秒
            time = 0f;
            timeStop = false;
```

在Update函数中设置动态计时

```c
        void Update(){
            //计时
            if(!timeStop){
                time += Time.deltaTime;
                if (time >= 1f){
                    second++;
                    time = 0f;
                }
                if (second >= 60){
                    minute++;
                    second = 0;
                }
                if (minute >= 60){
                    hour++;
                    minute = 0;
                }
                if (hour >= 99){
                    hour = 0;
                }
            }
            
        }
```

设计暂停和开始计时函数，方便控制器对计时过程进行控制：

```c
        /*暂停计时*/
        public void StopTiming(){
            timeStop = true;
        }
        /*开始计时*/
        public void beginTiming(){
            timeStop = false;
        }
```

##### 6.8.2 Move脚本

设置成员变量如下：

```c#
        float move_speed = 30;                   //移动速度
        int move_sign = 0;                       //0是不动，1移动
        Vector3 end_pos;                         //存储最终位置
        Vector3 middle_pos;                      //存储角色从船移动到陆地或者从陆地移动到船上的轨迹转折点的位置。 

```

对于move_sign变量，但物体被点击移动时，调用MovePosition函数，设置move_sign=1，Update只有再move_sign=1（可以移动时），才调用MoveTowards函数使得函数进行移动，只有当物体移动到终点位置时，move_sign才更改为0表示物体不移动。

当move_sign=1时，物体开始移动，物体移动的方式时先不断的移动到转折点，再不断的移动到终点，如下：

```c#
        void Update(){
            if(move_sign!=0 ){//物体可以移动
                transform.position = Vector3.MoveTowards(transform.position, middle_pos, move_speed * Time.deltaTime);
                if( transform.position == end_pos) move_sign=0;//只有当物体到达终点时，物体才停止移动
                else if (transform.position == middle_pos && middle_pos != end_pos){//转折点不是终点，需要再次移动
                    middle_pos=end_pos;
                }               
            }
        }
```

假设起点为(x1,y1),终点为(x2,y2)，转折点的设置方法如下：

| 移动事件           | 判断条件                         | 转折点  |
| ------------------ | -------------------------------- | ------- |
| 船水平移动         | 起点位置和终点位置y坐标相同      | 即终点  |
| 角色从陆地移动到船 | 起点位置y坐标大于终点位置y坐标   | (x2,y1) |
| 角色从船移动到陆地 | 起点位置y坐标小于于终点位置y坐标 | (x1,y2) |

具体代码如下：

```c#
        public void MovePosition(Vector3 position){
            end_pos = position;
            middle_pos = position;
            if (position.y < transform.position.y){     //角色从陆地移动到船
                middle_pos = new Vector3(position.x, transform.position.y, position.z);
            }
            else{                                        //角色从船移动到陆地
                middle_pos = new Vector3(transform.position.x, position.y, position.z);
            }
            move_sign = 1;//设置物体移动
        }
```



##### 6.8.3 Click脚本

用来检测船和角色是否被点击(这里别忘了，物体加上了Collider才能实现检测点击事件发生 , 当用户在 [Collider](https://docs.unity.cn/cn/2019.4/ScriptReference/Collider.html) 上按下鼠标按钮时，将调用 [OnMouseDown](https://docs.unity.cn/cn/2019.4/ScriptReference/MonoBehaviour.OnMouseDown.html)。

```c#
    /*鼠标点击脚本*/
    public class Click : MonoBehaviour{
        IUserAction action;
        RoleModel role = null;
        BoatModel boat = null;
        public void SetRole(RoleModel role){
            this.role = role;
        }
        public void SetBoat(BoatModel boat){
            this.boat = boat;
        }
        void Start(){
            action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        }
        void OnMouseDown(){
            if (boat == null && role == null) return;//被点击的对象为空时返回
            if (boat != null){//点击的对象为船，触发船进行移动
                action.MoveBoat();
            }
            else if(role != null){//点击的角色为船，触发角色进行移动
                action.MoveRole(role);
            }
        }
    }
```

### 7 附加功能实现

#### 7.1 计时器

1. 在Models中定义计时器脚本（具体实现见5.7.1),在控制器启动的时候（Start函数中）调用`timer = gameObject.AddComponent(typeof(Timer)) as Timer;//挂载计时器组件`语句挂载计时器，对其进行控制。

2. 在游戏胜利或者失败时，调用`timer.StopTiming()`函数使事件停止，但游戏重新开始的时候（调用控制器的Restart函数时），Restart函数调用`timer.Reset()`使时间清零。
3. 控制器还定义了`getTimer()`,并将其加入到界面可以调用的接口中，方便界面对动态的时间进行界面的展示。

#### 7.2 游戏暂停按钮

1. 用户界面设置标志位sign 其中=0表示游戏在进行,=1表示游戏失败，=2表示游戏胜利，=3表示游戏停止。

2. 所有的事件都是在sign =0即游戏进行中才能触发的，因此只要改变该标志位，就能屏蔽事件的产生。

3. 在用户体验上，当点击暂停按钮时候，我们需要通过文字展示告知用户暂停，停止计时，并提供返回游戏的按钮帮助用户重新回到游戏中，sign重新调整到0，并且重新开始计时。

   具体代码见5.5

#### 7.3 游戏规则展示按钮

在UserCUI.cs文件中定义isShowRules变量来表示是否对规则进行展示，当点击按钮时候，会调用`isShowRules = !isShowRules`，即显示或者隐藏规则。

#### 7.4 开始菜单

代码解释见5.6，开始菜单主要设计开始游戏按钮，点击该按钮后，程序调用`SceneManager.LoadScene("game", LoadSceneMode.Single)`切换到"game"界面并销毁开始菜单界面。

#### 7.5 返回菜单按钮

返回菜单按钮同6.4开始游戏按钮调用`SceneManager.LoadScene("startMenu", LoadSceneMode.Single)`切换到"game"界面并销毁当前界面。

#### 7.6 游戏重新开始按钮

修改sign=0，并通过用户界面接口`action = SSDirector.GetInstance().CurrentScenceController as IUserAction`,调用控制器的Restart函数重置界面，使所有对象回到初始状态。
