using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FirstDialog : DialogSystem
{
    public Door doorExit;
    

    public override void Update()
    {
        if (index == textList.Count && canTalk)
        {
            gameObject.SetActive(false);
            index = 0;
            door();
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
   

    public void door()
    {
        doorExit.OpenDoor();
        GameManager.instance.SaveData();
    }


}
