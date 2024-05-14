using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class 血量 : MonoBehaviour
{
    public TextMeshProUGUI 主角血量;//UI里文字写法
    public TextMeshProUGUI 怪物血量;//UI里文字写法

    private 主角 主角;//脚本名+ 自取名 shuju1  （private私人的不能看）
    private 怪物 怪物;//脚本名+ 自取名 shuju1  （private私人的不能看）

    void Start()
    {
        主角 = GameObject.Find("主角").GetComponent<主角>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        怪物 = GameObject.Find("怪物").GetComponent<怪物>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
    }

    // Update is called once per frame
    void Update()
    {
        主角血量.text = 主角.HP.ToString();//直接等于指定代码的数值
        怪物血量.text = 怪物.HP.ToString();//直接等于指定代码的数值
    }
}
