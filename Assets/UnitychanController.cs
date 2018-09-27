using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanController: MonoBehaviour {
    //アニメーションするためのコンポーネントを入れる
    Animator animator;
    //Unityちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    //地面の位置
    private float groundLevel = -3.0f;

    //ジャンプの速度の減衰
    private float dump = 0.8f;

    //ジャンプの速度
    float jumpVelocity = 20;

    //ゲームオーバーになる位置
    private float deadline = -9;

	// Use this for initialization
	void Start () {
        //アニメーターのコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        //Rigidbody2Dのコンポーネントを取得する
        this.rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //走るアニメーションを再生するために、Animatorのパラメーターを調節
        this.animator.SetFloat("Horizontal", 1);

        //着地しているかどうか調べる
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        this.animator.SetBool("isGround",isGround);

        //ジャンプ状態の時はボリュームを0にする
        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0;

        //着地状態でクリックされた場合
        if (Input.GetMouseButtonDown(0) && isGround) {
            //上方向の力を加える
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
        }

        //クリックを辞めたら上方向への速度を減衰させる
        if (Input.GetMouseButton(0) == false) {
            if (this.rigid2D.velocity.y > 0) {
                this.rigid2D.velocity *= this.dump;
            }
        }

        //deadlineを超えた場合、ゲームオーバーにする
        if (transform.position.x < this.deadline) {
            //UIControllerのGameOver関数を呼び出し、画面上に「Game　Over」と表示
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            //Unityちゃんを破棄する
            Destroy(gameObject);
        }
	}
}
