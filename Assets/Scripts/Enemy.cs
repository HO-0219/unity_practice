using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxhealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    bool isLive;
    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriter; 
    Animator anim;
    WaitForFixedUpdate wait;

    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    void FixedUpdate() {
        if(!GameManager.instance.isLive)
            return; 
                
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec); 
        rigid.velocity = Vector2.zero;

    }
    void LateUpdate(){
        if(!GameManager.instance.isLive)
            return;
            
        if(!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }
    void OnEnable() {
        isLive = true;
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead",false);
        health = maxhealth;

    }
    public void Init(SpawnData spawnData){
       anim.runtimeAnimatorController =  animCon[spawnData.spriterType]; 
        speed = spawnData.speed;
        maxhealth = spawnData.health;
        health = spawnData.health;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Bullet") || !isLive)
            return;
        
        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health > 0 ){
            anim.SetTrigger("Hit");
            //live and action
        }else{
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead",true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

        }

    }

    IEnumerator KnockBack(){

        yield return wait;  //1프레임 물리 프레임을 딜레이 준다 
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
    void Dead(){
        gameObject.SetActive(false);
        
    }

}