using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;


//引用 : https://qiita.com/fukaken5050/items/9619aeeb131120939bc1

public class ScreenShot : MonoBehaviour
{
    private string _fileName = "";
    public GameObject ar_view_pannel;
    public GameObject bottmNavigationBar;
    public GameObject screenShotButton;


    void Start()
    {
    }

    void Update()
    {
    }

    public void OnClick()
    {
        ar_view_pannel.SetActive(false);
        bottmNavigationBar.SetActive(false);
        screenShotButton.SetActive(false);
        StartCoroutine(WriteFileProcess());
        Invoke("SetScreenActiv", 1.0f);
    }

    private void SetScreenActiv()
    {
        ar_view_pannel.SetActive(true);
        bottmNavigationBar.SetActive(true);
        screenShotButton.SetActive(true);

    }



    private IEnumerator WriteFileProcess()
    {
        _fileName = "Screenshot" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        yield return CaptureScreenshotProcess();
        yield return MediaDirWriteFileProcess();
    }

    private IEnumerator CaptureScreenshotProcess()
    {
        Debug.Log("CaptureScreenshotProcess");
        yield return new WaitForEndOfFrame();
        string path = null;
#if UNITY_EDITOR
        path = _fileName;
#elif UNITY_ANDROID
        path = Application.persistentDataPath + "/" + _fileName;
#endif

        Debug.Log("BeginCaptureScreenshot:" + path);
        ScreenCapture.CaptureScreenshot(_fileName);
        Debug.Log("AfterCaptureScreenshot:" + path);

        while (File.Exists(path) == false)
        {
            Debug.Log("NoFile:" + path);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("CaptureOK:" + path);
        scanFile(path, null);//"image/png";
    }

    private IEnumerator MediaDirWriteFileProcess()
    {
        Debug.Log("MediaDirWriteFileProcess");
        if (Application.platform != RuntimePlatform.Android)
            yield return null;
#if UNITY_ANDROID
        var path = Application.persistentDataPath + "/" + _fileName;
        while (File.Exists(path) == false)
        {
            Debug.Log("NoFile:" + path);
            yield return new WaitForEndOfFrame();
        }
        // 保存パスを取得
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
        using (AndroidJavaObject joPublicDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", jcEnvironment.GetStatic<string>("DIRECTORY_PICTURES"/*"DIRECTORY_DCIM"*/ )))
        {
            var outputPath = joPublicDir.Call<string>("toString");
            Debug.Log("MediaDir:" + outputPath);
            //              outputPath += "/100ANDRO/" + _fileName;
            outputPath += "/Screenshots/" + _fileName;
            var pngBytes = File.ReadAllBytes(path);
            File.WriteAllBytes(outputPath, pngBytes);
            yield return new WaitForEndOfFrame();
            while (File.Exists(outputPath) == false)
            {
                Debug.Log("NoFile:" + outputPath);
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("MediaDirWriteFileOK:" + outputPath);
            scanFile(outputPath, null);
        }
#endif
    }

    //インデックス情報にファイル名を登録する
    //これをしないとPC から内部ストレージを参照した時にファイルが見えない
    static void scanFile(string path, string mimeType)
    {
        if (Application.platform != RuntimePlatform.Android)
            return;
#if UNITY_ANDROID
        using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject>("getApplicationContext"))
        using (AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass("android.media.MediaScannerConnection"))
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
        using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
        {
            Debug.Log("scanFile:" + path);
            var mimeTypes = (mimeType != null) ? new string[] { mimeType } : null;
            jcMediaScannerConnection.CallStatic("scanFile", joContext, new string[] { path }, mimeTypes, null);
        }
        Handheld.StopActivityIndicator();
#endif
    }
}