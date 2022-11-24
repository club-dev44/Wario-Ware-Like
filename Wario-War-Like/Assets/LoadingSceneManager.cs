using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{

    private GameManager gameManager;
    private AsyncOperation loadingSceneOperation;
    [SerializeField]
    private Slider loaderFill;

    private float startLoadingTimeStamp;
    private void Awake()
    {
        gameManager = GameManager.Instance; 
        StartCoroutine(tempDeChargement());
    }

    
    
    IEnumerator tempDeChargement()
    {
        yield return null;

        startLoadingTimeStamp = Time.time;
        loadingSceneOperation = gameManager.chargerProchainJeuxAsync();
        while (!loadingSceneOperation.isDone)
        {
            Debug.Log("Loading: " + loadingSceneOperation.progress);
            loaderFill.value = Mathf.Lerp(loaderFill.value,loadingSceneOperation.progress,Time.deltaTime/5);
            if (loadingSceneOperation.progress >= 0.9f)
                break;
            yield return null;
        }
        loaderFill.value = 0.99f;
        yield return new WaitForSeconds(3.0f - (Time.time - startLoadingTimeStamp) );
        loadingSceneOperation.allowSceneActivation = true;
        
        
    }

}
