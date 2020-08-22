using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{

    public GameObject Dialog;
    public GameObject talkUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Dialog.SetActive(true);
        talkUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Dialog.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Dialog.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            talkUI.SetActive(true);
        }
    }
}
