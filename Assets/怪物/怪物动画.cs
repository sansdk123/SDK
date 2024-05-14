using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物动画 : MonoBehaviour
{
    private 怪物 怪物;//脚本名+ 自取名 shuju1  （private私人的不能看）
    Rigidbody rb;//调用外部重力组件
    public GameObject 石头预制体;//
    public GameObject 激光预制体;//

    void Start()
    {
        怪物 = GameObject.Find("怪物").GetComponent<怪物>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        rb = GameObject.Find("怪物").GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void 待机()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        怪物.攻击中 = false;
    }

    private void 受击时间还原()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        Time.timeScale = 1;//时间控制 还原
        怪物.被打中 = false;
        GetComponent<Renderer>().material = 怪物.原始材质球;
        怪物.受击阶段 = 2;//0没受击中 1受击卡帧闪白中 2受击中
    }
    private void 受击结束()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        Time.timeScale = 1;//时间控制 还原
        怪物.被打中 = false;
        GetComponent<Renderer>().material = 怪物.原始材质球;
        怪物.受击阶段 = 0;//0没受击中 1受击卡帧闪白中 2受击中
    }

    private void 攻击结束()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        怪物.站立time = Random.Range(0.1f, 0.5f);//随机数
        怪物.休息time = Random.Range(0.5f, 2.1f);//随机数
        怪物.攻击中 = false;
    }


    private void 冲刺()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        //////////////////////  冲刺速度  //////////////////////
        if (怪物.朝向 == 0)//0朝向左
            rb.velocity = new Vector3(-15f, rb.velocity.y, rb.velocity.z);//往左走
        if (怪物.朝向 == 1)//1朝向右
            rb.velocity = new Vector3(15f, rb.velocity.y, rb.velocity.z);//往右走
    }

    private void 跳击()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        //////////////////////  跳跃惯性速度  //////////////////////
        rb.velocity = new Vector3(rb.velocity.x, 15f, rb.velocity.z);//跳跃Y速度
        rb.velocity = new Vector3(怪物.两者正负距离 * -1.4f, rb.velocity.y, rb.velocity.z);//惯性移动（准确跳到主角位置）
    }

    private void 投掷()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        Instantiate(石头预制体, GameObject.Find("石头预制体生成位置").transform.position, GameObject.Find("石头预制体生成位置").transform.rotation);
    }

    private void 激光()//这物体的动画帧数里加入事件 事件选择这个zhuanshen 就会在动画结束后加载这zhuanshen里面的代码
    {
        Instantiate(激光预制体, GameObject.Find("激光预制体生成位置").transform.position, GameObject.Find("激光预制体生成位置").transform.rotation);
    }

}
