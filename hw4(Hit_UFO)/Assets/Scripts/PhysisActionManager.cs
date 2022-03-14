using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisActionManager : SSActionManager, ISSActionCallback, IActionManager
{
    //飞行动作
    PhysisFlyAction flyAction;
    //控制器
    FirstController controller;
    public List<PhysisFlyAction> seq = new List<PhysisFlyAction>();
    protected new void Start()
    {
        controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
    }

    public void Fly(GameObject disk, float speed, Vector3 direction)
    {
        flyAction = PhysisFlyAction.GetSSAction(direction, speed);
        seq.Add(flyAction);
        RunAction(disk, flyAction, this);
    }

    //回调函数
    public void SSActionEvent(SSAction source,
    SSActionEventType events = SSActionEventType.Competed,
    int intParam = 0,
    string strParam = null,
    Object objectParam = null)
    {
        //飞碟结束飞行后进行回收
        seq.Remove(source as PhysisFlyAction);
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

