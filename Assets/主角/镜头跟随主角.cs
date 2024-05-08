using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 镜头跟随主角 : MonoBehaviour
{
    ///////////////////////////// （注意！游戏刚开始运行 如果主角走出 镜头停止跟随范围 镜头抽搐 只要调节镜头到 镜头停止跟随范围 的边缘就不会抽搐） /////////////////////////////
    private 主角 主角;//脚本名+ 自取名 shuju  （private私人的不能看）
    Transform 主角朝向;//主角朝向
    Vector3 主角朝向位置;//主角朝向跟随主角数值
    float x;//鼠标X轴 控制 旋转Y数值
    float y;//鼠标Y轴 控制 旋转X数值
    public float 俯视角度 = -50f;//俯视角度 数值越小可看越多
    public float 仰视角度 = 23f;//仰视角度 数值越大可看越多

    //镜头XY轴3D锁定
    public bool 镜头X轴3D锁定 = true;
    public bool 镜头Y轴3D锁定 = true;

    //镜头位置
    public Vector3 镜头位置 = new Vector3(0, 1.43f, -6.66f);//手动调节镜头跟随主角的X Y Z数值
    public float 延时跟随速度 = 5f;//手动调节相机延时速度
    public float 鼠标Xspeed = 1f;//手动调节鼠标X速度
    public float 鼠标Yspeed = 1f;//手动调节鼠标Y速度

    void Start()
    {
        主角 = GameObject.Find("主角").GetComponent<主角>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        主角朝向 = GameObject.Find("主角朝向").transform;
        主角朝向位置 = transform.position;//镜头初始位置
    }

    void Update()
    {
        ////////////////镜头跟随主角朝向 旋转 位置////////////////////////////////////////////////////////////////////////////////////////////////
        if (!镜头X轴3D锁定)//没有锁定X轴3D
            x += Input.GetAxis("Mouse X") * 鼠标Xspeed;//注意有个+号
        if (!镜头Y轴3D锁定)//没有锁定Y轴3D
            y -= Input.GetAxis("Mouse Y") * 鼠标Yspeed;//注意有个-号

        y = Mathf.Clamp(y, 俯视角度, 仰视角度);//y值的 活动范围
        Quaternion rotation = Quaternion.Euler(y, x, 0);//鼠标控制摄像机的旋转
        Vector3 position = rotation * new Vector3(镜头位置.x, 镜头位置.y, 镜头位置.z) + 主角朝向.position;//鼠标控制摄像机的位置

        //设置摄像机的位置与旋转
        transform.rotation = rotation;//镜头旋转位置
        transform.position = position;//镜头位置



        ////////////////主角朝向延时跟随主角 位置////////////////////////////////////////////////////////////////////////////////////////////////
        if (!主角.镜头停止跟随)//没有摸到镜头停止跟随碰撞盒
        {
            主角朝向位置.x = 主角.transform.position.x;
        }
        主角朝向位置.y = 主角.transform.position.y;
        主角朝向位置.z = 主角.transform.position.z;

        //给摄像机移动到应该在的位置的过程中加上延时效果
        主角朝向.transform.position = Vector3.Lerp(主角朝向.transform.position, 主角朝向位置, 延时跟随速度 * Time.deltaTime);
    }

}
