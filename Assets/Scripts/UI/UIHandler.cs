using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

    }


    private void Start()
    {
        GameBuilder.Instance.onTitle += GameBuilder_OnTitle;
        GameBuilder.Instance.onFailure += GameBuilder_OnFailure;
        GameBuilder.Instance.onSuccess += GameBuilder_OnSuccess;
        GameBuilder.Instance.onGameOver += GameBuilder_OnGameOver;
        GameBuilder.Instance.onGameStart += GameBuilder_OnGameStart;
    }

    private void GameBuilder_OnTitle()
    {
        TitleUI.SetActive(true);
        backGround.ResetColor();
        gameOverUI.SetActive(false);
    }
    private void GameBuilder_OnGameStart()
    {
        TitleUI.SetActive(false);
        backGround.Init();
        gameOverUI.SetActive(false);
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
        gameOverUI.SetActive(true);
    }

}
