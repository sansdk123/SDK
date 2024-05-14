using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 地面检测 : MonoBehaviour
{
    public bool 摸着;//是否摸着判定

    private void OnTriggerStay(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.tag == "地面")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            摸着 = true;
        }
    }

    private void OnTriggerExit(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.tag == "地面")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            摸着 = false;
        }
    }

}
