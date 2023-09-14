using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set;}


    [Header("Must Assign")]
    [SerializeField]
    private BackGround backGround;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject TitleUI;

    [SerializeField]
    private GameObject gameClearUI;

    [SerializeField]
    private Image[] specialImages;

    Color transparentColor = new Color(1, 1, 1, 0);
    Color specialEndColor = new Color(1, 1, 1, .1f);

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        GameBuilder.Instance.onTitle += GameBuilder_OnTitle;
        GameBuilder.Instance.onFailure += GameBuilder_OnFailure;
        GameBuilder.Instance.onSuccess += GameBuilder_OnSuccess;
        GameBuilder.Instance.onGameOver += GameBuilder_OnGameOver;
        GameBuilder.Instance.onGameStart += GameBuilder_OnGameStart;
        GameBuilder.Instance.onGameClear += GameBuilder_OnGameClear;
    }

    private void Update()
    {
        switch (GameBuilder.Instance.CurrentNumber)
        {
            case 40:
                StartCoroutine(SetSpeicialImage(specialImages[0]));
                break;
            case 80:
                StartCoroutine(SetSpeicialImage(specialImages[1]));
                break;
            case 140:
                StartCoroutine(SetSpeicialImage(specialImages[2]));
                break;
            case 170:
                StartCoroutine(SetSpeicialImage(specialImages[3]));
                break;
            default:
                break;
        }
    }

    private void GameBuilder_OnTitle()
    {
        ResetUIs();
        TitleUI.SetActive(true);

    }
    private void GameBuilder_OnGameStart()
    {
        ResetUIs();
        backGround.Init();
    }
    private void GameBuilder_OnFailure(Target target)
    {
        backGround.OnFailure();
    }
    private void GameBuilder_OnSuccess(Target target)
    {
        backGround.OnSuccess();
    }
    private void GameBuilder_OnGameOver()
    {
        ResetUIs();
        backGround.EndColor();
        gameOverUI.SetActive(true);
    }
    private void GameBuilder_OnGameClear()
    {
        ResetUIs();
        gameClearUI.SetActive(true);
    }

    private void ResetUIs()
    {

        backGround.ResetColor();
        gameOverUI.SetActive(false);
        gameClearUI.SetActive(false);
        backGround.ResetColor();
        foreach (var image in specialImages)
        {
            image.color = transparentColor;
        }
    }

    IEnumerator SetSpeicialImage(Image image)
    {
        float t = 0;
        int maxTimeHalf = 50;
        for(int i=0; i < maxTimeHalf; i++)
        {
            t = Mathf.Clamp01((float) i / maxTimeHalf);
            image.color = Color.Lerp(transparentColor, specialEndColor, t);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = maxTimeHalf; i > 0; i--)
        {
            t = Mathf.Clamp01((float)i / maxTimeHalf);
            image.color = Color.Lerp(transparentColor, specialEndColor, t);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
