using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class BottomNavigationBarController : MonoBehaviour
{
    private GameObject[] view;
    public GameObject arCam;
    public GameObject mainCam;

    WebViewObject webViewObject;

    private string[] viewType =
    {
        "BlogView_Panel",
        "ARView_Panel",
        "CollectionView_Panel",
        "hogeView_Panel"
    };

    // Start is called before the first frame update
    void Start()
    {
        view = GameObject.FindGameObjectsWithTag("View");
        ScreenTransition(viewType[0]);
        arCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ScreenTransition(string selectedView)
    {
        foreach (GameObject view in this.view)
        {
            view.SetActive((view.name == selectedView));
        }
    }

    public void OnClick(int num)
    {
        arCam.SetActive(num == 1);
        mainCam.SetActive(num != 1);
        ScreenTransition(viewType[num]);
    }

}
