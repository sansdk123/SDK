using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 医生门动画 : MonoBehaviour
{
    private 临时数据 临时数据;//脚本名+ 自取名 shuju1  （private私人的不能看）
    private 主角 主角;//脚本名+ 自取名 shuju1  （private私人的不能看）
    Animator anim;//调用外部动画组件
    Animator 按钮anim;//调用外部动画组件
    public bool 摸着;//是否摸着判定
    public int 门状态;//0开门 2关门

    void Start()
    {
        临时数据 = GameObject.Find("临时数据").GetComponent<临时数据>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        主角 = GameObject.Find("主角").GetComponent<主角>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        anim = GetComponent<Animator>();//获取动画  动画组件
        按钮anim = GameObject.Find("门按钮动画").GetComponent<Animator>();//获取动画  动画组件
        门状态 = 99;
    }

    private void OnTriggerEnter(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.tag == "主角")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            //主角.禁止攻击 = true;//
            摸着 = true;
            //Debug.Log("碰撞体名称：" + collision.name);//摸到的物体名字
        }

    }

    private void OnTriggerExit(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.tag == "主角")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            //主角.禁止攻击 = false;//
            摸着 = false;
        }
    }




    void Update()
    {
        //////////////////////  动画判定  ///////////////////////
        按钮anim.SetBool("摸着", 摸着);//跑步动画

        if (临时数据.攻击1键 && 摸着 && 门状态 == 0)
        {
            anim.SetTrigger("开门");//跳跃动画直接播放   (跳跃Y速度在 主角动画 脚本里)
            门状态 = 1;//
        }
        if (临时数据.攻击1键 && 摸着 && 门状态 == 2)
        {
            anim.SetTrigger("关门");//跳跃动画直接播放   (跳跃Y速度在 主角动画 脚本里)
            门状态 = 3;//
        }
    }

    private void 开门()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        门状态 = 2;//
    }

    private void 关门()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        门状态 = 0;//
    }

}
