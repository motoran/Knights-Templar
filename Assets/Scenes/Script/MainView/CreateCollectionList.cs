using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCollectionList : MonoBehaviour
{

    private GameObject[] ArObj;
    public Text name;
    public Text location;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ArObj = GameObject.FindGameObjectsWithTag("ArObj");
        foreach (GameObject view in this.ArObj)
        {
            if (PlayerPrefs.HasKey(view.name))
            {
                name.text = PlayerPrefs.GetString(view.name);
                location.text = PlayerPrefs.GetString("Location:" + view.name);
            }
        }

    }
}