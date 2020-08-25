using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtonBlogView : MonoBehaviour
{
    public GameObject obj;
    public Transform parent;
    private GameObject[] instance = new GameObject[11];

    // Start is called before the first frame update
    void Start()
    {
        // プレハブを元にオブジェクトを生成する

        for (int i = 0; i < 11; i++)
        {
            instance[i] = (GameObject)Instantiate(obj);
            instance[i].transform.Find("Text").GetComponent<Text>().text = i.ToString();
            instance[i].transform.SetParent(parent);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
