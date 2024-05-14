using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class 临时数据 : MonoBehaviour
{
    //////////////////////////按键数值/////////////////////////////
    private PlayerIndex playerIndex;
    private GamePadState state;
    public float 扳机键LT;//扳机键LT数值
    public float 扳机键RT;//扳机键RT数值

    public float 左右键;//（长按）手柄/键盘  -1f 1f  不按归0
    public float 上下键;//（长按）手柄/键盘  -1f 1f  不按归0
    public bool 跳跃键;//（短按）手柄/键盘/鼠标
    public bool 瞬移键;//（短按）手柄/键盘
    public bool 攻击1键;//（短按）手柄/键盘/鼠标
    public bool 防御键;//（长按）手柄/键盘


    //震动相关
    public int 镜头手柄震动 = 0;//0不震动 1震动 2震动中 3震动大 99不能操作
    public bool 手柄震动 = true;//手柄震动开关
    public bool 镜头震动 = true;//镜头震动开关
    public float 震动时长 = 0.1f;//控制震动时长
    public float 左马达震动幅度 = 1f;//左马达震动幅度
    public float 右马达震动幅度 = 1f;//右马达震动幅度
    public float 震动time = 0f;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);//挂着这脚本的物体就算切换场景也不会销毁
    }

    void Update()
    {
        //////////////////////////按键数值/////////////////////////////
        state = GamePad.GetState(playerIndex);
        扳机键LT = state.Triggers.Left;//扳机键LT数值
        扳机键RT = state.Triggers.Right;//扳机键RT数值

        左右键 = Input.GetAxis("Horizontal");//（长按）手柄左右 或 键盘AD 或 键盘左右     -1f 1f  不按归0
        上下键 = Input.GetAxis("Vertical");//（长按）手柄上下 或 键盘WS 或 键盘上下       -1f 1f  不按归0
        //奔跑键 = Input.GetKey(KeyCode.LeftShift) || 扳机键RT > 0.06f;//（长按）键盘左shift 或 RT键
        跳跃键 = Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.K);//（短按）手柄B 或 键盘K
        瞬移键 = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button3);//（短按）空格 或 手柄Y
        攻击1键 = Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.J);//（短按）手柄A 或 键盘J 或 鼠标左键
        防御键 = Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.LeftShift);//（短按）手柄X 或 键盘Shift



        //////////////////////  镜头手柄震动  //////////////////////
        if (镜头手柄震动 == 1)//1震动小
        {
            if (镜头震动)//镜头可震动
                CameraShake.Instance.camerashake(2.5f, 0.5f);//调用CM vcam1上的镜头震动脚本(第1数值是震动幅度 第2数值是震动时长)
            if (手柄震动)//手柄可震动
                震动time = 震动时长;
            镜头手柄震动 = 99;//99不能操作
        }
        if (镜头手柄震动 == 2)//2震动中
        {
            if (镜头震动)//镜头可震动
                CameraShake.Instance.camerashake(3.5f, 0.5f);//调用CM vcam1上的镜头震动脚本(第1数值是震动幅度 第2数值是震动时长)
            if (手柄震动)//手柄可震动
                震动time = 震动时长;
            镜头手柄震动 = 99;//99不能操作
        }
        if (镜头手柄震动 == 3)//2震动大
        {
            if (镜头震动)//镜头可震动
                CameraShake.Instance.camerashake(5f, 0.5f);//调用CM vcam1上的镜头震动脚本(第1数值是震动幅度 第2数值是震动时长)
            if (手柄震动)//手柄可震动
                震动time = 震动时长;
            镜头手柄震动 = 99;//99不能操作
        }

        //////////////////////  镜头震动停止  //////////////////////
        if (震动time > 0f)//连招time
        {
            震动time -= Time.deltaTime;//timerA计时器时间
            GamePad.SetVibration(playerIndex, 左马达震动幅度, 右马达震动幅度);//官方标准震动频率  两数值分别代表手柄  左  右  震动马达的功率
        }
        if (震动time <= 0f)
        {
            GamePad.SetVibration(playerIndex, 0f, 0f);//停止震动
            镜头手柄震动 = 0;//0不震动
        }
    }

}
