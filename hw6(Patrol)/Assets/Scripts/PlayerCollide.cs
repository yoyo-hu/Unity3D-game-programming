using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        //当玩家与侦察兵相撞
        if (other.gameObject.tag == "Player")
        {
            //触发玩家的death变量，使玩家倒地
            other.gameObject.GetComponent<Animator>().SetTrigger("death");
            //触发巡逻兵的shoot变量,使巡逻兵发动攻击
            this.GetComponent<Animator>().SetTrigger("shoot");
            //游戏结束
            Singleton<GameEventManager>.Instance.PlayerGameover();
        }
    }
}
