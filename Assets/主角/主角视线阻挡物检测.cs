using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 主角视线阻挡物检测 : MonoBehaviour
{
    [SerializeField] private LayerMask environmentLayer;//设置射线可以摸到的层  (比如 现在要用的 射线遮挡层)
    GameObject 视线阻挡物1;//挡在主角面前的物体
    GameObject 视线阻挡物2;//挡在主角面前的物体
    Material 原始材质球;//原始材质
    public Material 透明材质球; //把透明材质球拉到这里
    public bool 更换材质球;//是否更换了材质球
    void Start()
    {

    }

    void Update()
    {
        /////////////////镭射射线检测 地面 和 斜坡角度////////////////////////
        Ray ray = new Ray(transform.position, Vector3.back);//射线方向   如果射线方向是上下的话直接 Vector3.up  Vector3.down  不用纠正坐标方向  如果是其它方向的话参考敌人射线
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, environmentLayer))//0.5f是射线长度  射线长度可以决定摸到地面的 爬坡度数  射线越短可以爬坡的角度越小 反之可以爬坡角度越大
        {
            if (hit.collider)// 如果射线击中了物体，hit.collider将包含那个物体的Collider组件
            {
                视线阻挡物1 = hit.collider.gameObject;
                if (更换材质球 == false)// 如果射线击中了物体，hit.collider将包含那个物体的Collider组件
                {
                    视线阻挡物2 = hit.collider.gameObject;
                    原始材质球 = 视线阻挡物2.GetComponent<Renderer>().material;
                    视线阻挡物2.GetComponent<Renderer>().material = 透明材质球;
                    更换材质球 = true;
                }
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);//观察射线用  很吃性能  观察完删掉
            //Debug.Log(hit.collider.name);//摸到的物体名字
        }
        else
        {
            if (更换材质球 == true)// 如果射线击中了物体，hit.collider将包含那个物体的Collider组件
            {
                视线阻挡物2.GetComponent<Renderer>().material = 原始材质球;
                更换材质球 = false;
            }
            //Debug.Log("没有物体");//摸到的物体名字
        }
        if (更换材质球 == true && 视线阻挡物2.name != 视线阻挡物1.name)// 如果射线击中了物体，hit.collider将包含那个物体的Collider组件
        {
            视线阻挡物2.GetComponent<Renderer>().material = 原始材质球;
            视线阻挡物2 = 视线阻挡物1;
            更换材质球 = false;
        }
    }

}
