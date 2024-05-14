using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 激光预制体 : MonoBehaviour
{
    private 怪物 怪物;//脚本名+ 自取名 shuju1  （private私人的不能看）
    float 销毁time = 2f;//
    private bool 右边运动;//
    public float 激光长度 = 0f;//

    void Start()
    {
        怪物 = GameObject.Find("怪物").GetComponent<怪物>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        if (怪物.朝向 == 1)//1朝向右
            右边运动 = true;
    }

    void Update()
    {
        if (销毁time > 1.4f)
            激光长度 += 350f * Time.deltaTime;//

        if (!右边运动)//0朝向左
            transform.localScale = new Vector3(激光长度, transform.localScale.y, transform.localScale.z);//人物翻转向左（因为怪物图片是向左的 所以和主角方向相反设置）
        if (右边运动)//1朝向右
            transform.localScale = new Vector3(-激光长度, transform.localScale.y, transform.localScale.z);//人物翻转向左（因为怪物图片是向左的 所以和主角方向相反设置）

        if (销毁time < 1.4f)
        {
            if (!右边运动)//0朝向左
                transform.Translate(Vector3.left * Time.deltaTime * 35f);//左
            if (右边运动)//1朝向右
                transform.Translate(Vector3.right * Time.deltaTime * 35f);//左
        }


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
