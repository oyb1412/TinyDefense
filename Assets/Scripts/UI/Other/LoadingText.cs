using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    public string content;

    private float timer = 0f;
    private int dotCount = 0;
    private void Awake() {
        tmp = GetComponent<TextMeshProUGUI>();
    }
   

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5f) {
            dotCount = (dotCount + 1) % 4; // 0~3
            tmp.text = content + new string('.', dotCount);
            timer = 0f;
        }
    }
}
