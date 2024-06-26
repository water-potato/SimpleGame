using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    public static GameBuilder Instance { get; private set; }

    enum GameState
    {
        Stop,
        Easy,
        Normal,
        Hard,
        Clear
    }


    private GameState State
    {
        get { return _state; }
        set
        {
            switch(value)
            {
                case GameState.Stop:
                    isStop = true;
                    prefabs = null;
                    break;
                case GameState.Easy:
                    isStop = false;
                    prefabs = easyTargetPrefabs;
                    break;
                case GameState.Normal:
                    prefabs = normalTargetPrefabs;
                    break;
                case GameState.Hard:
                    prefabs = hardTargetPrefabs;
                    break;
                case GameState.Clear:
                    isStop = true;
                    prefabs = null;
                    break;
            }
            _state = value;
        }
    }


    [Header("Target Prefabs")]
    [SerializeField] private GameObject[] easyTargetPrefabs;
    [SerializeField] private GameObject[] normalTargetPrefabs;
    [SerializeField] private GameObject[] hardTargetPrefabs;
    private GameObject[] prefabs;
    
    [SerializeField] private float targetLivingTime = 2f;

    [Header("Generate")]
    [SerializeField] private float maxGenperiod;
    [SerializeField] private float minGenperiod = .5f;
    private float periodTime = 5f;

    private int currentNumber;
    [Header("Game Over")]
    [SerializeField] private bool canOver = true;
    [SerializeField] private int maxFailureCount;
    private bool isStop;

    [Header("ReadOnly")]
    [SerializeField] private float generateTime;
    [SerializeField] private GameState _state;


    private float genTimer;
    private float pTimer;
    private float sTimer;

    // Start is called before the first frame update


    #region Events
    public event Target.TargetFailureHandler onFailure;
    public event Target.TargetFailureHandler onSuccess;
    public event Action onGameOver;
    public event Action onGameStart;
    public event Action onTitle;
    public event Action onGameClear;
    #endregion

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
        GoToTitle();
    }
    void Update()
    {

        if (canOver && isStop)
            return;


        genTimer += Time.deltaTime;
        pTimer += Time.deltaTime;
        sTimer += Time.deltaTime;
        // �� Ÿ�� �Ǹ� ��


        if (currentNumber < 200)
        {
            if (genTimer > generateTime)
            {
                GenerateTarget();
                genTimer = 0;
            }
            // �ð�  ������ �ֱ� ª����
            if (pTimer >= periodTime)
            {
                generateTime = Mathf.Max(generateTime - 0.125f, minGenperiod);
                pTimer = 0f;
            }
        }
        else if(Managers.Target.ActiveTargets.Count == 0)
        {
            GameClear();
        }

        CheckForState();

#if UNITY_EDITOR
        if(Managers.Input.CheckTargetForPC(out Target target))
        {
            target.Success();
        }
#elif UNITY_ANDROID

        if (Managers.Input.CheckTargetForMobile(out List<Target> targets))
        {
            foreach(Target target in targets)
                target.Success();
        }
#endif
    }

    public void GameStart()
    {
        Target.onAnyTargetFailure -= onAnyTargetFailure;
        Target.onAnyTargetSuccess -= onAnyTargetSuccess;
        Target.onAnyTargetFailure += onAnyTargetFailure;
        Target.onAnyTargetSuccess += onAnyTargetSuccess;


        isStop = false;
        currentNumber = 1;
        generateTime = maxGenperiod;


        genTimer = 0;
        pTimer = 0; 
        sTimer = 0;

        State = GameState.Easy;

        onGameStart?.Invoke();

    }

    private void GenerateTarget()
    {
        if (prefabs != null)
        {
            int index = UnityEngine.Random.Range(0, prefabs.Length);
            Managers.Target.GenerateTarget(currentNumber++, targetLivingTime, prefabs[index], GetRandomPosition());
        }

    }

    private void CheckForState()
    {
        float toNormal = 5f;
        float toHard = 20f;

        if(State == GameState.Stop || State == GameState.Clear)
        {
            return;
        }

        if(sTimer >= toHard && State == GameState.Hard)
        {
            return;
        }


        if(sTimer >= toNormal &&State != GameState.Normal)
        {
            State = GameState.Normal;
            return;
        }
        if (sTimer >= toHard && State != GameState.Hard)
        {
            State = GameState.Hard;
            return;
        }

    }

    private void GameClear()
    {
        State = GameState.Clear;
        onGameClear?.Invoke();
    }


    private void onAnyTargetSuccess(Target target)
    {
        onSuccess?.Invoke(target);
    }
    private void onAnyTargetFailure(Target target)
    {
        if (State == GameState.Stop)
            return;
        onFailure?.Invoke(target);
    }

    public void GameOver()
    {

        State = GameState.Stop;
        onGameOver?.Invoke();


    }

    public void GoToTitle()
    {
        State = GameState.Stop;
        onTitle?.Invoke();
    }

    private Vector2 GetRandomPosition() => Managers.Vector.GetRandomPositionInViewPort();

    public int MaxFailureCount => maxFailureCount;
    public int CurrentNumber => currentNumber;
    public bool CanOver => canOver;
}
