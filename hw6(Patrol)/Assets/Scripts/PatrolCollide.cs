using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollide : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
    	//玩家进入侦察兵追捕范围，开始追捕
        if (collider.gameObject.tag == "Player")
        {
            //启动追捕模式
            this.gameObject.transform.parent.GetComponent<PatrolData>().follow_player = true;
            //将追捕对象设置为玩家
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = collider.gameObject;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        //玩家跑出侦察兵追捕范围，结束追捕
        if (collider.gameObject.tag == "Player")
        {
            //关闭追捕模式
            this.gameObject.transform.parent.GetComponent<PatrolData>().follow_player = false;
            //将追捕对象设置为空
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = null;
        }
    }
}
