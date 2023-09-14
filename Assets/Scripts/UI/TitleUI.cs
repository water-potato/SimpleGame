using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] texts;
    private float timer = 0f;
    private Color startColor = Color.white;
    private Color endColor = Color.black;

    private bool isStarting = false;
    private void OnEnable()
    {
        timer = 0f;
        isStarting = false;
    }
    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Min(timer * timer * 2f , 1);
        if (isStarting)
        {
            foreach(var text in texts)
            {
                text.color = Color.Lerp(endColor, startColor, t);
            }
            return;
        }

        foreach (var text in texts)
        {
            text.color = Color.Lerp(startColor, endColor, t);
        }
        if (Managers.Input.CheckTouch() && isStarting == false)
        {
            StartCoroutine(GameStartCoroutine());
        }


    }


    private IEnumerator GameStartCoroutine()
    {
        isStarting = true;
        timer = 0f;
        yield return new WaitForSeconds(1f);
        GameBuilder.Instance.GameStart();
        gameObject.SetActive(false);
    }
}
