using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;//单例
    public Transform mainPanel;//主菜单面板
    public Transform infoPanel;//详细列表面板
    //public GameObject successPanel;//胜利面板
    //public GameObject failPanel;//失败面板

    //public GameObject[] stars;//胜利面板上的3颗星星
    public GameObject[] levels;//主面板上4个大菜单
    Button returnBtn;//详细界面上的返回按钮
    //Button[] sucBtns;//胜利面板上的按钮数组
    //Button[] failBtns;//失败面板上的按钮数组

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
        levels[0].GetComponent<Button>().onClick.AddListener(OpenInfoPanel);//打开详细关卡面板
        levels[0].GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("starCount").ToString();//获取星星数量
        returnBtn = infoPanel.Find("returnBtn").GetComponent<Button>();
        returnBtn.onClick.AddListener(ReturnMainMenu);//返回主菜单

        //sucBtns = successPanel.GetComponentsInChildren<Button>();//查找成功面板上的3个按钮
        //sucBtns[0].onClick.AddListener(RetryClick);//重玩此关
        //sucBtns[1].onClick.AddListener(ReturnMainMenu);
        //sucBtns[2].onClick.AddListener(RetryClick);//因为还没做下一关，直接打开第一关
        ////====失败面板相关========
        //failBtns = failPanel.GetComponentsInChildren<Button>();//查找失败面板上的所有按钮数组
        //failBtns[0].onClick.AddListener(RetryClick);//重玩此关
        //failBtns[1].onClick.AddListener(ReturnMainMenu);
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
        mainPanel.gameObject.SetActive(true);
        infoPanel.gameObject.SetActive(false);
        //successPanel.SetActive(false);
        //failPanel.SetActive(false);
    }

    /// <summary>
    /// 打开详细关卡面板
    /// </summary>
    private void OpenInfoPanel()
    {
        infoPanel.gameObject.SetActive(true);
        mainPanel.gameObject.SetActive(false);//隐藏主界面
    }

    /// <summary>
    /// 几星通过，显示几颗星的特效
    /// </summary>
    /// <param name="n">通过的星星数</param>
    /// <returns></returns>
    //IEnumerator ShowStars(int n)
    //{
    //    for (int i = 0; i < n; i++)
    //    {
    //        yield return new WaitForSeconds(0.3f);//等待0.3秒，显示一颗星星
    //        stars[i].SetActive(true);
    //    }
    //}

    /// <summary>
    /// 显示成功面板
    /// </summary>
    /// <param name="starn">通关的星星数</param>
    //public void ShowSuccessPanel(int starn)
    //{
    //    successPanel.SetActive(true);//打开胜利面板
    //    StartCoroutine(ShowStars(starn));//动态显示通关特效
    //}

    //public void ShowFailPanel()
    //{
    //    failPanel.SetActive(true);
    //}
}
