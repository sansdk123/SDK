using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 石头预制体 : MonoBehaviour
{
    private 怪物 怪物;//脚本名+ 自取名 shuju1  （private私人的不能看）
    Rigidbody rb;//调用外部重力组件
    float 销毁time = 2f;//

    void Start()
    {
        怪物 = GameObject.Find("怪物").GetComponent<怪物>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        rb = GetComponent<Rigidbody>();
        //////////////////////  跳跃惯性速度  //////////////////////
        rb.velocity = new Vector3(rb.velocity.x, 15f, rb.velocity.z);//跳跃Y速度
        rb.velocity = new Vector3(怪物.两者正负距离 * -1.4f, rb.velocity.y, rb.velocity.z);//惯性移动（准确跳到主角位置）
    }

    void Update()
    {
        //////////////////////  销毁时间  //////////////////////
        if (销毁time > 0f)//
            销毁time -= Time.deltaTime;//timerA计时器时间
        if (销毁time <= 0f)//
            Destroy(gameObject);//销毁自身
    }

    private void OnTriggerEnter(Collider collision)//3D物体（要碰撞的两个物体都其中一个要勾选is Trigger   Stay是持续检测   Enter是检测一次 Exit是离开）
    {
        if (collision.name == "主角")//如果摸到tag标签是"Dimian"的物体               //玩家数据
        {
            //Debug.Log("碰撞体名称：" + collision.name);//摸到的物体名字
            //Destroy(gameObject);//销毁自身
        }
    }

}
