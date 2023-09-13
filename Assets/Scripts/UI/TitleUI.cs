using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private float timer = 0f;
    private Color startColor = Color.white;
    private Color endColor = Color.black;
    private void OnEnable()
    {
        timer = 0f;

    }
    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Min(timer * timer * 2f , 1);
        text.color = Color.Lerp(startColor, endColor, t);

        if (Managers.Input.CheckTouch())
        {
            GameBuilder.Instance.GameStart();
            gameObject.SetActive(false);
        }
    }
}
