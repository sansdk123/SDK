using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 跟随主角 : MonoBehaviour
{
    ///////////////////////////// （注意！游戏刚开始运行 如果主角走出 镜头停止跟随范围 镜头抽搐 只要复制镜头到 镜头停止跟随范围 的边缘不会抽搐的数值到 镜头位置1 就可以了） /////////////////////////////
    private 主角 主角;//脚本名+ 自取名 shuju  （private私人的不能看）
    Vector3 XYZ;//主角朝向跟随主角数值
    void Start()
    {
        主角 = GameObject.Find("主角").GetComponent<主角>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        XYZ = GameObject.Find("镜头位置1").transform.position;//初始位置（是在读取主角位置1时候 读取这镜头位置1的数值）
    }

    void Update()
    {
        if (!主角.镜头停止跟随)//没有摸到镜头停止跟随碰撞盒
        {
            XYZ.x = 主角.transform.position.x;
        }
        XYZ.y = 主角.transform.position.y;
        XYZ.z = 主角.transform.position.z;

        transform.position = XYZ;
    }
}
