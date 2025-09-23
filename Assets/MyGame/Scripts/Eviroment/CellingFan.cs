using System.Threading.Tasks;
using UnityEngine;

public class CellingFan : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] public bool isRotate = true;

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }
    }
}
