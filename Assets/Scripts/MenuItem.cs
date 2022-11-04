using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public GameObject itemPrefab;//小关卡预制体
    public int count = 28;//小关卡数量
    List<Item> items;//用列表保存小关卡代码的引用
    Transform container;//父容器
    void Start()
    {
        Init();
        //Test();
       // Test2(16);
    }

   
    void Update()
    {
        
    }

    /// <summary>
    /// 初始化方法
    /// </summary>
    void Init()
    {
        container = transform.Find("container");
        items = new List<Item>();//先实例化列表
        for (int i = 0; i < count; i++)
        {
            //int tmp = i;
            GameObject item = Instantiate(itemPrefab, container);//在面板容器上生成小关卡
            items.Add(item.GetComponent<Item>());//把小关卡脚本保存到列表里
            item.name = (i + 1).ToString();//改名
            items[i].SetText(i +1);//设置文本显示

            
            if(i <= 0)//只打开第一关
            {
                items[i].SetLock(false);
            }

           
            
        }
        
        int starCount = PlayerPrefs.GetInt("level1");
        Debug.Log("玩家星星数："+starCount);
        items[0].SetStars(starCount);
    }

    void Test()
    {
        int count = 0;
        for (int i = 1; i <= 100; i++)
        {
            if(i%3 ==0 && i %5 != 0)
            {
                Debug.Log(i);
                count++;
            }
        }
        Debug.Log("个数:"+count);
    }

    private void Test1(int n)
    {
        Debug.Log(Convert.ToString(n,2));
    }

    void Test2(int n)
    {
        int chushu = n; //3
        
        int yushu = 0;
        string str = string.Empty;
        while (chushu >1)
        {
            
            yushu = chushu % 2;
            chushu = chushu / 2;
            
            str = str.Insert(0,yushu.ToString());
        }
        str = str.Insert(0,chushu.ToString());
        
        while (str.Length < 32)
        {
            str=str.Insert(0, "0");
        }
        
        Debug.Log(str);
    }
}
