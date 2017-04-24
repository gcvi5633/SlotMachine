using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour {

    public GameObject[] slotObject;

    public void GetData() {
        StartCoroutine(WaitWWW());
    }

    IEnumerator WaitWWW() {
        //發送Web的參數時可以使用下面的WWWForm來包裝
        WWWForm form = new WWWForm();
        form.AddField("METHOD","spin");
        form.AddField("PARAMS","test");

        UnityWebRequest www = UnityWebRequest.Post("http://test2.3-pts.com/unityexam/getroll.php",form);

        yield return www.Send();

        #region 測試Web資料傳輸是否有誤，若是沒有則下載回傳資料
        //if(www.isError) {
        //    Debug.Log(www.error);
        //} else {
        //    Debug.Log("Data: " + www.downloadHandler.text);
        //}
        #endregion

        var json = www.downloadHandler.text;

        RequestData jsonString =  JsonUtility.FromJson<RequestData>(json);//解析Json資料

        //將取出的資料交給各個SlotObject
        for(int i = 0; i < slotObject.Length; i++) {
           StartCoroutine(slotObject[i].GetComponent<SlotManager>().WaitRoll(jsonString.CURRENT_ROLL[i]));
        }
    }
}

//Json格式
class RequestData {
    public int STATUS;
    public string MSG;
    public string[] CURRENT_ROLL;
}
