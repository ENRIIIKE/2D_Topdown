using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateScript : MonoBehaviour
{
    private TextMeshProUGUI framerateText;
    private float overframe = 120;
    private void Start()
    {
        framerateText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        float framerate = (1 / Time.deltaTime);
        framerate = Mathf.Round(framerate);

        if(framerate > overframe)
        {
            framerate = overframe;
        }

        framerateText.text = framerate.ToString();
    }
}
