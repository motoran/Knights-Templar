using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class BlogViewDisplay : MonoBehaviour
{
    private string url = "https://www.google.co.jp";
    WebViewObject webViewObject;

    private GameObject[] MemberNameDisplay;
    public Transform parent;

    MemberData memberdata = new MemberData();

    void Start()
    {
        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init((msg) => {
            Debug.Log(msg);
        });
        webViewObject.LoadURL(url);
        webViewObject.SetMargins(0, 200, 0, Screen.height / 4);
        webViewObject.SetVisibility(false);
        webViewObject.transform.SetParent(parent);
    }

    public void OnClick(int num)
    {
        MemberNameDisplay = GameObject.FindGameObjectsWithTag("MemberNameDisplay");

        if (num < 11)
        {
            string url = memberdata.getMemberSource(num);
            Debug.Log(url);

            MemberNameDisplay[0].GetComponent<Text>().text = memberdata.getMemberName(num) + "\n" + url;
            webViewObject.LoadURL(url);
            webViewObject.SetVisibility(true);
        }
        else
        {
            Debug.Log("back");
            InitWebView();
            return;
        }
        
    }

    public void InitWebView()
    {
        webViewObject.SetVisibility(false);
        MemberNameDisplay = GameObject.FindGameObjectsWithTag("MemberNameDisplay");
        MemberNameDisplay[0].GetComponent<Text>().text = "Member";
    }
}