using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}


public class NotesManager : MonoBehaviour
{
    public int noteNum; // 総ノーツ数
    private string songName; // 曲名

    public List<int> LaneNum = new List<int>(); // どこのレーンに落ちるか
    public List<int> NoteType = new List<int>(); // どんなノーツか
    public List<float> NotesTime = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject noteObj;

    void OnEnable() // オブジェクトが有効化されるときに発火
    {
        noteNum = 0; // 総ノーツ数を0に
        songName = "Calamity Fortune"; // 曲名取得
        Load(songName); // Load()を呼ぶ
    }

    private void Load(string SongName)
    {
        // jsonの読み込み
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float sectionDuration = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = sectionDuration * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
