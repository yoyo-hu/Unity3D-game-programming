using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mygame{

    public interface ISceneController{
        void LoadResources();
    }
    
    /*用户进行操作后与游戏中发生响应的接口*/
    public interface IUserAction{ 
        void MoveBoat();                                   //移动船
        Timer getTimer();                                  //用户界面活得计时器
        void Restart();                                    //重新开始
        void MoveRole(RoleModel role);                     //移动角色
        int Check();                                       //检测游戏结束
    }

    
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



    /*陆地模型*/
    public class LandModel{
        GameObject land;                                //陆地对象
        Vector3[] positions;                            //角色放在陆地上的位置
        int land_type;                                  //陆地类型，-1为目的陆地标志，1为起点陆地标志
        RoleModel[] roles = new RoleModel[6];           //在陆地上的角色

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
    }

    /*船模型*/
    public class BoatModel{
        GameObject boat;                                          
        Vector3[] start_vacancy;                //船在起点的空位
        Vector3[] end_vacancy;                  //船在终点的空位
        Move move;                                                    
        Click click;
        int boat_sign = 1;                      //船在起点还是终点,1为起点，-1为终点
        RoleModel[] roles = new RoleModel[2];   //在船上的角色

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
    }

    /*角色模型*/
    public class RoleModel{
        GameObject role;           //角色对象
        int role_sign;             //角色类别标记，0为牧师，1为恶魔
        Click click;
        Move move;
        bool on_boat;              //是否在船上       

        LandModel land_model = (SSDirector.GetInstance().CurrentScenceController as Controllor).start_land;

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
    }

    /*计时器脚本*/
    public class Timer: MonoBehaviour{
        public int hour = 0;            //小时
        public int minute = 0;          //分钟
        public int second = 0;          //秒
        public float time = 0f;         //总时间
        public bool timeStop=false;     //控制时钟是否暂停
        void Start(){
            hour = 0;          //小时
            minute = 0;        //分钟
            second = 0;        //秒
            time = 0f;         //总时间
            timeStop = false;  //是否暂停计时
        }
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
        /*暂停计时*/
        public void StopTiming(){
            timeStop = true;
        }
        /*开始计时*/
        public void beginTiming(){
            timeStop = false;
        }
        /*重置时间*/
        public void Reset(){
            hour = 0;    //小时
            minute = 0;  //分钟
            second = 0;  //秒
            time = 0f;
            timeStop = false;
        }

    }

    /*移动脚本*/
    public class Move : MonoBehaviour{
        float move_speed = 30;                   //移动速度
        int move_sign = 0;                       //0是不动，1移动
        Vector3 end_pos;                         //存储最终位置
        Vector3 middle_pos;                      //存储角色从船移动到陆地或者从陆地移动到船上的轨迹转折点的位置。 

        void Update(){
            if(move_sign!=0 ){//物体可以移动
                transform.position = Vector3.MoveTowards(transform.position, middle_pos, move_speed * Time.deltaTime);
                if( transform.position == end_pos) move_sign=0;//只有当物体到达终点时，物体才停止移动
                else if (transform.position == middle_pos && middle_pos != end_pos){//转折点不是终点，需要再次移动
                    middle_pos=end_pos;
                }               
            }
        }

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
    }

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

}

