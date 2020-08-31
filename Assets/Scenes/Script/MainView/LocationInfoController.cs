using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using muniCdAdapter;

public class LocationInfoController : MonoBehaviour
{
    [Serializable]
    public class LocationResult
    {
        public LocationInfo results;
    }

    [Serializable]
    public class LocationInfo
    {
        public string muniCd;
        public string lv01Nm;
    }

    public Text locationInformationText;

    IEnumerator Start()
    {
        if (Input.location.isEnabledByUser == false)
        {
            // 位置情報取得許可が出されていない場合、この処理でダイアログを出すことが出来る
            Input.location.Start();
            if (Input.location.isEnabledByUser == false)
            {
                locationInformationText.text = "位置情報取得が許可されていません";
                yield break;
            }
        }

        // 位置情報取得許可が得られた瞬間のStart処理はisEnabledByUserをtrueにするだけ
        // https://qiita.com/hirano/items/dde92f4ed76fb377746e#android-2
        if (Input.location.status == LocationServiceStatus.Stopped)
        {
            Input.location.Start();
        }
    
        int maxWait = 21;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            switch( maxWait%3 )
            {
                case 0:
                    locationInformationText.text = "位置情報取得中.";
                    break;
                case 1:
                    locationInformationText.text = "位置情報取得中..";
                    break;
                case 2:
                    locationInformationText.text = "位置情報取得中...";
                    break;
                default:
                    locationInformationText.text = "位置情報取得中";
                    break;
            }
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // サービスの開始がタイムアウトしたら（20秒以内に起動しなかったら）、終了
        if (maxWait < 1)
        {
            locationInformationText.text = "タイムアウトによる位置情報取得エラー";
            yield break;
        }

        // サービスの開始に失敗したら終了
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            locationInformationText.text = "何かしらの理由による位置情報取得エラー";
            yield break;
        }

        string url = "https://mreversegeocoder.gsi.go.jp/reverse-geocoder/LonLatToAddress?lat=" + Input.location.lastData.latitude + "&lon=" + Input.location.lastData.longitude;
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            locationInformationText.text = "apiエラー";
        }
        else
        {
            LocationResult location_result = JsonUtility.FromJson<LocationResult>(request.downloadHandler.text);

            string muniCd = Adapter.Convert_Code_Landname((string)location_result.results.muniCd);
            string lv01Nm = (string)location_result.results.lv01Nm;
            
            locationInformationText.text = muniCd + lv01Nm;
        }

        // 1分毎に位置情報更新
        yield return new WaitForSeconds(60);
    }
}
