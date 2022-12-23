using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenCap : MonoBehaviour
{
    public Canvas uiCanvas;
    public void CaptureStart()
    {

        StartCoroutine(MakeScreenShot());
    }

    public void CaptureStartMap()
    {

        StartCoroutine(MakeScreenShotMapt());
    }


    // Start is called before the first frame update
    void CaptureScreen()
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "ARPerspectiveDrawing_" + timestamp + ".png";
        CaptureScreenForMobile(fileName);
       
    }
     IEnumerator MakeScreenShot()
    {
        uiCanvas.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        CaptureScreen();
        uiCanvas.gameObject.SetActive(true);
        AndroidToast.I.ShowToastMessage("Screen Capture Success");
    }


    IEnumerator MakeScreenShotMapt()
    {
        
        GameObject.Find("CloseBtn").SetActive(false);
        GameObject.Find("ScreenCaptureBtn").SetActive(false);
        yield return new WaitForEndOfFrame();
        CaptureScreen();
        GameObject.Find("CloseBtn").SetActive(true);
        GameObject.Find("ScreenCaptureBtn").SetActive(true);
        AndroidToast.I.ShowToastMessage("Screen Capture Success");
    }


    private void CaptureScreenForMobile(string fileName)
    {
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
       
        // do something with texture
        string albumName = "ARPerspectiveDrawing";
        
        NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) =>
        { 
             Debug.Log(success);
            Debug.Log(path);
            PlayerPrefs.SetString("IMGPATH", path);


            GPSManager gps = GameObject.Find("GPSManager").GetComponent<GPSManager>();
            PlayerPrefs.SetFloat("LATITUDE", gps.latitude);
            PlayerPrefs.SetFloat("LONGITUDE", gps.longitude);
            SceneData.oldSceneName = SceneManager.GetActiveScene().name;
            
            SceneManager.LoadScene("CapImageScene");

        });

        // cleanup
        Object.Destroy(texture);
    }
}
