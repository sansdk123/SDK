using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 主角动画 : MonoBehaviour
{
    Rigidbody rb;//调用外部重力组件
    private 主角 主角;//脚本名+ 自取名 shuju1  （private私人的不能看）
    public GameObject 跑步烟雾预制体;//需要实例化的物体或预制体
    public GameObject 冲刺烟雾预制体;//
    public GameObject 起跳烟雾预制体;//
    public GameObject 落地烟雾预制体;//
    GameObject 烟雾生成位置;//

    void Start()
    {
        主角 = GameObject.Find("主角").GetComponent<主角>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        rb = GameObject.Find("主角").GetComponent<Rigidbody>();
        烟雾生成位置 = GameObject.Find("烟雾生成位置");
    }



    ///////////////////////////////主角图片一直朝向屏幕///////////////////////////////////
    void Update()
    {

    }



    private void 受击时间还原()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        Time.timeScale = 1;//时间控制 还原
        GameObject.Find("怪物").GetComponent<怪物>().打中 = false;
        GetComponent<Renderer>().material = GameObject.Find("怪物").GetComponent<怪物>().原始材质球;
        GameObject.Find("怪物").GetComponent<怪物>().打中阶段 = 2;//0没受击中 1受击卡帧闪白中 2受击中
    }
    private void 受击结束()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        Time.timeScale = 1;//时间控制 还原
        主角.被打中 = false;
        主角.弹反 = false;
        GetComponent<Renderer>().material = GameObject.Find("怪物").GetComponent<怪物>().原始材质球;
        GameObject.Find("怪物").GetComponent<怪物>().打中 = false;
        GameObject.Find("怪物").GetComponent<怪物>().打中阶段 = 0;//0没受击中 1受击卡帧闪白中 2受击中
    }




    ///////////////////////////////动画调用参数///////////////////////////////////
    private void jump空中真()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        //////////////////////  跳跃X惯性
        if (!主角.jump空中)//按下跳跃和在地面上
        {
            if (!主角.run)//
            {
                主角.空中惯性speed = 0f;//惯性X速度
                主角.立定跳 = true;//立定跳不能空中移动
            }
            if (主角.run)//
                主角.空中惯性speed = 主角.空中惯性速度;//惯性X速度
        }
        //////////////////////  跳跃Y速度
        rb.velocity = new Vector3(rb.velocity.x, 主角.跳跃速度, rb.velocity.z);//跳跃Y速度

        主角.jump空中 = true;
    }


    private void jump空中假()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        主角.jump空中 = false;
        主角.立定跳 = false;//立定跳
    }


    private void 攻击惯性()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        rb.velocity = new Vector3(主角.正负攻击惯性speed, rb.velocity.y, rb.velocity.z);//2D移动速度
    }


    private void 连招结束()//
    {
        主角.连招数 = 0;//连招次数
    }


    private void 跑步烟雾()//
    {
        GameObject 预制体 = Instantiate(跑步烟雾预制体, 烟雾生成位置.transform.position, 烟雾生成位置.transform.rotation);
        if (主角.朝向 == 0)//翻转左
            预制体.transform.localScale = new Vector3(-3.7f, 3.7f, 3.7f);//人物翻转向左
        if (主角.朝向 == 1)//翻转右
            预制体.transform.localScale = new Vector3(3.7f, 3.7f, 3.7f);//人物翻转向右
    }
    private void 冲刺烟雾()//
    {
        if (主角.地面)//主角在地面
        {
            GameObject 预制体 = Instantiate(冲刺烟雾预制体, 烟雾生成位置.transform.position, 烟雾生成位置.transform.rotation);
            if (主角.朝向 == 0)//翻转左
                预制体.transform.localScale = new Vector3(-3.7f, 3.7f, 3.7f);//人物翻转向左
            if (主角.朝向 == 1)//翻转右
                预制体.transform.localScale = new Vector3(3.7f, 3.7f, 3.7f);//人物翻转向右
        }
    }
    private void 起跳烟雾()//
    {
        GameObject 预制体 = Instantiate(起跳烟雾预制体, 烟雾生成位置.transform.position, 烟雾生成位置.transform.rotation);
        if (主角.朝向 == 0)//翻转左
            预制体.transform.localScale = new Vector3(-3.7f, 3.7f, 3.7f);//人物翻转向左
        if (主角.朝向 == 1)//翻转右
            预制体.transform.localScale = new Vector3(3.7f, 3.7f, 3.7f);//人物翻转向右
    }
    private void 落地烟雾()//
    {
        GameObject 预制体 = Instantiate(落地烟雾预制体, 烟雾生成位置.transform.position, 烟雾生成位置.transform.rotation);
        if (主角.朝向 == 0)//翻转左
            预制体.transform.localScale = new Vector3(-3.7f, 3.7f, 3.7f);//人物翻转向左
        if (主角.朝向 == 1)//翻转右
            预制体.transform.localScale = new Vector3(3.7f, 3.7f, 3.7f);//人物翻转向右
    }
}
