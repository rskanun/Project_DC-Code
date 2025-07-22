using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string version;
    public DateTime regdate;
    public SaveChapterData chapterData;
    public SavePositionData positionData;
    public SaveQuestData questData;

    public SaveData()
    {
        version = SaveFileInfo.Instance.version;
        regdate = DateTime.Now;
    }
}

[Serializable]
public class SaveChapterData
{
    public int chapter;
    public int root;
    public int subChapter;
}

[Serializable]
public class SavePositionData
{
    public string mapID;
    public Vector2 position;
}

[Serializable]
public class SaveQuestData
{
    public int currentQuest;
    public List<int> completeQuests;
}