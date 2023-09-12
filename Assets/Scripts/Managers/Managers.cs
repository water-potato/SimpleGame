using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;
    public static Managers Instance { get { Setup(); return instance; } }


    #region Managers

    private TargetManager target = new TargetManager();
    private VectorManager vector = new VectorManager();
    private InputManager input = new InputManager();
    public TargetManager Target { get { return Instance.target; } }
    public VectorManager Vector { get { return Instance.vector; } }
    public InputManager Input { get { return Instance.input; } }
    #endregion

    private static void Setup() {
        if (instance != null)
            return;

        GameObject go = FindAnyObjectByType<Managers>()?.gameObject;

        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }
        DontDestroyOnLoad(go);
        instance = go.GetComponent<Managers>();

        instance.target.Setup();
        instance.vector.Setup();
        
    }
}
