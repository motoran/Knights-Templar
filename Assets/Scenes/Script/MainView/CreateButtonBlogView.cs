using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MemberData
{
    public int getNumberOfMember()
    {
        return (int)MemberLineUp.num;
    }

    public string getMemberName(int num)
    {
        return MemberName[num];
    }

    public string getMemberSource(int num)
    {
        for (int i = (int)MemberSource.amaki; i < (int)MemberSource.num; i++)
        {
            if (Enum.GetName(typeof(MemberLineUp), num) == Enum.GetName(typeof(MemberSource), i))
            {
                return url + i.ToString("D2");
            }
        }
        return "null";
    }

    public string getMemberImageURL(int num)
    {
        return "http://www.nanabunnonijyuuni.com/assets/img/blog/thumb_photo/thumb_" + Enum.GetName(typeof(MemberLineUp), num) + ".png";
    }

    private enum MemberLineUp
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
        miyase,
        num = 11
    }

    private enum MemberSource
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
        kawase,
        num = 13
    }
    
    private string[] MemberName = { "天城サリー", "海乃るり", "河瀬詩", "倉岡水巴", "西條和", "白沢かなえ", "涼花萌", "高辻麗", "武田愛奈", "帆風千春", "宮瀬玲奈" };

    private string url = "http://blog.nanabunnonijyuuni.com/s/n227/diary/blog/list?ct=";
}

public class CreateButtonBlogView : MonoBehaviour
{
    List<GameObject> Buttons; //取得したボタンを格納する

    // Start is called before the first frame update
    IEnumerator Start()
    {
        GameObject area = GameObject.FindGameObjectWithTag("BlogButtonArea");
        Buttons = new List<GameObject>();
        MemberData memberdata = new MemberData();

        for (int i = 0; i < area.transform.childCount; i++)
        {
            Buttons.Add(area.transform.GetChild(i).gameObject);
        }

        // プレハブを元にオブジェクトを生成する
        for (int i = 0; i < memberdata.getNumberOfMember() ; i++)
        {
            Buttons[i].transform.Find("MemberName").GetComponent<Text>().text = memberdata.getMemberName(i);

            // wwwクラスのコンストラクタに画像URLを指定
            UnityWebRequest WebImage = UnityWebRequestTexture.GetTexture(memberdata.getMemberImageURL(i));
            //WWW WebImage = new WWW(memberdata.getMemberImageURL(i));

            // 画像ダウンロード完了を待機
            yield return WebImage.SendWebRequest();

            if (WebImage.isNetworkError || WebImage.isHttpError)
            {
                print(WebImage.error);
            }

            // webサーバから取得した画像をRaw Imagで表示する
            Buttons[i].GetComponent<RawImage>().texture = ((DownloadHandlerTexture)WebImage.downloadHandler).texture;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

