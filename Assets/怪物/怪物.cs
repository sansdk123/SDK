using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物 : MonoBehaviour
{
    private 临时数据 临时数据;//脚本名+ 自取名 shuju1  （private私人的不能看）
    private 主角 主角;//脚本名+ 自取名 shuju1  （private私人的不能看）
    Animator anim;//调用外部动画组件
    Rigidbody rb;//调用外部重力组件
    Transform 怪物左右;//翻转用

    //BOSS属性
    public float HP = 100f;
    public float maxHP = 100f;
    public float 攻击力 = 0f;//根据出的招数和武器装备和人物属性决定每次攻击的攻击力

    public float speed = 5f;//移动速度
    public float 受击惯性速度 = 5f;//移动速度
    public float 怪物卡帧时长 = 0.09f;//受击时候卡顿和变白色
    public float 主角卡帧时长 = 0.09f;//打中时候卡顿和变白色
    public float 招数0攻击力 = 10f;//0重攻击 1中跳击 2中冲刺 3远投掷 4远激光
    public float 招数1攻击力 = 15f;
    public float 招数2攻击力 = 8f;
    public float 招数3攻击力 = 7f;
    public float 招数4攻击力 = 5f;

    //动画判定
    public bool run;//是否跑步判定
    public bool 死亡;//是否死亡

    public float 两者距离;//
    public float 两者正负距离;//

    public int 朝向 = 0;//0朝向左 1朝向右
    public int 攻击招数 = 0;//1中跳击 2中冲刺 3远投掷 4远激光
    public float 站立time = 0f;//怪物随机休息时间 (代码在怪物动画里)
    public float 休息time = 0f;//怪物随机休息时间 (代码在怪物动画里)
    public bool 攻击中;//判定是否在攻击


    public bool 被打中;//是否被打中判定
    public int 受击阶段 = 0;//0没受击中 1受击卡帧闪白中 2受击中

    public bool 打中;//是否打中判定 (代码在怪物攻击范围里)
    public int 打中阶段 = 0;//0没受击中 1受击卡帧闪白中 2受击中


    public Material 原始材质球;//原始材质
    public Material 变白材质球; //把变白材质球拉到这里
    public GameObject 打击效果预制体;//
    public GameObject 血迹预制体;//
    public GameObject 弹反特效预制体;//

    void Start()
    {
        临时数据 = GameObject.Find("临时数据").GetComponent<临时数据>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        主角 = GameObject.Find("主角").GetComponent<主角>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        anim = transform.Find("怪物左右/怪物动画").GetComponent<Animator>();//获取子级的主角动画  动画组件
        怪物左右 = transform.Find("怪物左右");
        rb = GetComponent<Rigidbody>();
        原始材质球 = anim.GetComponent<Renderer>().material;//获取怪物动画材质球
    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////  动画判定  ///////////////////////
        anim.SetBool("run", run);//跑步动画
        anim.SetBool("死亡", 死亡);//地面动画

        if (HP <= 0f && !死亡)//死亡判定
        {
            GameObject 预制体 = Instantiate(血迹预制体, transform.Find("怪物左右/血迹预制体生成位置").position, transform.Find("怪物左右/血迹预制体生成位置").rotation);//生成血迹预制体
            if (朝向 == 0)//0朝向左
                预制体.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);//人物翻转向左（因为怪物图片是向左的 所以和主角方向相反设置）
            if (朝向 == 1)//0朝向右
                预制体.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);//人物翻转向左（因为怪物图片是向左的 所以和主角方向相反设置）

            run = false;
            死亡 = true;
        }

        if (!死亡)//如果主角没有瞬移
        {
            ///////////////////// 怪物停止任何攻击  //////////////////////
            if (休息time >= 0f)//
                休息time -= Time.deltaTime;//timerA计时器时间
            if (站立time >= 0f)//
                站立time -= Time.deltaTime;//timerA计时器时间

            if (受击阶段 == 0)//0没受击中
            {
                移动判定();
                攻击判定();
            }
            受击判定();
            if (!主角.瞬移)//如果主角没有瞬移
                打击判定();
        }

    }



    private void 移动判定()
    {
        ///////////////////// 移动动画判定  //////////////////////
        if (两者距离 > 1.8f && 攻击中 == false && 站立time <= 0f)//移动判定
        {
            run = true;
        }
        if (两者距离 <= 1.8f || 攻击中 == true)//停止移动
        {
            run = false;
        }


        ///////////////////// 距离判定  //////////////////////
        两者距离 = Mathf.Abs(主角.transform.position.x - transform.position.x);
        两者正负距离 = transform.position.x - 主角.transform.position.x;//两者之间距离


        ///////////////////// 人物朝向 //////////////////////
        if (两者正负距离 >= 0.3f && 攻击中 == false && 站立time <= 0f)//朝向左
        {
            朝向 = 0;//0朝向左
            怪物左右.localScale = new Vector3(1f, 1f, 1f);//人物翻转向左（因为怪物图片是向左的 所以和主角方向相反设置）
        }
        if (两者正负距离 <= -0.3f && 攻击中 == false && 站立time <= 0f)//朝向右
        {
            朝向 = 1;//1朝向右
            怪物左右.localScale = new Vector3(-1f, 1f, 1f);//人物翻转向右
        }



        ///////////////////// 移动判定  //////////////////////
        if (两者正负距离 > 1.8f && 攻击中 == false && 站立time <= 0f)//朝向左
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);//往左走
        }
        if (两者正负距离 < -1.8f && 攻击中 == false && 站立time <= 0f)//朝向右
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);//往右走
        }
    }



    private void 攻击判定()
    {
        ///////////////////// 近攻击判定  //////////////////////
        if (两者距离 <= 1.8f && 攻击中 == false && 休息time <= 0f && 站立time <= 0f)//
        {
            ///// 攻击力计算 /////
            攻击力 = 招数0攻击力;
            攻击招数 = 0;
            anim.SetTrigger("近重击");//动画直接播放
            攻击中 = true;
        }
        ///////////////////// 中远攻击判定  //////////////////////
        if (两者距离 >= 3.5f && 攻击中 == false && 休息time <= 0f && 站立time <= 0f)//1中跳击 2中冲刺 3远投掷 4远激光
        {
            攻击招数 = Random.Range(1, 5);//随机数1到4
            if (攻击招数 == 1)//
            {
                ///// 攻击力计算 /////
                攻击力 = 招数1攻击力;
                anim.SetTrigger("中跳击");//中跳击
            }

            if (攻击招数 == 2)//
            {
                ///// 攻击力计算 /////
                攻击力 = 招数2攻击力;
                anim.SetTrigger("中冲刺");//中冲刺
            }

            if (攻击招数 == 3)//
            {
                ///// 攻击力计算 /////
                攻击力 = 招数3攻击力;
                anim.SetTrigger("远投掷");//远投掷
            }

            if (攻击招数 == 4)//
            {
                ///// 攻击力计算 /////
                攻击力 = 招数4攻击力;
                anim.SetTrigger("远激光");//远激光
            }

            攻击中 = true;
        }
    }



    private void 受击判定()
    {
        if (被打中 && 受击阶段 == 0)//0没受击中 1受击卡帧闪白中 2受击中
        {
            anim.GetComponent<Renderer>().material = 变白材质球;
            Time.timeScale = 怪物卡帧时长;//时间控制 减速
            anim.SetTrigger("时间减速");//
            Instantiate(打击效果预制体, transform.Find("打击效果预制体生成位置").position, transform.Find("打击效果预制体生成位置").rotation);//生成打击效果预制体
            受击阶段 = 1;
            被打中 = false;
        }
        if (受击阶段 == 2)//
        {
            anim.GetComponent<Renderer>().material = 原始材质球;
            anim.SetTrigger("受伤");//
            HP -= 主角.攻击力;
            if (主角.连招数 == 1)//主角正在招数1
            {
                临时数据.镜头手柄震动 = 1;//0不震动 1震动 2震动中 3震动大 99不能操作
            }
            if (主角.连招数 == 0)//主角正在招数2
            {
                临时数据.镜头手柄震动 = 3;//0不震动 1震动 2震动中 3震动大 99不能操作
            }
            //////////////////////  受击惯性  //////////////////////
            if (两者正负距离 < 0f)//0朝向左
            {
                怪物左右.localScale = new Vector3(-1f, 1f, 1f);//人物翻转向右
                rb.velocity = new Vector3(-受击惯性速度, rb.velocity.y, rb.velocity.z);//往左走
            }
            if (两者正负距离 > 0f)//1朝向右
            {
                怪物左右.localScale = new Vector3(1f, 1f, 1f);//人物翻转向左（因为怪物图片是向左的 所以和主角方向相反设置）
                rb.velocity = new Vector3(受击惯性速度, rb.velocity.y, rb.velocity.z);//往右走
            }


            受击阶段 = 99;
        }
    }




    private void 打击判定()
    {
        if (打中 && 打中阶段 == 0)//0没受击中 1受击卡帧闪白中 2受击中
        {
            GameObject.Find("主角动画").GetComponent<Renderer>().material = 变白材质球;
            Time.timeScale = 主角卡帧时长;//时间控制 减速
            GameObject.Find("主角动画").GetComponent<Animator>().SetTrigger("时间减速");//
            主角.被打中 = true;
            //Instantiate(打击效果预制体, GameObject.Find("主角打击效果预制体生成位置").transform.position, GameObject.Find("主角打击效果预制体生成位置").transform.rotation);//生成打击效果预制体
            打中阶段 = 1;
            打中 = false;
        }

        if (打中阶段 == 1)//弹反判定
        {
            if (临时数据.攻击1键)//主角正在招数1
            {
                GameObject.Find("主角动画").GetComponent<Animator>().SetTrigger("弹反");//
                Instantiate(弹反特效预制体, GameObject.Find("弹反特效预制体生成位置").transform.position, GameObject.Find("弹反特效预制体生成位置").transform.rotation);//生成打击效果预制体
                主角.弹反 = true;
            }
        }

        if (打中阶段 == 2)//
        {
            GameObject.Find("主角动画").GetComponent<Renderer>().material = 原始材质球;
            if (!主角.防御 && !主角.死亡 && !主角.弹反)//如果主角没有防御 和 死亡
            {
                GameObject.Find("主角动画").GetComponent<Animator>().SetTrigger("受伤");//
                Instantiate(打击效果预制体, GameObject.Find("主角打击效果预制体生成位置").transform.position, GameObject.Find("主角打击效果预制体生成位置").transform.rotation);//生成打击效果预制体
            }
            if (主角.防御)//如果主角没有防御 和 死亡
                主角.HP -= 攻击力 * 0.5f;
            if (!主角.防御 && !主角.弹反)//如果主角没有防御 和 死亡
                主角.HP -= 攻击力;
            if (攻击招数 <= 2)//主角正在招数1
            {
                临时数据.镜头手柄震动 = 3;//0不震动 1震动 2震动中 3震动大 99不能操作
            }
            if (攻击招数 > 2)//主角正在招数2
            {
                临时数据.镜头手柄震动 = 1;//0不震动 1震动 2震动中 3震动大 99不能操作
            }
            //////////////////////  冲刺速度  //////////////////////
            if (两者正负距离 < 0f)//0朝向左
                主角.朝向 = 0;//主角朝向左
            if (两者正负距离 > 0f)//1朝向右
                主角.朝向 = 1;//主角朝向右
            主角.GetComponent<Rigidbody>().velocity = new Vector3(-主角.正负攻击惯性speed, rb.velocity.y, rb.velocity.z);//2D移动速度

            打中阶段 = 99;
        }
    }


    private void OnTriggerEnter(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.tag == "主角攻击范围")//记得把 主角攻击范围 层改回默认层 或者其它层 不要主角层 不然怪物碰撞不了
        {
            被打中 = true;
        }

    }

    private void OnTriggerExit(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.tag == "主角攻击范围")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            被打中 = false;
        }
    }

}
