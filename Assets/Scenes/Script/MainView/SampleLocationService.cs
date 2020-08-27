using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using MiniJSON;

public class SampleLocationService : MonoBehaviour
{
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
        else
        {
            // アクセスの許可と位置情報の取得に成功
            locationInformationText.text = "緯度" + Input.location.lastData.latitude + 
                                           "経度" + Input.location.lastData.longitude;
        }
        
        WWW results = new WWW("http://www.finds.jp/ws/rgeocode.php?json&lat=" + Input.location.lastData.latitude + "&lon=" + Input.location.lastData.longitude); 
        var search  = Json.Deserialize(results.text) as IDictionary;
        var result = search["result"] as IDictionary;
        var prefecture = result["prefecture"] as IDictionary;
        var municipality = result["municipality"] as IDictionary;
        string currentPosition = prefecture["pname"] as string + municipality["mname"] as string;
        locationInformationText.text = currentPosition;
        // 位置の更新を継続的に取得する必要がない場合はサービスを停止する
        Input.location.Stop();
    }
}