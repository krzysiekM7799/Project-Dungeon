using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    Text fpsText;
    float deltaTime;

    private void Start()
    {
        fpsText = GetComponent<Text>();
    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }
}