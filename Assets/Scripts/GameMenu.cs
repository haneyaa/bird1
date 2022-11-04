using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu Instance;
    public GameObject successPanel;//胜利面板
    public GameObject failPanel;//失败面板

    public GameObject[] stars;//胜利面板上的3颗星星

    Button[] sucBtns;//胜利面板上的按钮数组
    Button[] failBtns;//失败面板上的按钮数组

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
    /// 初始化方法
    /// </summary>
    void Init()
    {


        sucBtns = successPanel.GetComponentsInChildren<Button>();//查找成功面板上的3个按钮
        sucBtns[0].onClick.AddListener(RetryClick);//重玩此关
        sucBtns[1].onClick.AddListener(ReturnMainMenu);
        sucBtns[2].onClick.AddListener(RetryClick);//因为还没做下一关，直接打开第一关
        //====失败面板相关========
        failBtns = failPanel.GetComponentsInChildren<Button>();//查找失败面板上的所有按钮数组
        failBtns[0].onClick.AddListener(RetryClick);//重玩此关
        failBtns[1].onClick.AddListener(ReturnMainMenu);
    }

    /// <summary>
    /// 重新加载第一关
    /// </summary>
    private void RetryClick()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 返回主面板。除了主面板以外，所有面板都关闭
    /// </summary>
    private void ReturnMainMenu()
    {

        
        SceneManager.LoadScene(0);
    }




    /// <summary>
    /// 几星通过，显示几颗星的特效
    /// </summary>
    /// <param name="n">通过的星星数</param>
    /// <returns></returns>
    IEnumerator ShowStars(int n)
    {
        
        for (int i = 0; i < n; i++)
        {
            yield return new WaitForSeconds(0.3f);//等待0.3秒，显示一颗星星
            stars[i].SetActive(true);
        }
    }

    /// <summary>
    /// 显示成功面板
    /// </summary>
    /// <param name="starn">通关的星星数</param>
    public void ShowSuccessPanel(int starn)
    {
        Debug.Log("调用了ShowSuccessPanel");
        int starCount = PlayerPrefs.GetInt("starCount");
        PlayerPrefs.SetInt("level1",starn);
        PlayerPrefs.SetInt("starCount",starCount + starn);
        successPanel.SetActive(true);//打开胜利面板
        StartCoroutine(ShowStars(starn));//动态显示通关特效
    }

    public void ShowFailPanel()
    {
        Debug.Log("调用了ShowFailPanel");
        failPanel.SetActive(true);
    }
}
