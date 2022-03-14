using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    // public GameObject disk_Prefab;
    private List<DiskData> used = new List<DiskData>();             //正被使用的飞碟
    private List<DiskData> free = new List<DiskData>();           //空闲的飞碟

    public GameObject GetDisk(int round){
        GameObject disk;
        //如果有空闲的飞碟，则直接使用，否则生成一个新的
        if (free.Count == 0){
            GameObject disk_Prefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk_Prefab"), Vector3.zero, Quaternion.identity);
            disk_Prefab.SetActive(false);
            disk = GameObject.Instantiate<GameObject>(disk_Prefab, Vector3.zero, Quaternion.identity);
            disk.AddComponent<DiskData>();
        }
        else{

            disk = free[0].gameObject;
            free.RemoveAt(0);
        }

        float MaxSpeed = round + 4;
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color color = new Color(r, g, b);
        disk.GetComponent<Renderer>().material.color = color;
        disk.GetComponent<DiskData>().speed = Random.Range(0f, MaxSpeed);
        disk.GetComponent<DiskData>().direction = new Vector3(UnityEngine.Random.Range(-1f, 1f) > 0 ? 2 : -2, 1, 0);
        float level = UnityEngine.Random.Range(0, 2f) * (round + 1);
        used.Add(disk.GetComponent<DiskData>());

        return disk;
    }

    public void FreeDisk(GameObject disk)
    {
        //找到使用中的飞碟，将其踢出并加入到空闲队列
        foreach (DiskData diskData in used)
        {
            if (diskData.gameObject.GetInstanceID() == disk.GetInstanceID())
            {
                disk.SetActive(false);
                free.Add(diskData);
                used.Remove(diskData);
                break;
            }

        }
    }
}
