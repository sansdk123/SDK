using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物攻击范围 : MonoBehaviour
{
    private 怪物 怪物;//脚本名+ 自取名 shuju1  （private私人的不能看）

    void Start()
    {
        怪物 = GameObject.Find("怪物").GetComponent<怪物>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
    }

    void Update()
    {
    }



    private void OnTriggerEnter(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.name == "主角")//记得把 主角攻击范围 层改回默认层 或者其它层 不要主角层 不然怪物碰撞不了
        {
            怪物.打中 = true;
        }

    }

    private void OnTriggerExit(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.name == "主角")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            怪物.打中 = false;
        }
    }


}
