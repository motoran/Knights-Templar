using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BottomNavigationBarController : MonoBehaviour
{
    private GameObject[] view;

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
        ScreenTransition(viewType[num]);
    }

}
