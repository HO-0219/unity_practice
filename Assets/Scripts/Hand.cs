using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;
    SpriteRenderer player;
    Vector3 rigthPos = new Vector3(0.282f ,-0.187f, 0f);
    Vector3 rigthPosRevers = new Vector3(-0.282f ,-0.187f, 0f);
    Quaternion leftRot = Quaternion.Euler(0,0,-35);
    Quaternion leftRotRevers = Quaternion.Euler(0,0,-135);
    
    void Awake(){
        player = GetComponentsInParent<SpriteRenderer>()[1];


    }
    void LateUpdate() {
        bool isReverse = player.flipX;
        if(isLeft){
            transform.localRotation = isReverse ? leftRotRevers : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 :  6 ;
        }else{
              transform.localPosition = isReverse ? rigthPosRevers : rigthPos;
                spriter.flipX = isReverse;
                spriter.sortingOrder = isReverse ? 6 :  4 ;
        } 
    }
}   
