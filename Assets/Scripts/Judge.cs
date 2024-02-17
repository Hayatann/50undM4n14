using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Judge : MonoBehaviour
{
    //変数の宣言
    [SerializeField] private GameObject[] MessageObj;//プレイヤーに判定を伝えるゲームオブジェクト
    [SerializeField] NotesManager notesManager;//スクリプト「notesManager」を入れる変数

    [SerializeField] private TextMeshProUGUI comboText;

    [SerializeField] private TextMeshProUGUI scoreText;

    private AudioSource audio;

    [SerializeField] private AudioClip hitSound;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.Start)
        {
            Debug.Log(notesManager.NotesTime0);
            
            // Dキーの処理 (レーン0)
            if (Input.GetKeyDown(KeyCode.D))
            {
                Judgement(GetABS(Time.time - (notesManager.NotesTime0[0] + GManager.instance.StartTime)), 0);
            }

            // Fキーの処理 (レーン1)
            if (Input.GetKeyDown(KeyCode.F))
            {
                Judgement(GetABS(Time.time - (notesManager.NotesTime1[0] + GManager.instance.StartTime)), 1);
            }

            // Spaceキーの処理 (レーン2)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Judgement(GetABS(Time.time - (notesManager.NotesTime2[0] + GManager.instance.StartTime)), 2);
            }

            // Jキーの処理 (レーン3)
            if (Input.GetKeyDown(KeyCode.J))
            {
                Judgement(GetABS(Time.time - (notesManager.NotesTime3[0] + GManager.instance.StartTime)), 3);
            }

            // Kキーの処理 (レーン4)
            if (Input.GetKeyDown(KeyCode.K))
            {
                Judgement(GetABS(Time.time - (notesManager.NotesTime4[0] + GManager.instance.StartTime)), 4);
            }

            if (Time.time > notesManager.NotesTime0[0] + 0.16f + GManager.instance.StartTime)//本来ノーツをたたくべき時間から160msたっても入力がなかった場合
            {
                message(3);
                deleteData(0);
                Debug.Log("Dust");
                GManager.instance.dust++;
                GManager.instance.combo = 0;
                //ミス
            }
            if (Time.time > notesManager.NotesTime1[0] + 0.16f + GManager.instance.StartTime)//本来ノーツをたたくべき時間から160msたっても入力がなかった場合
            {
                message(3);
                deleteData(1);
                Debug.Log("Dust");
                GManager.instance.dust++;
                GManager.instance.combo = 0;
                //ミス
            }
            if (Time.time > notesManager.NotesTime2[0] + 0.16f + GManager.instance.StartTime)//本来ノーツをたたくべき時間から160msたっても入力がなかった場合
            {
                message(3);
                deleteData(2);
                Debug.Log("Dust");
                GManager.instance.dust++;
                GManager.instance.combo = 0;
                //ミス
            }
            if (Time.time > notesManager.NotesTime3[0] + 0.16f + GManager.instance.StartTime)//本来ノーツをたたくべき時間から160msたっても入力がなかった場合
            {
                message(3);
                deleteData(3);
                Debug.Log("Dust");
                GManager.instance.dust++;
                GManager.instance.combo = 0;
                //ミス
            }
            if (Time.time > notesManager.NotesTime4[0] + 0.16f + GManager.instance.StartTime)//本来ノーツをたたくべき時間から160msたっても入力がなかった場合
            {
                message(3);
                deleteData(4);
                Debug.Log("Dust");
                GManager.instance.dust++;
                GManager.instance.combo = 0;
                //ミス
            }
        }
    }
    void Judgement(float timeLag, int laneNum)
    {
        // audio.PlayOneShot(hitSound);
        if (timeLag <= 0.05)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が50ms以下だったら
        {
            Debug.Log("Stellar Crystal");
            message(0);
            GManager.instance.ratioScore += 5;
            GManager.instance.stellar_crystal++;
            GManager.instance.combo++;
            deleteData(laneNum);
        }
        else
        {
            if (timeLag <= 0.08)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が80ms以下だったら
            {
                Debug.Log("Stellar");
                message(1);
                GManager.instance.ratioScore += 3;
                GManager.instance.stellar++;
                GManager.instance.combo++;
                deleteData(laneNum);
            }
            else
            {
                if (timeLag <= 0.12)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が120ms以下だったら
                {
                    Debug.Log("Bad");
                    message(2);
                    GManager.instance.ratioScore += 1;
                    GManager.instance.bad++;
                    GManager.instance.combo++;
                    deleteData(laneNum);
                }
            }
        }
    }
    float GetABS(float num)//引数の絶対値を返す関数
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }

    void deleteData(int laneNum)//すでにたたいたノーツを削除する関数
    {
        switch (laneNum)
        {
            case 0:
                Destroy(notesManager.NotesObj0[0]);
                notesManager.NotesTime0.RemoveAt(0);
                notesManager.NoteType0.RemoveAt(0);
                notesManager.NotesObj0.RemoveAt(0);
                break;
            case 1:
                Destroy(notesManager.NotesObj1[0]);
                notesManager.NotesTime1.RemoveAt(0);
                notesManager.NoteType1.RemoveAt(0);
                notesManager.NotesObj1.RemoveAt(0);
                break;
            case 2:
                Destroy(notesManager.NotesObj2[0]);
                notesManager.NotesTime2.RemoveAt(0);
                notesManager.NoteType2.RemoveAt(0);
                notesManager.NotesObj2.RemoveAt(0);
                break;
            case 3:
                Destroy(notesManager.NotesObj3[0]);
                notesManager.NotesTime3.RemoveAt(0);
                notesManager.NoteType3.RemoveAt(0);
                notesManager.NotesObj3.RemoveAt(0);
                break;
            case 4:
                Destroy(notesManager.NotesObj4[0]);
                notesManager.NotesTime4.RemoveAt(0);
                notesManager.NoteType4.RemoveAt(0);
                notesManager.NotesObj4.RemoveAt(0);
                break;
        }
        notesManager.LaneNum.RemoveAt(0);
        GManager.instance.score = (int)Math.Round(1000000 *
            Math.Floor(GManager.instance.ratioScore / GManager.instance.maxScore * 1000000) / 1000000);
        comboText.text = GManager.instance.combo.ToString();
        scoreText.text = GManager.instance.score.ToString();
    }

    void message(int judge)//判定を表示する
    {
        Instantiate(MessageObj[judge],new Vector3(notesManager.LaneNum[0]-1.5f,0.76f,0.15f),Quaternion.Euler(45,0,0));
    }
}
