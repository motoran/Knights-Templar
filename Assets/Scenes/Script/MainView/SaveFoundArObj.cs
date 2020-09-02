using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SaveFoundArObj : MonoBehaviour
{
    public GameObject ArObj;
    public GameObject getCollectionToast;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveArObj()
    {
        if (!PlayerPrefs.HasKey(ArObj.name))
        {
            PlayerPrefs.SetString(ArObj.name, ArObj.name);
            PlayerPrefs.Save();

            GameObject LocationInfoController = GameObject.Find("MainView_Canvas");
            string LocationInfo = LocationInfoController.GetComponent<LocationInfoController>().LocationInfoGetter();
            PlayerPrefs.SetString("Location",LocationInfo);
            PlayerPrefs.Save();
            
            GetCollectionToast();
        }
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
