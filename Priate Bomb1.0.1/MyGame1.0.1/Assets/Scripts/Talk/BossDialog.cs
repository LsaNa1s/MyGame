using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BossDialog : DialogSystem
{
    public GameObject Enemy;


    public void Start()
    {
        Debug.Log("子类开始");
        /*Time.timeScale = 0;*/
        
    }

    public override void Update()
    {
        if (canTalk && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            Enemy.gameObject.SetActive(true);
            canTalk = false;
            return;
        }

        if (canTalk)
        {
            if (textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinished)
            {
                cancelTyping = !cancelTyping;
            }
            canTalk = false;
        }
        
    }

    

}
