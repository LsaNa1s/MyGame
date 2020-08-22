
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public bool canTalk;
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;


    [Header("文本文件")]
    public TextAsset textFile;//文本文件
    public int index;//编号
    public float textSpeed;

    public bool textFinished;//完成打字
    public bool cancelTyping;//取消打字


    [Header("头像")]
    public Sprite face01, face02,face03;

    

    public List<string> textList = new List<string>();





    // Start is called before the first frame update
    void Awake()
    {
        GetTextFromFile(textFile);
        
    }

    public virtual void OnEnable()
    {
        cancelTyping = false;
        textFinished = true;
        canTalk = false;
        StartCoroutine(SetTextUI());//开启携程
        
 
        /*textLabel.text = textList[index];
        index++;*/
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(TouchPhase.Began == 0 && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            
            return;
        }
        /*if (Input.GetKeyDown(KeyCode.E) &&textFinished)
        {
            *//*textLabel.text = textList[index];
            index++;*//*
            StartCoroutine(SetTextUI());//开启携程
        }*/

        if (TouchPhase.Began == 0)
        {
            if(textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinished)
            {
                cancelTyping = !cancelTyping;
            }
        }
    }

    public void checkInput()
    {

        canTalk = true;

    }


    public void GetTextFromFile(TextAsset file)
    {
        textList.Clear();//开始时清空列表
        index = 0;//序号归零

        var LineData = file.text.Split('\n');//文本按行切割

        foreach(var line in LineData)
        {
            textList.Add(line);
        }

    }

    public IEnumerator SetTextUI()//携程
    {
        textFinished = false;
        textLabel.text = "";//列表清空
       //Debug.Log("执行携程");
        //Debug.Log("textList[index]=" + textList[index]);

        switch (textList[index][0])
        {
            case 'P':
                //Debug.Log("执行携程分字");
                faceImage.sprite = face01;
                index++;

                break;

            case 'E':
                //Debug.Log("执行携程分字");
                faceImage.sprite = face02;
                index++;
                break;

            case 'Z':
                //Debug.Log("执行携程分字");
                faceImage.sprite = face03;
                index++;
                break;
        }


        //实现逐字打印
        /*for (int i = 0; i < textList[index].Length; i++)
        {
            //Debug.Log("执行携程逐字打印");
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);//返回并等待
        }*/
        int letter = 0;
        while(!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter ++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;

        textFinished = true;
        index++;
    }


}
