using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback, IActionManager{

    //控制器
    FirstController controller;
    public List<CCFlyAction> seq = new List<CCFlyAction>();
    protected new void Start(){
        controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
    }

    public void Fly(GameObject disk, float speed, Vector3 direction){
        CCFlyAction flyAction = CCFlyAction.GetSSAction(direction, speed);
        seq.Add(flyAction);
        RunAction(disk, flyAction, this);
    }

    //回调函数
    public void SSActionEvent(SSAction source,
    SSActionEventType events = SSActionEventType.Competed,
    int intParam = 0,
    string strParam = null,
    Object objectParam = null){
        //飞碟结束飞行后进行回收
        seq.Remove(source as CCFlyAction);
        controller.FreeDisk(source.gameObject);
    }

    public void Pause(){
        foreach (var k in seq){
            k.enable = false;    
        }
    }

    public void continueGame(){
        foreach (var k in seq){
            k.enable = true;    
        }
    }

}
