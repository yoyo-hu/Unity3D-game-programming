# hw3 实验报告

## 1 作业要求

**牧师与魔鬼 动作分离版**

设计一个裁判类，当游戏达到结束条件时，通知场景控制器游戏结束

### 2 实现细节

**在原来代码的基础上，修改如下：**

* 将UserGUI的sign成员变量和Controller的Check方法提取到了Judge中,并在Controller中添加了获得游戏状态的方法和判断游戏状态的方法
* 把UserGUI的IsShowRules成员变量放到了Judge中，并在Controller中添加了设置和获取方法、
* 把BoatModel的IsEmpty提取到了Judge中
* 把RoleModel的IsOnBoat放到了Judge中

**经过修改后：**

用户界面UserGUI就可以通过调用Controller提供用户接口action去询问来自裁判类的判断。

Controller新增加的接口如下：

```
        private Judge judge;
        
        public void setGameState(int state) {
            judge.setGameState(state);
        }

        public int getGameState() {
            return judge.getGameState();
        }

        public bool isPlaying() {
            return judge.getGameState() == 0;
        }

        public bool isLose() {
            return judge.getGameState() == 1;
        }

        public bool isWin() {
            return judge.getGameState() == 2;
        }

        public bool isPause() {
            return judge.getGameState() == 3;
        }
```

**Judge实现如下：**

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mygame {

    public class Judge {
        private int gamestate = 0; //=0表示游戏在进行,=1表示游戏失败，=2表示游戏胜利，=3表示游戏停止
        private bool isShowRules = false;

        public void setGameState(int state) {
            gamestate = state;
        }

        public int getGameState() {
            return gamestate;
        }

        public bool isPlaying() {
            return gamestate == 0;
        }

        public void setIsShowRules(bool flags) {
            isShowRules = flags;
        }

        public bool getIsShowRules() {
            return isShowRules;
        }

        /*判断船是否是空的，空船不能移动*/
        public bool IsEmptyBoat(BoatModel boat) {
            RoleModel[] roles = boat.getRoles();
            for (int i = 0; i < roles.Length; ++i){
                if (roles[i] != null)
                    return false;
            }
            return true;
        }

        public bool IsOnBoat(RoleModel role) {
            return role.getIsOnBoat();
        }

        /*检查游戏是否结束*/
        public int Check(LandModel start_land, LandModel destination, BoatModel boat){
            int start_priest = (start_land.GetRoleNum())[0];
            int start_devil = (start_land.GetRoleNum())[1];
            int end_priest = (destination.GetRoleNum())[0];
            int end_devil = (destination.GetRoleNum())[1];
            //终点有全部对象，游戏胜利
            if (end_priest + end_devil == 6){
                gamestate = 2;
                return 2;
            } 
                
            //统计岸的一边（包括船和陆地上）魔鬼和牧师各自的数量
            int[] boat_role_num = boat.GetRoleNumber();
            if (boat.GetBoatSign() == 1) 
            {
                start_priest += boat_role_num[0];
                start_devil += boat_role_num[1];
            }
            else
            {
                end_priest += boat_role_num[0];
                end_devil += boat_role_num[1];
            }

            //起点存在牧师且魔鬼数量大于牧师，牧师被吃，游戏失败
            if (start_priest > 0 && start_priest < start_devil)
            {      
                gamestate = 1;
                return 1;
            }
            //终点存在牧师且魔鬼数量大于牧师，牧师被吃，游戏失败
            if (end_priest > 0 && end_priest < end_devil)
            {
                gamestate = 1;
                return 1;
            }
            gamestate = 0;
            return 0;
        }
    }
}

```

UserGUI通过调用action.getIsShowRules()，action.isPause()，action.isLose()，action.isWin()来判断是否展现规则，是否暂停，输或者赢。

### 3 游戏结果截图：

开始页面:
![1](https://img-blog.csdnimg.cn/2a57c9e061fc4ede8bdca68b94b8a988.png)
游戏页面：

![2](https://img-blog.csdnimg.cn/f52a1d7912264a3cb0628077d03e6543.png)


查看规则：

![请添加图片描述](https://img-blog.csdnimg.cn/9484355587934ae5aede8a6f9cc3cb99.png)


游戏暂停：

![请添加图片描述](https://img-blog.csdnimg.cn/fb7a7e61ce684c74a31805fffac5ec01.png)


游戏成功：
![请添加图片描述](https://img-blog.csdnimg.cn/64337d8ee97d487ab7da86f3f20de785.png)


游戏失败：

![请添加图片描述](https://img-blog.csdnimg.cn/bf1a2bf2e14343f3a96840e5638e356c.png)