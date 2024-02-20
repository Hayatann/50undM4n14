using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

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
    public List<float> NotesHoldTime0 = new List<float>(); // ホールドノーツ持続時間
    public List<GameObject> NotesObj0 = new List<GameObject>();
    public List<int> NoteType1 = new List<int>(); // どんなノーツか
    public List<float> NotesTime1 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<float> NotesHoldTime1 = new List<float>(); // ホールドノーツ持続時間
    public List<GameObject> NotesObj1 = new List<GameObject>();
    public List<int> NoteType2 = new List<int>(); // どんなノーツか
    public List<float> NotesTime2 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<float> NotesHoldTime2 = new List<float>(); // ホールドノーツ持続時間
    public List<GameObject> NotesObj2 = new List<GameObject>();
    public List<int> NoteType3 = new List<int>(); // どんなノーツか
    public List<float> NotesTime3 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<float> NotesHoldTime3 = new List<float>(); // ホールドノーツ持続時間
    public List<GameObject> NotesObj3 = new List<GameObject>();
    public List<int> NoteType4 = new List<int>(); // どんなノーツか
    public List<float> NotesTime4 = new List<float>(); // ノーツと判定線が重なる瞬間
    public List<float> NotesHoldTime4 = new List<float>(); // ホールドノーツ持続時間
    public List<GameObject> NotesObj4 = new List<GameObject>();

    [SerializeField] private float NotesSpeed;
    [FormerlySerializedAs("noteObj")] [SerializeField] GameObject tapNoteObj;
    [SerializeField] GameObject longNoteObj;

    private void Start()
    {
        Renderer rend = tapNoteObj.GetComponent<Renderer>();
        Vector3 size = rend.bounds.size;
        float distanceToBottom = size.z / 2;
        Debug.Log($"distance: {distanceToBottom}");
    }

    void OnEnable() // オブジェクトが有効化されるときに発火
    {
        Debug.Log(GManager.instance);
        NotesSpeed = 8;
        noteNum = 0; // 総ノーツ数を0に
        songName = "Calamity Fortune"; // 曲名取得
        Load(songName); // Load()を呼ぶ
    }
    public void ScaleAround(GameObject target, Vector3 pivot, Vector3 newScale)
    {
        // pivot を中心に、target のScaleを変化させる
        Vector3 targetPos = target.transform.localPosition;
        Vector3 diff = targetPos - pivot;
        float relativeScale = newScale.x / target.transform.localScale.x;

        Vector3 resultPos = pivot + diff * relativeScale;
        target.transform.localScale = newScale;
        target.transform.localPosition = resultPos;
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
                    if (inputJson.notes[i].type == 2)
                    {
                        float startTime = time;
                        float endSectionDuration = 60f / (inputJson.BPM * (float)inputJson.notes[i].notes[0].LPB);
                        float endBeatSec = endSectionDuration * (float)inputJson.notes[i].notes[0].LPB;
                        float endTime = (endBeatSec * inputJson.notes[i].notes[0].num / (float)inputJson.notes[i].notes[0].LPB) + inputJson.offset * 0.01f;
                        NotesHoldTime0.Add(endTime - startTime);
                        // Debug.Log($"HoldTime: {endTime - startTime}");
                        // ScaleAround(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.5f, NotesTime0[index0] * NotesSpeed),
                        //     Quaternion.identity), new Vector3(0, 0, -0.175f), new Vector3(1, 0.01f, 4));
                    }
                    z = NotesTime0[index0] * NotesSpeed;
                    NotesObj0.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.5f, z), Quaternion.identity));
                    index0++;
                    break;
                case 1:
                    NotesTime1.Add(time);
                    NoteType1.Add(inputJson.notes[i].type);
                    if (inputJson.notes[i].type == 2)
                    {
                        float startTime = time;
                        float endSectionDuration = 60f / (inputJson.BPM * (float)inputJson.notes[i].notes[0].LPB);
                        float endBeatSec = endSectionDuration * (float)inputJson.notes[i].notes[0].LPB;
                        float endTime = (endBeatSec * inputJson.notes[i].notes[0].num / (float)inputJson.notes[i].notes[0].LPB) + inputJson.offset * 0.01f;
                        NotesHoldTime1.Add(endTime - startTime);
                    }
                    z = NotesTime1[index1] * NotesSpeed;
                    NotesObj1.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.5f, z), Quaternion.identity));
                    index1++;
                    break;
                case 2:
                    NotesTime2.Add(time);
                    NoteType2.Add(inputJson.notes[i].type);
                    if (inputJson.notes[i].type == 2)
                    {
                        float startTime = time;
                        float endSectionDuration = 60f / (inputJson.BPM * (float)inputJson.notes[i].notes[0].LPB);
                        float endBeatSec = endSectionDuration * (float)inputJson.notes[i].notes[0].LPB;
                        float endTime = (endBeatSec * inputJson.notes[i].notes[0].num / (float)inputJson.notes[i].notes[0].LPB) + inputJson.offset * 0.01f;
                        NotesHoldTime2.Add(endTime - startTime);
                    }
                    z = NotesTime2[index2] * NotesSpeed;
                    NotesObj2.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.5f, z), Quaternion.identity));
                    index2++;
                    break;
                case 3:
                    NotesTime3.Add(time);
                    NoteType3.Add(inputJson.notes[i].type);
                    if (inputJson.notes[i].type == 2)
                    {
                        float startTime = time;
                        float endSectionDuration = 60f / (inputJson.BPM * (float)inputJson.notes[i].notes[0].LPB);
                        float endBeatSec = endSectionDuration * (float)inputJson.notes[i].notes[0].LPB;
                        float endTime = (endBeatSec * inputJson.notes[i].notes[0].num / (float)inputJson.notes[i].notes[0].LPB) + inputJson.offset * 0.01f;
                        NotesHoldTime3.Add(endTime - startTime);
                    }
                    z = NotesTime3[index3] * NotesSpeed;
                    NotesObj3.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.5f, z), Quaternion.identity));
                    index3++;
                    break;
                case 4:
                    NotesTime4.Add(time);
                    NoteType4.Add(inputJson.notes[i].type);
                    if (inputJson.notes[i].type == 2)
                    {
                        float startTime = time;
                        float endSectionDuration = 60f / (inputJson.BPM * (float)inputJson.notes[i].notes[0].LPB);
                        float endBeatSec = endSectionDuration * (float)inputJson.notes[i].notes[0].LPB;
                        float endTime = (endBeatSec * inputJson.notes[i].notes[0].num / (float)inputJson.notes[i].notes[0].LPB) + inputJson.offset * 0.01f;
                        NotesHoldTime4.Add(endTime - startTime);
                    }
                    z = NotesTime4[index4] * NotesSpeed;
                    NotesObj4.Add(Instantiate(tapNoteObj, new Vector3(inputJson.notes[i].block - 1.0f, 0.5f, z), Quaternion.identity));
                    index4++;
                    break;
            }
            LaneNum.Add(inputJson.notes[i].block);
        }
    }
}
