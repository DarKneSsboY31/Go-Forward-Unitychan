using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    //SEを定義、SEにアタッチされたモノを流すようにする
    public AudioClip SE;
    public float volume;
    
    //キューブの移動速度
    private float speed = -0.2f;

    //消滅位置
    private float deadline = -10;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        //キューブを移動させる
        transform.Translate(this.speed, 0, 0);

        //画面外に出たら破棄する
        if (transform.position.x < this.deadline) {
            Destroy(gameObject);
        }
	}
    //タグが「TouchBlock」のものに当たったら、音を鳴らす
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "TouchBlock")
        {
            GetComponent<AudioSource>().PlayOneShot(SE, volume);
        }
    }
}
