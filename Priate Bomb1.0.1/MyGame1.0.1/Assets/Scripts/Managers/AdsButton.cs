using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsButton : MonoBehaviour,IUnityAdsListener
{
    //条件编译
#if UNITY_IOS
    private string gameID = "3774866";
#elif UNITY_ANDROID
    private string gameID = "3774867";
#endif
    Button adsButton;
    public string placementID = "rewardedVideo";

    // Start is called before the first frame update
    void Start()
    {
        adsButton = GetComponent<Button>();
        /*adsButton.interactable = Advertisement.IsReady(placementID);*/

        if (adsButton)
        {
            adsButton.onClick.AddListener(ShowRewardAds);//监听
        }

        //广告初始化
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);
    }
    
    public void ShowRewardAds()
    {
        Advertisement.Show(placementID);
    }


    //广告接口内容
    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                Debug.Log("广告播完了有奖励");
                FindObjectOfType<PlayerController>().health = 3;
                FindObjectOfType<PlayerController>().isDead = false;
                UIManager.instanse.UpdateHealth(FindObjectOfType<PlayerController>().health);
                
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        if(Advertisement.IsReady(placementID))
            Debug.Log("我准备好了");
    }

    
}
