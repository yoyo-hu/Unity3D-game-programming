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
