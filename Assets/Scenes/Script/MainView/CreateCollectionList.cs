using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCollectionList : MonoBehaviour
{

    private GameObject[] ArObj;
    public Text text;

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
                text.text = PlayerPrefs.GetString(view.name);
            }
        }

    }
}
