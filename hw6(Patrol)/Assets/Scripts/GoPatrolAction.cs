using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPatrolAction : SSAction
{
    private enum Dirction { EAST, NORTH, WEST, SOUTH };
    private float pos_x, pos_z;                  //移动前的初始x和z方向坐标
    private float move_length;                   //移动的长度
    private float move_speed = 1.2f;             //移动速度
    private bool Is_reach_des = true;            //是否到达目的地
    private Dirction dirction = Dirction.EAST;   //移动的方向
    private PatrolData patrol_data;              //侦察兵的数据


    private GoPatrolAction() { }
    public static GoPatrolAction GetSSAction(Vector3 location)
    {
        GoPatrolAction action = CreateInstance<GoPatrolAction>();
        action.pos_x = location.x;
        action.pos_z = location.z;
        //设定移动矩形的边长
        action.move_length = Random.Range(5, 8);
        return action;
    }
    void Gopatrol()
    {
        if (Is_reach_des)
        {
            switch (dirction)
            {
                case Dirction.EAST:
                    pos_x -= move_length;
                    break;
                case Dirction.NORTH:
                    pos_z += move_length;
                    break;
                case Dirction.WEST:
                    pos_x += move_length;
                    break;
                case Dirction.SOUTH:
                    pos_z -= move_length;
                    break;
            }
            Is_reach_des = false;
        }
        this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
        float distance = Vector3.Distance(transform.position, new Vector3(pos_x, 0, pos_z));
        //当前位置与目的地距离浮点数的比较
        if (distance > 0.9)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pos_x, 0, pos_z), move_speed * Time.deltaTime);
        }
        else //转弯
        {
            dirction = dirction + 1;
            if (dirction > Dirction.SOUTH)
            {
                dirction = Dirction.EAST;
            }
            Is_reach_des = true;
        }
    }

    public override void Update()
    {
        //侦察移动
        Gopatrol();
        //如果侦察兵需要跟随玩家并且玩家就在侦察兵所在的区域，侦查动作结束
        if (patrol_data.follow_player && patrol_data.wall_sign == patrol_data.sign)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this, 0, this.gameobject);
        }
    }
    public override void Start()
    {
        this.gameobject.GetComponent<Animator>().SetBool("run", true);
        patrol_data = this.gameobject.GetComponent<PatrolData>();
    }


}
