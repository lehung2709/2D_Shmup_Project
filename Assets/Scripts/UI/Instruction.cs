using TMPro;
using UnityEngine;

public class Instruction : TextMeshProUGUI
{
    public float blinkSpeed = 2f; 

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);

        Color newColor = color;
        newColor.a = alpha;
        color = newColor;
    }
}
