using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//안드로이드 전용 (퍼미션)
using UnityEngine.Android;

public class GPSManager : MonoBehaviour
{

    
    public static GPSManager instance;
    public Text latitude_text;
    public Text longitude_text;
    //10번 요청
    public float maxWaitTime = 10.0f;

    //1초에 한번씩
    public float resendTime = 2.0f;

    public float latitude;
    public float longitude;
    float waitTime = 0;

    public bool receiveGPS = false;


    //싱글톤
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(GPS_On());

    }

    IEnumerator GPS_On()
    {
        // 만일, 위치 정보 수신에 대해 사용자의 허가를 받지 못했다면...
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            // 허가를 요청하는 팝업 띄운다.
            Permission.RequestUserPermission(Permission.FineLocation);

            // 동의 받았는지 확인될 때까지 잠시 대기한다.
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                //프레임당 한번씩
                yield return null;
            }
        }

        // 만일, 사용자의 GPS 장치가 켜져있지 않다면, 함수를 종료한다.
        if (!Input.location.isEnabledByUser)
        {
            if(latitude_text != null)
            {
                latitude_text.text = "GPS off";
            }
            if (longitude_text != null)
            {
                longitude_text.text = "GPS off";
            }
            //코루팀 멈춤
            yield break;
        }

        // 위치 데이터를 요청한다.
        Input.location.Start();

        // 만일, 위치 데이터를 받으려고 하는 중이라면 대기한다.
        while (Input.location.status == LocationServiceStatus.Initializing && waitTime < maxWaitTime)
        {
            //1초에 한번씩
            yield return new WaitForSeconds(1.0f);
            waitTime++;
        }

        //에러처리부분
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            // 위치 정보 수신에 실패했다고 표시한다.
            if (latitude_text != null)
            {
                latitude_text.text = "위치 정보 수신 실패!";
            }
            longitude_text.text = "위치 정보 수신 실패!";
        }

        if (waitTime >= maxWaitTime)
        {
            if (latitude_text != null)
            {
                latitude_text.text = "응답 대기 시간 초과!";
            }
            if (longitude_text != null)
            {
                longitude_text.text = "응답 대기 시간 초과!";
            }
        }

        receiveGPS = true;

        while (receiveGPS)
        {
            // 수신된 위치 정보 데이터를 UI에 출력한다.
            LocationInfo li = Input.location.lastData;
            //위도 정도 
            latitude = li.latitude;
            longitude = li.longitude;

            if (latitude_text != null)
            {
                latitude_text.text = "위도: " + latitude.ToString();
            }
            if (longitude_text != null)
            {
                longitude_text.text = "경도: " + longitude.ToString();
            }

            //1초에 한번씩 받음
            yield return new WaitForSeconds(resendTime);
        }
    }
}