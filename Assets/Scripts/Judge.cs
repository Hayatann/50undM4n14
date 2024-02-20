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

    private AudioSource audioSource;
    public AudioClip hitSoundStellarCrystal;
    public AudioClip hitSoundStellar;
    public AudioClip hitSoundBad;
    int soundIndex = -1;

    [SerializeField] AudioClip[] hitSounds;
  // Start is called before the first frame update
  void Start()
  {
    audioSource = GetComponent<AudioSource>();

    hitSounds = new AudioClip[] { hitSoundStellarCrystal, hitSoundStellar, hitSoundBad };

    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.Start)
        {
            CheckInputAndJudge(KeyCode.D, notesManager.NotesTime0, 0);
            CheckInputAndJudge(KeyCode.F, notesManager.NotesTime1, 1);
            CheckInputAndJudge(KeyCode.Space, notesManager.NotesTime2, 2);
            CheckInputAndJudge(KeyCode.J, notesManager.NotesTime3, 3);
            CheckInputAndJudge(KeyCode.K, notesManager.NotesTime4, 4);
            // if (Time.time > notesTimes[i] + 0.16f + GManager.instance.StartTime)//本来ノーツをたたくべき時間から160msたっても入力がなかった場合
            // {
            //     message(3);
            //     deleteData(laneNum);
            //     GManager.instance.dust++;
            //     GManager.instance.combo = 0;
            //     //ミス
            // }
            CheckForMissedNotes(notesManager.NotesTime0, 0);
            CheckForMissedNotes(notesManager.NotesTime1, 1);
            CheckForMissedNotes(notesManager.NotesTime2, 2);
            CheckForMissedNotes(notesManager.NotesTime3, 3);
            CheckForMissedNotes(notesManager.NotesTime4, 4);
        }
    }
    void CheckInputAndJudge(KeyCode key, List<float> notesTimes, int laneNum)
    {
        if (Input.GetKeyDown(key))
        {
            for (int i = 0; i < notesTimes.Count; i++)
            {
                soundIndex = -1;
                float timeLag = GetABS(Time.time - (notesTimes[i] + GManager.instance.StartTime));
                if (timeLag <= 0.05) // SomeThresholdは判定の閾値
                {
                    soundIndex = 0;
                    message(0);
                    GManager.instance.ratioScore += 5;
                    GManager.instance.stellar_crystal++;
                    GManager.instance.combo++;
                    audioSource.clip = hitSounds[0];
                    audioSource.Play();
                    deleteData(laneNum);
                    // ここでノーツを削除するロジックを追加
                    break; // 最も近いノーツに対して判定を行ったらループを抜ける
                }
                else if (timeLag <= 0.08)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が80ms以下だったら
                {
                    soundIndex = 1;
                    message(1);
                    GManager.instance.ratioScore += 3;
                    GManager.instance.stellar++;
                    GManager.instance.combo++;
                    audioSource.clip = hitSounds[1];
                    audioSource.Play();
                    deleteData(laneNum);
                    break; // 最も近いノーツに対して判定を行ったらループを抜ける
                }
                else if (timeLag <= 0.12)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が120ms以下だったら
                {
                    soundIndex = 2;
                    message(2);
                    GManager.instance.ratioScore += 1;
                    GManager.instance.bad++;
                    GManager.instance.combo++;
                    audioSource.clip = hitSounds[2];
                    audioSource.Play();
                    deleteData(laneNum);
                    break; // 最も近いノーツに対して判定を行ったらループを抜ける
                }
            }
        }
    }
    void CheckForMissedNotes(List<float> notesTimes, int laneNum)
    {
        while (notesTimes.Count > 0 && Time.time > notesTimes[0] + GManager.instance.StartTime + 0.16f)
        {
            // 最初のノーツが見逃された場合、ミスとして処理
            message(3); // ミスのメッセージを表示（message関数を適宜調整してください）
            deleteData(laneNum);
            GManager.instance.dust++;
            GManager.instance.combo = 0;
            // コンボをリセット
        }
    }
  void Judgement(float timeLag, int laneNum)
  {
    soundIndex = -1;
    if (timeLag <= 0.05)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が50ms以下だったら
    {
      soundIndex = 0;
      message(0);
      GManager.instance.ratioScore += 5;
      GManager.instance.stellar_crystal++;
      GManager.instance.combo++;
      deleteData(laneNum);
    }
    else if (timeLag <= 0.08)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が80ms以下だったら
      {
        soundIndex = 1;
        message(1);
        GManager.instance.ratioScore += 3;
        GManager.instance.stellar++;
        GManager.instance.combo++;
        deleteData(laneNum);
      }
      else if (timeLag <= 0.12)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が120ms以下だったら
        {
          soundIndex = 2;
          message(2);
          GManager.instance.ratioScore += 1;
          GManager.instance.bad++;
          GManager.instance.combo++;
          deleteData(laneNum);
        }
    if (soundIndex != -1)
    {
      audioSource.clip = hitSounds[soundIndex];
      audioSource.Play();
    }
    else
    {
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
