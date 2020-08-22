using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //使用单例模式,一个类只有一个实例，并且这个实例是在整个全局可以访问的，可以确保当前这个类
    //在整个游戏过程中只被实例化一次，可以统筹管理这里的函数与方法。
    public static UIManager instanse;//实例化

    public GameObject healthBar;
    public GameObject gameOverPanel;


    [Header("UI Elements")]
    public GameObject pauseMenu;
    public GameObject winGame;
    public Slider bossHealthBar;

    private void Awake()
    {
        //判断当前UI是否存在
        if (instanse == null)
            instanse = this;
        else
            Destroy(gameObject);//删除当前


    }

    public void UpdateHealth(float currentHealth)//switch语句控制心的显示
    {
        switch (currentHealth)
        {
            case 3:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                healthBar.transform.GetChild(0).gameObject.SetActive(false);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }



    }

    public void PauseGame()//暂停游戏
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0;//百分比制，0为停止
    }

    public void ResumeGame()//继续游戏
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void WinGame()
    {
        winGame.SetActive(true);
    }

    public void SetBossHealth(float health)//设置boss血量
    {
        bossHealthBar.maxValue = health;
    }

    public void UpdateBossHealth(float health)//实时减少boss血条
    {
        bossHealthBar.value = health;
    }

    public void GameOverUI(bool playerDead)
    {
        gameOverPanel.SetActive(playerDead);
    }


}
