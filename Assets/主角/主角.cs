using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 主角 : MonoBehaviour
{
    private 临时数据 临时数据;//脚本名+ 自取名 shuju1  （private私人的不能看）
    private 地面检测 地面检测;//脚本名+ 自取名 shuju  （private私人的不能看）
    Animator anim;//调用外部动画组件
    Rigidbody rb;//调用外部重力组件
    BoxCollider Box;//调用外部CapsuleCollider碰撞盒组件

    public bool 移动模式3D;//2D移动还是3D移动
    public bool 镜头停止跟随;//是否镜头停止跟随

    //动画判定
    public bool walk;//是否走路判定
    public bool run;//是否跑步判定
    public bool isfanGun;//是否翻滚状态
    public bool jump空中;//是否在空中判定
    public bool 地面;//是否在地面判定

    //各种参数
    public float speed = 0f;//移动速度
    public float 惯性speed = 0f;//惯性移动速度
    public float jumpSpeed = 6.0f;//跳跃速度
    public float fanGuntimer = 0.3f;//翻滚时间
    public float velocityY = 0f;//重力Y数值

    public PhysicMaterial max;//摩擦满
    public PhysicMaterial zero;//摩擦零

    private void OnTriggerStay(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.tag == "镜头停止跟随")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            镜头停止跟随 = true;
        }
    }

    private void OnTriggerExit(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.tag == "镜头停止跟随")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            镜头停止跟随 = false;
        }
    }

    void Start()
    {
        临时数据 = GameObject.Find("临时数据").GetComponent<临时数据>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        地面检测 = GameObject.Find("地面检测").GetComponent<地面检测>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        anim = transform.Find("主角动画").GetComponent<Animator>();//获取子级的主角动画  动画组件
        rb = GetComponent<Rigidbody>();
        Box = GetComponent<BoxCollider>();
    }

    void Update()
    {
        //////////////////////  动画判定  ///////////////////////
        anim.SetBool("walk", walk);//走路动画
        anim.SetBool("run", run);//跑步动画
        anim.SetBool("jump空中", jump空中);//jump空中动画
        anim.SetBool("地面", 地面);//地面动画
        anim.SetFloat("velocity Y", rb.velocity.y);//
        地面 = 地面检测.摸着;//是否在地面判定
        velocityY = rb.velocity.y;//重力Y数值


        if (!移动模式3D)//2D移动模式
        {
            if (临时数据.左右键 == 0)//如果不按左右键
            {
                walk = false;
                run = false;
                speed = 0f;//移动速度减少
            }
            if (临时数据.左右键 != 0)//如果按左右键
            {
                if (!临时数据.奔跑键)//如果按奔跑键
                {
                    walk = true;
                    run = false;
                }
                if (临时数据.奔跑键)//如果按奔跑键
                {
                    run = true;
                    walk = false;
                }
            }
        }
        if (移动模式3D)//3D移动模式
        {
            if (临时数据.左右键 == 0 && 临时数据.上下键 == 0)//如果不按左右上下键
            {
                walk = false;
                run = false;
                speed = 0f;//移动速度减少
            }
            if (临时数据.左右键 != 0 || 临时数据.上下键 != 0)//如果按左右上下键
            {
                if (!临时数据.奔跑键)//如果按奔跑键
                {
                    walk = true;
                    run = false;
                }
                if (临时数据.奔跑键)//如果按奔跑键
                {
                    run = true;
                    walk = false;
                }
            }
        }



        ///////////////////// 摩檫力 和 Rigidbody组件控制//////////////////////
        if (!移动模式3D)//2D移动模式
        {
            //Rigidbody组件的 constraints 只打开  FreezePositionZ  和  FreezeRotationXYZ
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            if (临时数据.左右键 != 0 || jump空中)//按下左右 和 在空中
                Box.sharedMaterial = zero;//摩檫力ZERO
            if (临时数据.左右键 == 0 && !jump空中)//不按左右 和 不在空中
                Box.sharedMaterial = max;//摩檫力MAX
        }
        if (移动模式3D)//3D移动模式
        {
            //Rigidbody组件的 constraints 只打开  FreezeRotationXYZ
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            if (临时数据.左右键 != 0 || 临时数据.上下键 != 0 || jump空中)//按下左右 或 上下 和 在空中
                Box.sharedMaterial = zero;//摩檫力ZERO
            if (临时数据.左右键 == 0 && 临时数据.上下键 == 0 && !jump空中)//不按左右上下 和 不在空中
                Box.sharedMaterial = max;//摩檫力MAX
        }



        //////////////////////  跳跃代码  ///////////////////////
        if (临时数据.跳跃键 && 地面)//按下跳跃和在地面上
        {
            anim.SetTrigger("jump");//跳跃动画直接播放   (跳跃Y速度在 主角动画 脚本里)
        }



        /////////////////////移动代码//////////////////////
        Vector3 input = (GameObject.Find("主角朝向").transform.forward).normalized;//人物朝向
        if (!移动模式3D)//2D移动模式
            rb.velocity = new Vector3(input.x * speed, rb.velocity.y, rb.velocity.z);//2D移动速度
        if (移动模式3D)//3D移动模式
            rb.velocity = new Vector3(input.x * speed, rb.velocity.y, input.z * speed * 0.8f);//3D移动速度


        if (临时数据.左右键 < 0)//翻转左
            anim.transform.localScale = new Vector3(-4.26f, 4.26f, 4.26f);//人物翻转向左
        if (临时数据.左右键 > 0)//翻转右
            anim.transform.localScale = new Vector3(4.26f, 4.26f, 4.26f);//人物翻转向右

        if (walk == true && !run && !isfanGun)//
            speed = 5f;//移动速度
        if (run == true && !isfanGun)//
            speed = 10f;//移动速度
        if (isfanGun == true)//
            speed = 15f;//移动速度



        /////////////////////惯性代码//////////////////////
        if (!移动模式3D)//2D移动模式
        {
            if ((临时数据.左右键 == 0 || 惯性speed > speed) && jump空中)//如果不按左右上下键 或 惯性速度大于移动速度 和 在空中
                rb.velocity = new Vector3(input.x * 惯性speed, rb.velocity.y, rb.velocity.z);//2D惯性X速度  (惯性speed在 主角动画 脚本里)
        }
        if (移动模式3D)//3D移动模式
        {
            if (((临时数据.左右键 == 0 && 临时数据.上下键 == 0) || 惯性speed > speed) && jump空中)//如果不按左右上下键 或 惯性速度大于移动速度 和 在空中
                rb.velocity = new Vector3(input.x * 惯性speed, rb.velocity.y, input.z * 惯性speed * 0.8f);//3D惯性X速度  (惯性speed在 主角动画 脚本里)
        }



        /////////////////////翻滚代码//////////////////////
        if (isfanGun)
        {
            残影管理.instance.GetFormPool();//加载外部的 残影管理 文件(残影代码里写着 Tag标签是Player 的物体才会加载残影)
            fanGuntimer -= Time.deltaTime;//翻滚时间 - 计时器时间
        }
        if ((临时数据.左右键 != 0 || 临时数据.上下键 != 0) && 临时数据.瞬移键 && !isfanGun && fanGuntimer >= 0.3f)//翻滚条件
        {//按左或右 和 按手柄X键或者按键盘U键 和 翻滚时间>= 0.3f
            isfanGun = true;//翻滚状态等于真
        }

        if (fanGuntimer <= 0f)//如果翻滚时间<=0秒
        {
            fanGuntimer = 0.3f;//翻滚时间=0.3秒
            isfanGun = false;//翻滚=假
        }

    }

}

