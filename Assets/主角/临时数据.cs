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
    public bool 奔跑键;//（长按）手柄/键盘
    public bool 跳跃键;//（短按）手柄/键盘/鼠标
    public bool 瞬移键;//（短按）手柄/键盘


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
        奔跑键 = Input.GetKey(KeyCode.LeftShift) || 扳机键RT > 0.06f;//（长按）键盘左shift 或 RT键
        跳跃键 = Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.K);//（短按）手柄B 或 键盘K
        瞬移键 = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button3);//（短按）空格 或 手柄Y

    }

}
