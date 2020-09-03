using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class BlogViewDisplay : MonoBehaviour
{
    private string url = "http://blog.nanabunnonijyuuni.com/s/n227/diary/blog/list";
    WebViewObject webViewObject;

    private GameObject[] MemberNameDisplay;
    private GameObject[] BlogTitlePanel;
    private GameObject[] BottomNavigationBar;
    public Transform parent;

    MemberData memberdata = new MemberData();

    void Start()
    {
        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init((msg) => {
            Debug.Log(msg);
        });
        webViewObject.LoadURL(url);

        BlogTitlePanel = GameObject.FindGameObjectsWithTag("BlogTitlePanel");
        float topMargin = BlogTitlePanel[0].GetComponent<RectTransform>().sizeDelta.y;

        Debug.Log("topMargin:" + topMargin);

        BottomNavigationBar = GameObject.FindGameObjectsWithTag("BottomNavigationBar");
        float bottomMargin = BottomNavigationBar[0].GetComponent<RectTransform>().sizeDelta.y * BottomNavigationBar[0].GetComponent<RectTransform>().localScale.y;

        Debug.Log("bottomMargin:" + bottomMargin);

        webViewObject.SetMargins(0, (int)topMargin, 0, (int)bottomMargin);
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

            MemberNameDisplay[0].GetComponent<Text>().text = memberdata.getMemberName(num);
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