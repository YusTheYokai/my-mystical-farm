using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Animal
{

    public SpriteRenderer cow;
    public Sprite[] cowForms;
    int cowForm = 0;

    private void OnMouseDown() {
        Debug.Log("clicked");
        ChangeCowForm();
    }

    void ChangeCowForm() {
        if (cowForm == 0){
            UpdateCow();
        }
    }

    void UpdateCow(){
        cow.sprite = cowForms[1];
    }
}
