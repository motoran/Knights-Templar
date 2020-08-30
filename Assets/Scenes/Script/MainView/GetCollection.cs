using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;


//参考 : https://qiita.com/fukaken5050/items/9619aeeb131120939bc1

public class GetCollection : MonoBehaviour
{
    public GameObject getCollectionToast;

    void Start()
    {
    }

    void Update()
    {
    }

    public void GetCollectionToast()
    {
        getCollectionToast.SetActive(true);
        Invoke("ToastSetActiveFalse", 2.0f);
    }

    private void ToastSetActiveFalse()
    {
        getCollectionToast.SetActive(false);
    }


}