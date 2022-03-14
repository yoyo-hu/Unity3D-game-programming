using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;
public class Controllor : MonoBehaviour, ISceneController, IUserAction{
    public LandModel start_land; //开始陆地
    public LandModel destination; //目标陆地
    public BoatModel boat; //船
    private RoleModel[] roles; //角色
    UserGUI game_GUI; //用户界面
    public Timer timer; //计时器

    void Start (){
        SSDirector.GetInstance().CurrentScenceController = this;//设置导演类的控制器
        game_GUI = gameObject.AddComponent<UserGUI>() as UserGUI;//挂载用户界面组件
        timer = gameObject.AddComponent(typeof(Timer)) as Timer;//挂载计时器组件
        LoadResources();//加载界面资源
    }

    /*定义该函数，便于用户界面获得计时器以及其中的时钟变量*/
    public Timer getTimer(){
        return timer;
    }

    /*创建资源，并将其定义在特定的位置*/
    public void LoadResources(){
        //创建水，陆地，角色，船
        GameObject water = Instantiate(Resources.Load("Perfabs/Water", typeof(GameObject)), new Vector3(0 , -2 , 0), Quaternion.identity) as GameObject;
        water.name = "water"; 
        //创建陆地:两岸
        start_land = new LandModel("start");
        destination = new LandModel("end");
        //创建船
        boat = new BoatModel();
        //创建6个角色
        roles = new RoleModel[6];
        //创建3个牧师
        for (int i = 0; i < 3; ++i){
            RoleModel role = new RoleModel("priest");
            role.SetName("priest" + i);
            role.SetPosition(start_land.GetEmpty());
            role.MoveToLand(start_land);
            start_land.AddRoleOnLand(role);
            roles[i] = role;
        }
        //创建3个恶魔
        for (int i = 0; i < 3; ++i){
            RoleModel role = new RoleModel("devil");
            role.SetName("devil" + i);
            role.SetPosition(start_land.GetEmpty());
            role.MoveToLand(start_land);
            start_land.AddRoleOnLand(role);
            roles[i + 3] = role;
        }
    }
    /*移动船*/
    public void MoveBoat(){
        if (boat.IsEmpty() || game_GUI.sign != 0) return;//船为空或者游戏没有在进行的时候，船不能移动
        boat.BoatMove();
        game_GUI.sign=Check();//每一次移动触发一次检查

    }

    /*移动角色*/
    public void MoveRole(RoleModel role){
        if (game_GUI.sign != 0) return;//如果游戏没有在进行则角色不能移动
        if (role.IsOnBoat()){//当角色在船上时，点击回到船所靠的陆地
            LandModel land;
            if (boat.GetBoatSign() == -1)
                land = destination;
            else
                land = start_land;
            boat.DeleteRoleByName(role.GetName());//删掉船上的该角色
            role.Move(land.GetEmpty());
            role.MoveToLand(land);
            land.AddRoleOnLand(role);//添加该角色到上岸陆地
        }
        else{                                
            LandModel land = role.GetLandModel();
            if (boat.GetEmptyIndex() == -1 || land.GetLandType() != boat.GetBoatSign()) return;   //船没有空位，也不是船停靠的陆地，就不上船

            land.DeleteRoleByName(role.GetName());
            role.Move(boat.GetEmpty());
            role.MoveToBoat(boat);
            boat.AddRoleOnBoat(role);
        }
        game_GUI.sign=Check();//每一次移动触发一次检查

    }

    /*重新开始游戏*/
    public void Restart(){
        timer.Reset();
        start_land.Reset();
        destination.Reset();
        boat.Reset();
        for (int i = 0; i < roles.Length; ++i)
        {
            roles[i].Reset();
        }

    }

    /*检查游戏是否结束*/
    public int Check(){
        int start_priest = (start_land.GetRoleNum())[0];
        int start_devil = (start_land.GetRoleNum())[1];
        int end_priest = (destination.GetRoleNum())[0];
        int end_devil = (destination.GetRoleNum())[1];
        //终点有全部对象，游戏胜利
        if (end_priest + end_devil == 6){
            timer.StopTiming();
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
            timer.StopTiming();
            return 1;
        }
        //终点存在牧师且魔鬼数量大于牧师，牧师被吃，游戏失败
        if (end_priest > 0 && end_priest < end_devil)
        {
            timer.StopTiming();
            return 1;
        }
        return 0;
    }
}
