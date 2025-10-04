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
    [SerializeField] private ParticleSystem leafParticle;
    [SerializeField] private ParticleSystem smallRainParticle;
    [SerializeField] private ParticleSystem largeRainParticle;

    private Light directionLight;
    [SerializeField] private Light lightningLight;

    [SerializeField] private float normalLightIntensity = 10f;
    [SerializeField] private float rainLightIntensity = 5f;

    private enum Weather
    {
        FallingLeaf,
        SmallRain,
        LargeRain,
    }
    private Weather currentWeather = Weather.FallingLeaf;

    private bool isLightning = false;





    private void Start()
    {
        directionLight = directionalLight.GetComponent<Light>();
        windParamater.customValue = skyNormalSpeed;
        VolumeProfile volumeProfile = mainVolume.sharedProfile;
        if(volumeProfile.TryGet<HDRISky>(out HDRISky component))
        {
            hDRISky = component;
        }


        ChangeTimeInDay();
        ChangeWeather();
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
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeWeather();
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
                targetRotation = Quaternion.Euler(270, 0, 0);
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


    private void ChangeWeather()
    {
        DisableParticle();
        switch (currentWeather)
        {
            case Weather.FallingLeaf:
                directionLight.intensity = normalLightIntensity;
                leafParticle.gameObject.SetActive(true);
                currentWeather = Weather.SmallRain;
                break;
            case Weather.SmallRain:
                directionLight.intensity = rainLightIntensity;
                smallRainParticle.gameObject.SetActive(true);
                currentWeather = Weather.LargeRain;
                break;
            case Weather.LargeRain:
                isLightning = true;
                directionLight.intensity = rainLightIntensity - 1;
                largeRainParticle.gameObject.SetActive(true);
                currentWeather = Weather.FallingLeaf;
                StartCoroutine(PlayLightning());
                break;
        }
    }

    private IEnumerator PlayLightning()
    {
        while (isLightning)
        {
            float wait = Random.Range(3, 5);
            yield return new WaitForSeconds(wait);

            int flashCount = Random.Range(1, 4);
            for (int i = 0; i < flashCount; i++)
            {
                lightningLight.gameObject.SetActive(true);
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                lightningLight.gameObject.SetActive(false);
                yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
            }
        }
    }

    private void DisableParticle()
    {
        isLightning = false;
        foreach(Transform chil in transform)
        {
            chil.gameObject.SetActive(false);
        }
    }

}
