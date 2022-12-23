using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlManager : MonoBehaviour
{
    public WindZone wind;
    
    public Slider lightHslider;
    public Slider lightVslider;
    public GameObject dLight;
    Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = dLight.transform.eulerAngles;
        lightHslider.value = 0.5f;
        lightVslider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float h =lightHslider.value*120.0f - 60.0f;
        float v =lightVslider.value*120.0f - 60.0f;

        dLight.transform.eulerAngles = new Vector3(origin.x + h, origin.y + v, 0.0f);
    }

    public void WindOnOff()
    {
        AndroidToast.I.ShowToastMessage(("Wind: " + (!wind.gameObject.activeSelf).ToString()));
        wind.gameObject.SetActive(!wind.gameObject.activeSelf);
    }


}
