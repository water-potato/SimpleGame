using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackspaceChecker : MonoBehaviour
{
    bool firstFlag;
    bool secondFlag;

#if UNITY_ANDROID
    private AndroidJavaClass unityPlayer;
    private AndroidJavaObject unityActivity;
    private AndroidJavaClass toastClass;

    private void Start()
    {
        toastClass = new AndroidJavaClass("android.widget.Toast");
    }
#endif
    void Update()
    {
        if(secondFlag && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !firstFlag)
        {
            // 첫번째 뒤로가기
            firstFlag = true;
            ShowToastMessage("One More Tap to Quit");
            StartCoroutine(DoubleCheckTimer());
        }
        
    }


    [System.Diagnostics.Conditional("UNITY_ANDROID")]
    private void ShowToastMessage(string message)
    {
#if UNITY_ANDROID
        if (unityActivity != null)
            {
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 3);
                    toastObject.Call("show");
                }));
            }
#endif
    }

    private IEnumerator DoubleCheckTimer()
    {
        yield return new WaitForSeconds(.5f);
        secondFlag = true;
        yield return new WaitForSeconds(2);
        secondFlag = false;
    }
}
