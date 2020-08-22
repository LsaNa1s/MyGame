using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;//再次使用单例模式

    private PlayerController player;
    private Door doorExit;
    private DialogSystem dialogSystem;

    public bool gameOver;

    public List<Enemy> enemies = new List<Enemy>();
    public int DialogIndexNumber;


    public void Awake()
    {
        //单例模式赋值
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        /*player = FindObjectOfType<PlayerController>();//实例化player 
        doorExit = FindObjectOfType<Door>();*/

    }

    public void Update()
    {
        gameOver = player.isDead;
        UIManager.instanse.GameOverUI(gameOver);
        
    }


    //观察者模式========
    //让其他人汇报它们是什么，从而反方向调用
    public void IsEnemy(Enemy enemy)//如果是敌人加入集合
    {
        enemies.Add(enemy);
    }
    public void EnemyDead(Enemy enemy)//如果敌人集合为零打开门
    {
        enemies.Remove(enemy);

        if(enemies.Count == 0 )
        {
            doorExit.OpenDoor();
            SaveData();//存储当前场景数据
            if(SceneManager.GetActiveScene().name == "Boss")
            {
                UIManager.instanse.WinGame();
            }
        }
    }

    public void IsPlayer(PlayerController controller)//赋值player
    {
        player = controller;


    }

    public void IsExitDoor(Door door)
    {
        doorExit = door;
    }


    //=================

    public void RestartScene()//重启当前场景
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //PlayerPrefs.DeleteKey("playerHealth");//删除单个键值
        UIManager.instanse.ResumeGame();
    }

    public void NewGame()//开启新游戏
    {
        PlayerPrefs.DeleteAll();//清除所有保存PlayerPrefs的数据
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()//保存游戏
    {
        if (PlayerPrefs.HasKey("sceneIndex"))//判断sceneIndex含有键值
            SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));//获取保存的场景索引值
        else
            NewGame();
    }

    public void GoToMainMenu()//去主菜单
    {
        SceneManager.LoadScene(0);
        UIManager.instanse.ResumeGame();
    }

    public void NextLevel()//切换下一个场景
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()//退出游戏
    {
        Application.Quit();
    }

    public float LoadHealth()//血量加载
    {
        if (!PlayerPrefs.HasKey("playerHealth"))//如果存储数据中没有这个键值
        {
            PlayerPrefs.SetFloat("playerHealth", 3f);
        }
        float currentHealth = PlayerPrefs.GetFloat("playerHealth");//获得当前键值的存储值


        return currentHealth;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("playerHealth", player.health);//存储玩家血量
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex + 1);//保存当前场景的下一个场景的索引值
        PlayerPrefs.Save();
    }

}
