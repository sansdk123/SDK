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

    //主角属性
    public float HP = 100f;
    public float maxHP = 100f;
    public float 充能值 = 100f;
    public float max充能值 = 100f;
    public float 耐力值 = 100f;
    public float max耐力值 = 100f;
    public float 韧性值 = 100f;
    public float max韧性值 = 100f;
    public float 攻击力 = 0f;//根据出的招数和武器装备和人物属性决定每次攻击的攻击力
    public float 防御力 = 100f;

    public float 攻击惯性速度 = 3f;//攻击惯性移动速度
    public float 移动速度 = 8f;
    public float 跳跃速度 = 13f;//跳跃速度
    public float 空中惯性速度 = 6f;//空中惯性移动速度
    public float 瞬移速度 = 25f;
    public float 瞬移时长 = 0.3f;//瞬移时长
    public float 连招间隔时间 = 0.5f;//连招按键间隔时间
    public float 攻击动作时间 = 0.3f;//每一招攻击需要的时间

    public float 轻攻击1攻击力 = 5f;
    public float 轻攻击2攻击力 = 7f;


    //动画判定
    public bool run;//是否跑步判定
    public bool 瞬移;//是否翻滚状态
    public bool jump空中;//是否在空中判定
    public bool 地面;//是否在地面判定
    public bool 防御;//是否防御状态
    public bool 下落攻击;//是否在下落攻击判定
    public bool 死亡;//是否死亡
    public bool 弹反;//是否弹反（代码在怪物和主角动画里）
    public bool 立定跳;//是否立定跳（代码在主角动画里）
    public bool 被打中;//是否被打中（代码在怪物里判定）

    public bool 镜头停止跟随;//是否镜头停止跟随
    public bool 禁止改变方向;//禁止改变方向
    public bool 禁止移动;//禁止改变方向
    public bool 禁止攻击;//禁止改变方向


    //各种参数
    public float 正负speed = 0f;//移动速度
    public float speed = 0f;//移动速度
    public float 正负空中惯性speed = 0f;//惯性移动速度
    public float 空中惯性speed = 0f;//惯性移动速度
    public float 正负攻击惯性speed = 0f;//移动速度
    public float fanGuntimer = 0f;//翻滚时间
    public float velocityY = 0f;//重力Y数值
    public int 朝向 = 1;//0朝向左 1朝向右(主角动画 主角朝向 怪物 有代码)
    //攻击参数
    public float 连招time = 0f;//连招间隔时间显示
    public int 连招数 = 0;//连招次数 1是招数1 2是招数2 （怪物脚本 调用来判定震动大小）

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
        fanGuntimer = 瞬移时长;
    }

    void Update()
    {
        //////////////////////  动画判定  ///////////////////////
        anim.SetBool("run", run);//跑步动画
        anim.SetBool("瞬移", 瞬移);//瞬移动画
        anim.SetBool("jump空中", jump空中);//jump空中动画
        anim.SetBool("地面", 地面);//地面动画
        anim.SetBool("防御", 防御);//防御动画
        anim.SetBool("下落攻击", 下落攻击);//下落攻击动画
        anim.SetBool("死亡", 死亡);//死亡动画
        anim.SetFloat("velocity Y", rb.velocity.y);//
        地面 = 地面检测.摸着;//是否在地面判定
        velocityY = rb.velocity.y;//重力Y数值

        //////////////////////  禁止判定  ///////////////////////
        if (!jump空中 && !瞬移 && !防御 && 地面)
            禁止改变方向 = false;
        else
            禁止改变方向 = true;

        if (防御 || 立定跳)
            禁止移动 = true;
        else
            禁止移动 = false;


        if (HP <= 0f)//死亡判定
        {
            run = false;
            死亡 = true;
        }


        ///////////////////// 摩檫力 和 Rigidbody组件控制//////////////////////
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //Rigidbody组件的 constraints 只打开  FreezePositionZ  和  FreezeRotationXYZ
        if (临时数据.左右键 != 0 || jump空中 || 瞬移 || 连招time > 0f || 被打中)//按下左右 或 在空中 或 连招time 或 被打中
            Box.sharedMaterial = zero;//摩檫力ZERO
        if ((临时数据.左右键 == 0 && !jump空中 && !瞬移 && 连招time <= 0f && !被打中) || (防御 && 临时数据.左右键 == 0))//不按左右 和 不在空中 和 连招time
            Box.sharedMaterial = max;//摩檫力MAX
        //主角.GetComponent<BoxCollider>().sharedMaterial = 主角.max;//摩檫力MAX（外部调用摩檫力写法）


        if (!死亡)//没有死亡
        {
            攻击代码();
            移动代码();
        }
    }


    private void 攻击代码()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        /////////////////////防御代码//////////////////////
        if (临时数据.防御键)//如果按着防御键
            防御 = true;
        if (!临时数据.防御键)//如果按着防御键
            防御 = false;


        /////////////////////跳跃攻击//////////////////////
        if (临时数据.攻击1键 && !地面)//
            下落攻击 = true;
        if (地面 && !jump空中)//
            下落攻击 = false;


        /////////////////////攻击代码//////////////////////
        if (连招time >= 0f)//连招time
            连招time -= Time.deltaTime;//timerA计时器时间
        if (连招time > 攻击动作时间)//
        {
            speed = 0f;//移动速度
            run = false;
        }
        if (连招time < 0f)//
            连招数 = 0;//
        if (临时数据.攻击1键 && !禁止攻击 && !禁止改变方向)
        {
            if (连招time < 连招间隔时间 && 连招数 == 0)//连招1
            {
                ///// 攻击力计算 /////
                攻击力 = 轻攻击1攻击力;

                anim.SetTrigger("攻击1");
                连招time = 连招间隔时间 + 攻击动作时间;//连招总时间
                连招数++;//
            }
            if (连招time < 连招间隔时间 && 连招数 == 1)//连招2
            {
                ///// 攻击力计算 /////
                攻击力 = 轻攻击2攻击力;

                anim.SetTrigger("攻击2");
                连招time = 连招间隔时间 + 攻击动作时间;//连招总时间
                连招数 = 0;//
            }
        }
    }



    private void 移动代码()//
    {

        ///////////////////// 朝向判定  //////////////////////
        if (临时数据.左右键 < 0 && !禁止改变方向 && 连招time <= 连招间隔时间)//翻转左
            朝向 = 0;//0朝向左
        if (临时数据.左右键 > 0 && !禁止改变方向 && 连招time <= 连招间隔时间)//翻转右
            朝向 = 1;//1朝向右

        if (朝向 == 0)//翻转左
        {
            正负speed = -speed;//左-右+
            正负空中惯性speed = -空中惯性speed;//左-右+
            正负攻击惯性speed = -攻击惯性速度;//左-右+
            anim.transform.localScale = new Vector3(-3.7f, 3.7f, 3.7f);//人物翻转向左
        }
        if (朝向 == 1)//翻转右
        {
            正负speed = speed;//左-右+
            正负空中惯性speed = 空中惯性speed;//左-右+
            正负攻击惯性speed = 攻击惯性速度;//左-右+
            anim.transform.localScale = new Vector3(3.7f, 3.7f, 3.7f);//人物翻转向右
        }


        /////////////////////移动代码//////////////////////
        if (临时数据.左右键 == 0)//如果不按左右键
        {
            run = false;
            if (!瞬移)//
                speed = 0f;//移动速度减少
        }
        if (临时数据.左右键 != 0 && !禁止移动 && 连招time <= 连招间隔时间)//如果按左右键 和（连招time 0.2秒时差快速切换跑步动画）
        {
            run = true;
        }

        if (连招time <= 连招间隔时间 && !禁止移动 && !被打中)//2D移动模式
            rb.velocity = new Vector3(正负speed, rb.velocity.y, rb.velocity.z);//2D移动速度

        if (run == true && !瞬移 && 连招time <= 连招间隔时间)//
        {
            if ((临时数据.左右键 < 0 && 朝向 == 0) || (临时数据.左右键 > 0 && 朝向 == 1))//按左和朝向左 或 按右和朝向右
                speed = 移动速度;//移动速度
        }
        if (瞬移 == true)//
            speed = 瞬移速度;//瞬移速度


        //////////////////////  跳跃代码  ///////////////////////
        if (临时数据.跳跃键 && !禁止改变方向 && 连招time <= 连招间隔时间)//按下跳跃和在地面上 和（连招time 0.2秒时差快速切换跳跃动画）
        {
            anim.SetTrigger("jump");//跳跃动画直接播放   (跳跃Y速度在 主角动画 脚本里)
        }


        /////////////////////惯性代码//////////////////////
        if (空中惯性speed > speed && jump空中)//如果不按左右上下键 或 惯性速度大于移动速度 和 在空中
            rb.velocity = new Vector3(正负空中惯性speed, rb.velocity.y, rb.velocity.z);//2D惯性X速度  (惯性speed在 主角动画 脚本里)



        /////////////////////翻滚代码//////////////////////
        if (临时数据.瞬移键 && !禁止改变方向 && fanGuntimer >= 瞬移时长)//翻滚条件
        {//按左或右 和 按手柄X键或者按键盘U键 和 翻滚时间>= 0.3f
            anim.SetTrigger("冲刺");
            瞬移 = true;//翻滚状态等于真
        }
        if (fanGuntimer <= 0f)//如果翻滚时间<=0秒
        {
            fanGuntimer = 瞬移时长;//翻滚时间=0.3秒
            瞬移 = false;//翻滚=假
        }
        if (瞬移)
        {
            残影管理.instance.GetFormPool();//加载外部的 残影管理 文件(残影代码里写着 Tag标签是Player 的物体才会加载残影)
            fanGuntimer -= Time.deltaTime;//翻滚时间 - 计时器时间
        }
    }

}

