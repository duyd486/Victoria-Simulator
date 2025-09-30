using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private Transform directionalLight;
    [SerializeField] private Volume mainVolume;

    [SerializeField] private Texture morningSkyTexture;
    [SerializeField] private Texture afternoonSkyTexture;
    [SerializeField] private Texture nightSkyTexture;


    private HDRISky hDRISky;
    private Quaternion targetRotation = Quaternion.Euler(60, 0, 0);
    private float speedChange = 1f;

    private enum TimeInDay
    {
        Morning,
        Afternoon,
        Night,
    }
    private TimeInDay timeNow = TimeInDay.Morning;

    private void Start()
    {
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
                directionalLight.localRotation = Quaternion.identity;
                hDRISky.hdriSky.Override(morningSkyTexture);
                targetRotation = Quaternion.Euler(60, 0, 0);
                speedChange = 0.5f;
                timeNow = TimeInDay.Afternoon;
                break;
            case TimeInDay.Afternoon:
                hDRISky.hdriSky.Override(afternoonSkyTexture);
                targetRotation = Quaternion.Euler(32, 160, 120);
                speedChange = 1f;
                timeNow = TimeInDay.Night;
                break;
            case TimeInDay.Night:
                hDRISky.hdriSky.Override(nightSkyTexture);
                targetRotation = Quaternion.Euler(200, 0, 0);
                speedChange = 1f;
                timeNow = TimeInDay.Morning;
                break;
        }
    }

}
