using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 主角朝向 : MonoBehaviour
{
	private 临时数据 临时数据;//脚本名+ 自取名 shuju  （private私人的不能看）
    private 主角 主角;//脚本名+ 自取名 shuju1  （private私人的不能看）
    public Transform playerCamera;//(主角朝向2 脚本要用这镜头 所以只能public 不能private)
    public float 人物旋转朝向速度 = 1f;//人物旋转朝向速度 1是最快速度旋转  0.06是最顺滑慢速度旋转

    public float h;//左右按键数值
    public float v;//上下按键数值
    private int defaultBehaviour;
	private Vector3 lastDirection;
	private List<GenericBehaviour> behaviours;
	private Rigidbody rBody;

	public float GetH { get { return h; } }
	public float GetV { get { return v; } }
	public Rigidbody GetRigidBody { get { return rBody; } }
	public int GetDefaultBehaviour { get { return defaultBehaviour; } }

	void Awake()
	{
		临时数据 = GameObject.Find("临时数据").GetComponent<临时数据>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        主角 = GameObject.Find("主角").GetComponent<主角>();//自取名 shuju1 = 获取("脚本所在的物体的名字").<脚本名>() 只能调用public类型函数
        playerCamera = GameObject.Find("Main Camera").transform;//
        behaviours = new List<GenericBehaviour>();
		rBody = GetComponent<Rigidbody>();
	}

	void Update()
	{
        if (临时数据.左右键 < 0)
            h = -1;//左
        if (临时数据.左右键 > 0)
            h = 1;//右
        if (临时数据.左右键 == 0)
            h = 0;//左右0


        if (临时数据.上下键 < 0)
        {
            if (主角.移动模式3D)//3D移动模式(3D模式按上下才有效旋转)
                v = -1;//下
        }
        if (临时数据.上下键 > 0)
        {
            if (主角.移动模式3D)//3D移动模式(3D模式按上才有效旋转)
                v = 1;//上
        }
        if (临时数据.上下键 == 0)
            v = 0;//上下0
    }


	void FixedUpdate()
	{
        foreach (GenericBehaviour behaviour in behaviours)
        {
            behaviour.LocalFixedUpdate();
        }
    }
	public void SubscribeBehaviour(GenericBehaviour behaviour)
	{
		behaviours.Add(behaviour);
	}
	public void RegisterDefaultBehaviour(int behaviourCode)
	{
		defaultBehaviour = behaviourCode;
	}
	public bool IsMoving()
	{
		return (h != 0) || (v != 0);
	}
	public void SetLastDirection(Vector3 direction)
	{
		lastDirection = direction;
	}
	public void Repositioning()
	{
		if (lastDirection != Vector3.zero)
		{
			lastDirection.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
			Quaternion newRotation = Quaternion.Slerp(rBody.rotation, targetRotation, 人物旋转朝向速度);//人物旋转朝向速度
            rBody.MoveRotation(newRotation);
		}
	}
}


public abstract class GenericBehaviour : MonoBehaviour
{
	protected 主角朝向 behaviourManager;//获取自身主角朝向脚本
    protected int behaviourCode;

	void Awake()
	{
		behaviourManager = GetComponent<主角朝向>();//获取自身主角朝向脚本
        behaviourCode = this.GetType().GetHashCode();
	}

	public virtual void LocalFixedUpdate() { }

}
