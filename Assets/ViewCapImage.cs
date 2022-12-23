using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class ViewCapImage : MonoBehaviour
{

     RawImage rawImage; // �ҷ��� �̹����� ������ RawImage
    private void Start()
    {
        string path = PlayerPrefs.GetString("IMGPATH");
        
        rawImage = GetComponent<RawImage>();


        //NativeGallery.GetImageFromGallery((path) =>
     //   {
       //     FileInfo selectedImage = new FileInfo(path);

         //   if (!string.IsNullOrEmpty(path))
                StartCoroutine( LoadImage(path) ) ;

       // });
    }

    //�̹��� �ε� �ڷ�ƾ            
    IEnumerator LoadImage(string imagePath)
    {
        byte[] imageData = File.ReadAllBytes(imagePath);
        string imageName = Path.GetFileName(imagePath).Split('.')[0];
        string saveImagePath = Application.persistentDataPath + "/ARPerspectiveDrawing";
        Debug.Log(imagePath);
        Debug.Log(imageName);
        Debug.Log(saveImagePath);

        File.WriteAllBytes(saveImagePath + imageName + ".png", imageData);

        var tempImage = File.ReadAllBytes(imagePath);

        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        texture.LoadImage(tempImage);

        rawImage.texture = texture;

        yield return null;

    }


}
