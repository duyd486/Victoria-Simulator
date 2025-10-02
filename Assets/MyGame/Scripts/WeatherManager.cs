using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private Transform directionalLight;
    [SerializeField] private Volume mainVolume;

    [Header("Time In Day")]
    [SerializeField] private Texture morningSkyTexture;
    [SerializeField] private Texture afternoonSkyTexture;
    [SerializeField] private Texture nightSkyTexture;

    [SerializeField] float skyNormalSpeed = 150;
    [SerializeField] float skyChangeSpeed = 12000;

    private enum TimeInDay
    {
        Morning,
        Afternoon,
        Night,
    }
    private TimeInDay timeNow = TimeInDay.Morning;

    private WindParameter.WindParamaterValue windParamater = new WindParameter.WindParamaterValue();
    private HDRISky hDRISky;
    private Quaternion targetRotation = Quaternion.Euler(60, 0, 0);
    private float speedChange = 1f;

    [Header("Weather")]
    [SerializeField] private GameObject leafParticle;





    private void Start()
    {
        windParamater.customValue = skyNormalSpeed;
        VolumeProfile volumeProfile = mainVolume.sharedProfile;
        if(volumeProfile.TryGet<HDRISky>(out HDRISky component))
        {
            hDRISky = component;
        }
        hDRISky.hdriSky.Override(morningSkyTexture);
        targetRotation = Quaternion.Euler(60, 0, 0);
        timeNow = TimeInDay.Afternoon;
    }

    void Update()
    {
        directionalLight.localRotation = Quaternion.Lerp(
            directionalLight.localRotation,
            targetRotation,
            speedChange * Time.deltaTime
        );

        if (Input.GetKeyDown(KeyCode.I)) {
            ChangeTimeInDay();
        }
    }



    void ChangeTimeInDay()
    {
        switch (timeNow)
        {
            case TimeInDay.Morning:
                StartCoroutine(ChangeSky(morningSkyTexture));
                directionalLight.localRotation = Quaternion.identity;
                targetRotation = Quaternion.Euler(60, 0, 0);
                speedChange = 0.5f;
                timeNow = TimeInDay.Afternoon;
                break;
            case TimeInDay.Afternoon:
                StartCoroutine(ChangeSky(afternoonSkyTexture));
                targetRotation = Quaternion.Euler(32, 160, 120);
                speedChange = 1f;
                timeNow = TimeInDay.Night;
                break;
            case TimeInDay.Night:
                StartCoroutine(ChangeSky(nightSkyTexture));
                targetRotation = Quaternion.Euler(200, 0, 0);
                speedChange = 1f;
                timeNow = TimeInDay.Morning;
                break;
        }
    }

    private IEnumerator ChangeSky(Texture newSky)
    {
        windParamater.customValue = skyChangeSpeed;

        hDRISky.scrollSpeed.Override(windParamater);
        
        yield return new WaitForSeconds(1);

        hDRISky.hdriSky.Override(newSky);


        yield return new WaitForSeconds(1);

        windParamater.customValue = skyNormalSpeed;

        hDRISky.scrollSpeed.Override(windParamater);


    }

}
