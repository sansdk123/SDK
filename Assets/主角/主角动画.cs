using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class 主角动画 : MonoBehaviour
{
    Camera 镜头;//镜头
    private 主角 主角;//脚本名+ 自取名 shuju1  （private私人的不能看）
    Rigidbody rb;//调用外部重力组件

    void Start()
    {
        主角 = GameObject.Find("主角").GetComponent<主角>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        rb = GameObject.Find("主角").GetComponent<Rigidbody>();
        镜头 = Camera.main;//获取镜头
    }



    ///////////////////////////////主角朝向屏幕///////////////////////////////////
    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 镜头.farClipPlane / 2);//计算屏幕中心点的世界坐标
        Vector3 worldCenter = 镜头.ScreenToWorldPoint(screenCenter);
        Vector3 directionToCenter = worldCenter - transform.position;//计算物体到屏幕中心点的向量
        Quaternion lookRotation = Quaternion.LookRotation(directionToCenter, Vector3.up);//确保物体朝向屏幕中心
        //transform.rotation = lookRotation;//物体全部旋转XYZ一直朝向屏幕
        transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);//只有旋转Y一直朝向屏幕
    }



    ///////////////////////////////动画调用参数///////////////////////////////////
    private void jump空中真()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        //////////////////////  跳跃X惯性
        if (!主角.jump空中)//按下跳跃和在地面上
        {
            if (!主角.walk && !主角.run)//
                主角.惯性speed = 0f;//惯性X速度
            if (主角.walk)//
                主角.惯性speed = 3.5f;//惯性X速度
            if (主角.run)//
                主角.惯性speed = 7f;//惯性X速度
        }
        //////////////////////  跳跃Y速度
        if (主角.run)//
            rb.velocity = new Vector3(rb.velocity.x, 主角.jumpSpeed * 1.2f, rb.velocity.z);//跳跃Y速度
        if (!主角.run)//
            rb.velocity = new Vector3(rb.velocity.x, 主角.jumpSpeed, rb.velocity.z);//跳跃Y速度

        主角.jump空中 = true;
    }
    private void jump空中假()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        主角.jump空中 = false;
    }

}
