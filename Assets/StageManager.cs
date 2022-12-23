using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//씬을 로드하기 위해 선언
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject gpsObj;
    
     public void LoadNextScene(string sceneName)
    {
        SceneData.oldSceneName = SceneManager.GetActiveScene().name;
        //print("게임씬을 로드합니다.");
        SceneManager.LoadScene(sceneName);
        //SceneManager.LoadScene(1);    //지정한 씬의 인덱스
    }


    public void LoadNextSceneClose()
    {

        //print("게임씬을 로드합니다.");

        if (SceneManager.GetActiveScene().name == "CapImageScene")
        {
            SceneManager.LoadScene(SceneData.oldSceneName);
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
            
        }
        //SceneManager.LoadScene(1);    //지정한 씬의 인덱스
    }



    public void LoadNextSceneGPS(string sceneName)
    {
        GPSManager  gps = gpsObj.GetComponent<GPSManager>();
                PlayerPrefs.SetFloat("LATITUDE", gps. latitude);
               PlayerPrefs.SetFloat("LONGITUDE", gps.longitude);
        //       PlayerPrefs.SetFloat("LATITUDE", 35.14403f);
        //     PlayerPrefs.SetFloat("LONGITUDE",129.1123f);

        SceneData.oldSceneName = SceneManager.GetActiveScene().name;
        //print("게임씬을 로드합니다.");
        SceneManager.LoadScene(sceneName);
        //SceneManager.LoadScene(1);    //지정한 씬의 인덱스
    }


    public void LoadNextSceneCloseGPS()
    {
        GPSManager gps = gpsObj.GetComponent<GPSManager>();
        PlayerPrefs.SetFloat("LATITUDE", gps.latitude);
        PlayerPrefs.SetFloat("LONGITUDE", gps.longitude);
        //       PlayerPrefs.SetFloat("LATITUDE", 35.14403f);
        //     PlayerPrefs.SetFloat("LONGITUDE",129.1123f);

           //print("게임씬을 로드합니다.");
        SceneManager.LoadScene(SceneData.oldSceneName);
        //SceneManager.LoadScene(1);    //지정한 씬의 인덱스
    }
}
