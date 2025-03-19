using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Light flashLight;
    private bool isOn = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isOn = !isOn;
            flashLight.enabled = isOn;
        }
    }
}
