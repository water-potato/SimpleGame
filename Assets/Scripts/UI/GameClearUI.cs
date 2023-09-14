using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 4f)
            return;
        if(Input.touchCount > 0)
        {
            GameBuilder.Instance.GoToTitle();
        }

    }
}
