using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldmineCollide : MonoBehaviour
{
	//当玩家与金矿相撞
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && this.gameObject.activeSelf)
        {
            //设置金矿消失
            this.gameObject.SetActive(false);
            //减少金矿数量
            Singleton<GameEventManager>.Instance.ReduceGoldmineNum();
        }
    }
}
