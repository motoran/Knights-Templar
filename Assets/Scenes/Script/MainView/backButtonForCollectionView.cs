using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButtonForCollectionView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisObj;
    public GameObject CollectionView;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    public void BackCollectionView()
    {
        thisObj.SetActive(false);
        CollectionView.SetActive(true);
    }

}
