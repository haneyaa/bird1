using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    Image lockImg;//锁图片
    Image[] stars;//星星图片数组
    Text level;//等级文本框
    Button btn;//按钮组件

    private void Awake()
    {
        Init();//初始化方法
    }
    void Start()
    {
        
    }

    private void Init()
    {
        lockImg = GetComponent<Image>();//获取锁图片
        Image[] imgs = GetComponentsInChildren<Image>();
        //获取星星数组
        stars = new Image[3];//星星数组长度为3
        for (int i = 0; i < 3; i++)
        {
            stars[i] = imgs[i + 1];
        }
        level = GetComponentInChildren<Text>();//查找文本框
       
        btn = GetComponent<Button>();//查找按钮组件
        btn.onClick.AddListener(OnBtnClick);//添加按钮点击事件

        SetLock(true);
    }

    private void OnBtnClick()
    {
        SceneManager.LoadScene(1);//加载第一关
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 设置文本的数字
    /// </summary>
    /// <param name="n"></param>
    public void SetText(int n)
    {
        Debug.Log(level);
        level.text = n.ToString();
    }

    /// <summary>
    /// 设置星星数量显示
    /// </summary>
    /// <param name="count">通关时的星星数量</param>
    public void SetStars(int count)
    {
        for (int i = 0; i < 3; i++)//循环遍历3颗星星
        {
            if(i < count)//如果在通关星星数量之内
            {
                stars[i].color = Color.white;//星星被点亮
            }
            else//否则
            {
                stars[i].color = Color.gray;//星星是暗灰色
            }
        }
    }


    /// <summary>
    /// 关卡是否锁定
    /// </summary>
    /// <param name="flag"></param>
    public void SetLock(bool flag)
    {
        if (flag)//如果关卡锁定
        {
            lockImg.enabled = true;//锁图片显示
            for (int i = 0; i < 3; i++)//3颗星星不显示
            {
                stars[i].enabled = false;
            }
            level.enabled = false;//文字不显示·
            btn.enabled = false;//锁定状态，按钮不能点                   
        }
        else
        {
            lockImg.enabled = false;//锁图片显示
            for (int i = 0; i < 3; i++)//3颗星星不显示
            {
                stars[i].enabled = true;
            }
            level.enabled = true;//文字不显示
            btn.enabled = true;//不锁定按钮才能点击
        }
    }
}
