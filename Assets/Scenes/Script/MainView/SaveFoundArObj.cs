using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SaveFoundArObj : MonoBehaviour
{

    public GameObject ArObj;

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
        }
    }

}
