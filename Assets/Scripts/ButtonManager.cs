using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Text btnText;

    bool scrolling = false;

    void Awake() {
        scrolling = false;
    }

    void Update() {
        //檢測其中一個Slot的Animation，因為是全部一起動作所以偵測一個即可
        //若是Animation的Name是 "Base Layer.Idle"，會改變按鈕的名稱，以及改變scroll的狀態
        if(GetComponent<WebRequest>().slotObject[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).
            IsName("Base Layer.Idle")) {
            btnText.text = "Spin";
            scrolling = false;
        } else {
            btnText.text = "Stop!";
            scrolling = true;
        }
    }

    public void PressButton() {
        if(scrolling) {
            for(int i = 0; i < GetComponent<WebRequest>().slotObject.Length; i++) {
                GetComponent<WebRequest>().slotObject[i].GetComponent<SlotManager>().StopScroll();
            }
        } else if(!scrolling) {
            GetComponent<WebRequest>().GetData();
        }
    }
}
