using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreateButtonBlogView : MonoBehaviour
{
    public GameObject obj;
    public Transform parent;
    private GameObject[] member = new GameObject[11];

    private static string url_head = "http://www.nanabunnonijyuuni.com/assets/img/blog/thumb_photo/thumb_";
    private string url_bottom;
    private string Image_url;

    private enum MemberName: int
    {
        amaki,
        umino,
        kawase,
        kuraoka,
        saijo,
        shirosawa,
        suzuhana,
        takatsuji,
        takeda,
        hokaze,
        miyase
    }

    private enum MemberSource: int
    {
        amaki = 1,
        umino,
        kuraoka,
        saijo,
        shirosawa,
        hanakawa,
        hokaze,
        miyase,
        suzuhana,
        takatsuji,
        takeda,
        kawase
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // プレハブを元にオブジェクトを生成する
        for (int i = 0; i < 11; i++)
        {
            url_bottom = "amaki";

            member[i] = (GameObject)Instantiate(obj);
            member[i].transform.Find("Text").GetComponent<Text>().text = i.ToString();
            switch (i)
            {
                case (int)MemberName.amaki:
                    url_bottom = "amaki"; 
                    break;
                case (int)MemberName.umino:
                    url_bottom = "umino";
                    break;
                case (int)MemberName.kawase:
                    url_bottom = "umino";
                    break;
                case (int)MemberName.kuraoka:
                    break;
                case (int)MemberName.saijo:
                    break;
                case (int)MemberName.shirosawa:
                    break;
                case (int)MemberName.suzuhana:
                    break;
                case (int)MemberName.takatsuji:
                    break;
                case (int)MemberName.takeda:
                    break;
                case (int)MemberName.hokaze:
                    break;
                case (int)MemberName.miyase:
                    break;
                default:
                    break;
            }

            Image_url = url_head + url_bottom + ".png";

            WWW webImage = new WWW(Image_url);

            // 画像ダウンロード完了を待機
            yield return webImage;

            // webサーバから取得した画像をRaw Imagで表示する
            member[i].GetComponent<RawImage>().texture = webImage.textureNonReadable;

            member[i].transform.SetParent(parent);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
