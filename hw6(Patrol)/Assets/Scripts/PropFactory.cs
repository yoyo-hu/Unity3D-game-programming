using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFactory : MonoBehaviour
{
    private GameObject patrol = null;                              //巡逻兵
    private List<GameObject> usedPatrol = new List<GameObject>();        //正在被使用的巡逻兵
    private GameObject goldmine = null;                             //金矿
    private List<GameObject> usedgoldmine = new List<GameObject>();      //正在被使用的金矿


    public List<GameObject> GetPatrols()
    {
        int[] pos_x = { -6, 4, 13 };
        int[] pos_z = { -4, 6, -13 };
        int index = 0;
        //生成不同的巡逻兵初始位置
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector3 vec = new Vector3(pos_x[i], 0, pos_z[j]);
                patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"));
                patrol.transform.position = vec;
                patrol.GetComponent<PatrolData>().sign = index + 1;
                patrol.GetComponent<PatrolData>().start_position = vec;
                usedPatrol.Add(patrol);
                index++;
            }
        }

        return usedPatrol;
    }


    public List<GameObject> GetGoldmine()
    {
        float range = 12;                                      //金矿生成的坐标范围
        for (int i = 0; i < 12; i++)
        {
            goldmine = Instantiate(Resources.Load<GameObject>("Prefabs/Goldmine"));
            float ranx = Random.Range(-range, range);
            float ranz = Random.Range(-range, range);
            goldmine.transform.position = new Vector3(ranx, 0, ranz);
            usedgoldmine.Add(goldmine);
        }

        return usedgoldmine;
    }
    
    public void StopPatrol()
    {
        int size = usedPatrol.Count;
        //切换所有侦查兵的动画
        for (int i = 0; i < size; i++)
        {
            usedPatrol[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
