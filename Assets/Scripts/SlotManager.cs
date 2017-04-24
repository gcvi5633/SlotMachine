using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour {

    Animator _animator;

    public RawImage mainSlotItem;

    public RawImage[] otherSlotItem;
    public Texture[] slotPicture;
    Texture image;

    bool startScroll;
    bool endScroll;

    void Awake() {
        _animator = GetComponent<Animator>();
        ChangeSlotPicture();
    }

    void Update() {
        //隨時檢查Animation的參數
        _animator.SetBool("Start",startScroll);
        _animator.SetBool("End",endScroll);
    }

    public IEnumerator WaitRoll(string slotName) {
        startScroll = true;
        endScroll = false;
        ChangeMainSlotPic(slotName);
        ChangeSlotPicture();
        yield return new WaitForSeconds(1f);
        startScroll = false;
        endScroll = true;
    }

    //將其他的Slot加入隨機圖片
    void ChangeSlotPicture() {
        for(int i = 0; i < otherSlotItem.Length; i++) {
            var randomInt = Random.Range(0,slotPicture.Length);
            otherSlotItem[i].GetComponent<RawImage>().texture = slotPicture[randomInt];
        }
    }

    //將主要的Slot加上目標圖片
    public void ChangeMainSlotPic(string slot) {
        image = Resources.Load(slot) as Texture;
        mainSlotItem.GetComponent<RawImage>().texture = image;
    }

    //可以直接結束轉動並顯示結果
    public void StopScroll() {
        StopAllCoroutines();
        startScroll = false;
        endScroll = true;
    }
}
