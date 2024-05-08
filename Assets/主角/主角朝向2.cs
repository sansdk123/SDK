using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 主角朝向2 : GenericBehaviour
{
    void Start()
    {
        behaviourManager.SubscribeBehaviour(this);
        behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
    }

    void Update()
    {

    }

    public override void LocalFixedUpdate()//主角旋转相关
    {
        MovementManagement(behaviourManager.GetH, behaviourManager.GetV);//主角旋转相关
    }

    void MovementManagement(float horizontal, float vertical)//主角旋转相关
    {
        //这里可以用如果不开枪条件 才能加载Rotating(horizontal, vertical); 控制视觉
        Rotating(horizontal, vertical);//主角旋转相关
    }

    Vector3 Rotating(float horizontal, float vertical)//主角旋转相关
    {
        Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

        forward.y = 0.0f;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 targetDirection;
        targetDirection = forward * vertical + right * horizontal;

        if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.人物旋转朝向速度);//人物旋转朝向速度
            behaviourManager.GetRigidBody.MoveRotation(newRotation);
            behaviourManager.SetLastDirection(targetDirection);
        }
        if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
        {
            behaviourManager.Repositioning();
        }

        return targetDirection;
    }

}
