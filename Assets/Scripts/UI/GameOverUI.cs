using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Must Assign")]
    [SerializeField]
    Image[] buttonImages;
    [SerializeField]
    TextMeshProUGUI[] buttonTexts;
    bool isLerping = false;
    float lerpTimer;
    private void OnEnable()
    {
        foreach (var image in buttonImages)
        {
            image.gameObject.SetActive(false);
        }
        StartCoroutine(AppearButtons());
    }

    private void Update()
    {
        if (isLerping == false)
            return;

        lerpTimer += Time.deltaTime;
        float t = Mathf.Min(lerpTimer*lerpTimer, 1);
        foreach (var image in buttonImages)
        {
            Color color = image.color;
            color.a = t;
            image.color = color;
        }

        foreach(var text in buttonTexts)
        {
            Color color = text.color;
            color.a = t;
            text.color = color;
        }

    }

    private IEnumerator AppearButtons()
    {
        isLerping = false;
        lerpTimer = 0f;
        yield return new WaitForSeconds(1f);
        foreach (var button in buttonImages)
        {
            button.gameObject.SetActive(true);

        }
        isLerping = true;

    }
}
