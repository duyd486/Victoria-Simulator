using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CellingFan : MonoBehaviour
{
    public float fanSpeed = 0f;
    public float onFanSpeed = 500f;
    void Update()
    {
        if (!GameManager.Instance.isCellingFanRotate && fanSpeed > 0)
        {
            fanSpeed -= Time.deltaTime * 50;
        }
        else if (fanSpeed < onFanSpeed)
        {
            fanSpeed += Time.deltaTime * 50;
        }
        transform.Rotate(Vector3.up * Time.deltaTime * fanSpeed);
    }
}
