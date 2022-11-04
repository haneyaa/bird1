using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCtrl : MonoBehaviour
{
    public static BirdCtrl Instance;//单例
    public List<Bird> birds;//场景中所以小鸟的数组
    public List<GameObject> birdGos;
    public List<Pig> pigs;//场景中小猪的列表
    Vector3 startPos;//弹弓上的位置
    public int curIndex;//当前小鸟在数组的索引
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Init();
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// 初始化小鸟状态数组
    /// </summary>
    void Init()
    {
        int length = birds.Count;//获取小鸟数组的长度
        for (int i = 0; i < length; i++)
        {
            if(i == curIndex)//如果是弹弓上的鸟
            {
                birds[i].SetCurBird(true);//弹簧关节启用
                startPos = birds[i].startPos;//记录弹弓上的位置
            }
            else//否则，弹簧关节禁用
            {
                birds[i].SetCurBird(false);
            }
        }
    }

    /// <summary>
    /// 切換下一只小鳥
    /// </summary>
    public void Next()
    {
        if(curIndex < birds.Count - 1)//为了防止数组越界
        {
            curIndex++;//切换下一个索引
            
        }
        birds[curIndex].transform.position = startPos;
        birds[curIndex].SetCurBird(true);//启用弹簧关节
        
    }

    /// <summary>
    /// 游戏完成
    /// </summary>
    public void GameOver()
    {
        int pigCount = pigs.Count;
        int birdCount = birdGos.Count;//通关时小鸟的数量
        //胜利逻辑
        //if(birdCount <= 0)
        //{
        //    if(pigCount > 0)
        //    {
        //        GameMenu.Instance.ShowFailPanel();
        //    }
        //    else
        //    {
        //        GameMenu.Instance.ShowSuccessPanel(1);
        //    }
        //}
        //else
        //{
        //    if(pigCount > 0)
        //    {
        //        BirdCtrl.Instance.Next();//调用管理器类中下一只方法
        //    }
        //    else
        //    {
        //        GameMenu.Instance.ShowSuccessPanel(birdCount+1);
        //    }
            
        //}

        if(pigCount <= 0)//小猪数量小于0
        {
            GameMenu.Instance.ShowSuccessPanel(birdCount + 1);
        }
        else
        {
            if(birdCount <= 0)
            {
                GameMenu.Instance.ShowFailPanel();
            }
            else
            {
                BirdCtrl.Instance.Next();//调用管理器类中下一只方法
            }
        }
    }
  

    
}
