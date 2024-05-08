using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 残影 : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("时间控制参数")]
    public float activeTime = 1f;//显示时间
    public float activeStart;//开始显示的时间点

    [Header("不透明度控制")]
    private float alpha;//透明度
    public float alphaSet = 0.5f;//残影透明度初始值
    public float alphaMultiplier = 0.8f;//残影的间隔

    private void OnEnable()
    {
        player = GameObject.Find("主角动画").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;//透明度

        thisSprite.sprite = playerSprite.sprite;

        transform.position = new Vector3(player.position.x, player.position.y - 0.1f, player.position.z + 0.1f);//残影比主角Y轴低一点 Z轴后退一点 就不会和主角重叠
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    void Update()
    {
        color = new Color(1f, 0f, 0, alpha);//Color(1,1,1,1)前3数值各通道颜色 最后数值透明度

        alpha *= alphaMultiplier;//透明度渐变等于残影的间隔

        thisSprite.color = color;

        if (Time.time >= activeStart + activeTime)
        {
            //返回对象池
            残影管理.instance.ReturnPool(this.gameObject);
        }
    }

}