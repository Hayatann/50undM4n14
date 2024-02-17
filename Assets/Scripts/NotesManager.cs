using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
    public Note[] notes;
}



public class NotesManager : MonoBehaviour
{
    public int noteNum; // 総ノーツ数
    private string songName; // 曲名

    public List<int> LaneNum = new List<int>(); // どこのレーンに落ちるか
    public List<int> NoteType0 = new List<int>(); // どんなノーツか
    public List<float> NotesTime0 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<GameObject> NotesObj0 = new List<GameObject>();
    public List<int> NoteType1 = new List<int>(); // どんなノーツか
    public List<float> NotesTime1 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<GameObject> NotesObj1 = new List<GameObject>();
    public List<int> NoteType2 = new List<int>(); // どんなノーツか
    public List<float> NotesTime2 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<GameObject> NotesObj2 = new List<GameObject>();
    public List<int> NoteType3 = new List<int>(); // どんなノーツか
    public List<float> NotesTime3 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<GameObject> NotesObj3 = new List<GameObject>();
    public List<int> NoteType4 = new List<int>(); // どんなノーツか
    public List<float> NotesTime4 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<GameObject> NotesObj4 = new List<GameObject>();

    [SerializeField] private float NotesSpeed;
    [FormerlySerializedAs("noteObj")] [SerializeField] GameObject tapNoteObj;
    [SerializeField] GameObject longNoteObj;

    void OnEnable() // オブジェクトが有効化されるときに発火
    {
        Debug.Log(GManager.instance);
        NotesSpeed = 8;
        noteNum = 0; // 総ノーツ数を0に
        songName = "Calamity Fortune"; // 曲名取得
        Load(songName); // Load()を呼ぶ
    }

    private void Load(string SongName)
    {
        // jsonの読み込み
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);
        Debug.Log(inputJson);

        noteNum = inputJson.notes.Length;
        GManager.instance.maxScore = noteNum * 5;
        int index0 = 0;
        int index1 = 0;
        int index2 = 0;
        int index3 = 0;
        int index4 = 0;
        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float sectionDuration = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = sectionDuration * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            float z;
            switch (inputJson.notes[i].block)
            {
                case 0:
                    NotesTime0.Add(time);
                    NoteType0.Add(inputJson.notes[i].type);
                    z = NotesTime0[index0] * NotesSpeed;
                    NotesObj0.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.55f, z), Quaternion.identity));
                    index0++;
                    break;
                case 1:
                    NotesTime1.Add(time);
                    NoteType1.Add(inputJson.notes[i].type);
                    z = NotesTime1[index1] * NotesSpeed;
                    NotesObj1.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.55f, z), Quaternion.identity));
                    index1++;
                    break;
                case 2:
                    NotesTime2.Add(time);
                    NoteType2.Add(inputJson.notes[i].type);
                    z = NotesTime2[index2] * NotesSpeed;
                    NotesObj2.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.55f, z), Quaternion.identity));
                    index2++;
                    break;
                case 3:
                    NotesTime3.Add(time);
                    NoteType3.Add(inputJson.notes[i].type);
                    z = NotesTime3[index3] * NotesSpeed;
                    NotesObj3.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.55f, z), Quaternion.identity));
                    index3++;
                    break;
                case 4:
                    NotesTime4.Add(time);
                    NoteType4.Add(inputJson.notes[i].type);
                    z = NotesTime4[index4] * NotesSpeed;
                    NotesObj4.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.55f, z), Quaternion.identity));
                    index4++;
                    break;
            }
            LaneNum.Add(inputJson.notes[i].block);
        }
    }
}
