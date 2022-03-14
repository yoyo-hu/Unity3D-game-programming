using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public GameObject follow;            //跟随的物体
    public float smothing = 5f;          //相机跟随的速度
    Vector3 offset;                      //相机与物体相对偏移位置
    public float sensitivityMouse = 2f;
    public float sensitivetyMouseWheel = 10f;
 
    void Update()
    {
        //滚轮实现镜头缩进和拉远
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            this.GetComponent<Camera>().fieldOfView = this.GetComponent<Camera>().fieldOfView - Input.GetAxis("Mouse ScrollWheel") * sensitivetyMouseWheel;
        }
        //按着鼠标右键实现视角转动
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(-Input.GetAxis("Mouse Y") * sensitivityMouse, Input.GetAxis("Mouse X") * sensitivityMouse, 0);
        }

        Vector3 target = follow.transform.position + offset;
        //摄像机自身位置到目标位置平滑过渡
        transform.position = Vector3.Lerp(transform.position, target, smothing * Time.deltaTime);
    }
    void Start()
    {
        offset = transform.position - follow.transform.position;
    }


}
 