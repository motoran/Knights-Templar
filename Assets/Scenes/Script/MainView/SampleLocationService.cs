using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using muniCdAdapter;

public class SampleLocationService : MonoBehaviour
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
        // Androidの場合、位置情報取得許可がされていない場合、この処理でダイアログが出る
        Input.location.Start();

        // Userが位置情報取得を拒否した場合
        if (!Input.location.isEnabledByUser)
        {
            locationInformationText.text = "位置情報の取得が許可されていません";
            yield break;
        }
        
        // 位置情報取得を許可した瞬間はlocation serviceの稼働状態がStoppedのままの場合があるため以下で再度動かす
        // https://qiita.com/hirano/items/dde92f4ed76fb377746e#android-2
        if (Input.location.status == LocationServiceStatus.Stopped)
        {
            Input.location.Start();
        }
    
        // 初期化が終了するまで待つ
        int maxWait = 20; // タイムアウトは20秒
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1); // 1秒待つ
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
            // エラーが起きた場合はエラー内容を表示
            locationInformationText.text = "hogehogeな訳がねぇ";
        }
        else
        {
            LocationResult location_result = JsonUtility.FromJson<LocationResult>(request.downloadHandler.text);

            string muniCd = Adapter.Convert_Code_Landname((string)location_result.results.muniCd);
            string lv01Nm = (string)location_result.results.lv01Nm;
            
            // レスポンスをテキストで表示
            locationInformationText.text = muniCd + lv01Nm;
        }
        // 位置の更新を継続的に取得する必要がない場合はサービスを停止する
        Input.location.Stop();
    }
}