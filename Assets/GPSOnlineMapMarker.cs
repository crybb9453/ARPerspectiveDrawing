using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GPSOnlineMapMarker : MonoBehaviour
{

       OnlineMaps om;
    
    
    void Start()
    {

        om =GetComponent<OnlineMaps>();
            
        OnlineMapsMarker m = new OnlineMapsMarker();

        float latitude = PlayerPrefs.GetFloat("LATITUDE");
        float  longitude = PlayerPrefs.GetFloat("LONGITUDE");
        om.SetPosition(longitude, latitude);
        om.zoom = 18;
       // om.RedrawImmediately();
        

        m.SetPosition(longitude, latitude);
        m.label = "ARPerspectiveDrawing";
        m.scale = 5.0f;
        GetComponent<OnlineMapsMarkerManager>().Add(m);

    }

}