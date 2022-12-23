using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class ARManager : MonoBehaviour
{
    ARRaycastManager arRaycastManager;
    Vector2 viewCenter;
    public GameObject indicator;
    public GameObject model;
    



    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        viewCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        model.SetActive(false);
        indicator.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

           
        if (arRaycastManager.Raycast(viewCenter, hits) && model.activeSelf==false)
        {
            Pose pos = hits[0].pose;
            pos.rotation = Quaternion.LookRotation(
                new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z));

            indicator.SetActive(true);
            indicator.transform.SetPositionAndRotation(pos.position, pos.rotation);

        }
        else
        {
            indicator.SetActive(false);

        }

        if (model.activeSelf == false)
        {
            // 버튼 위가 아닌 곳에 화면을 터치하였을 때에...
            if (Input.GetMouseButtonDown(0))
            {
                // 인디케이터가 활성화되어 있을 때
                if (indicator.activeSelf)
                {

                    model.transform.position = indicator.transform.position;
                    model.transform.rotation = indicator.transform.rotation;
                    model.SetActive(true);
                   

                }
            }
        }
        
    }
}
