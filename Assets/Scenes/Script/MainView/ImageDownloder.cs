using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageDownloder : MonoBehaviour
{
    IEnumerator Start()
    {

        // wwwクラスのコンストラクタに画像URLを指定
        string url = "http://www.nanabunnonijyuuni.com/assets/img/blog/thumb_photo/thumb_amaki.png";
        WWW www = new WWW(url);

        // 画像ダウンロード完了を待機
        yield return www;

        // webサーバから取得した画像をRaw Imagで表示する
        RawImage rawImage = GetComponent<RawImage>();
        rawImage.texture = www.textureNonReadable;

        //ピクセルサイズ等倍に
        rawImage.SetNativeSize();
    }

}