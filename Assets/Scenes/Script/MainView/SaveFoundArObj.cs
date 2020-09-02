using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class SaveFoundArObj : MonoBehaviour
{

    public GameObject ArObj;
    public Text location;
    public Text buttonText;
    public GameObject getCollectionToast;
    public RawImage jito;
    public Texture jitoImage;


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

            string LocationInfo = location.text.ToString();
            buttonText.text = LocationInfo;
            PlayerPrefs.SetString("Location:" + ArObj.name, LocationInfo);

            PlayerPrefs.Save();

            jito.texture = jitoImage;

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
