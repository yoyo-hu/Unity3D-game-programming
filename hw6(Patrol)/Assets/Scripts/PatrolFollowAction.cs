using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollowAction : SSAction
{
    private float speed = 2f;                  //跟随玩家的速度
    private GameObject player;                 //玩家
    private PatrolData patrol_data;             //侦查兵数据

    private PatrolFollowAction() { }
    public static PatrolFollowAction GetSSAction(GameObject player)
    {
        PatrolFollowAction action = CreateInstance<PatrolFollowAction>();
        action.player = player;
        return action;
    }

    public override void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        this.transform.LookAt(player.transform.position);
        //如果侦察兵没有跟随对象，或者需要跟随的玩家不在侦查兵的区域内
        if (!patrol_data.follow_player || patrol_data.wall_sign != patrol_data.sign)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this, 1, this.gameobject);
        }
    }

    public override void Start()
    {
        patrol_data = this.gameobject.GetComponent<PatrolData>();
    }

}
