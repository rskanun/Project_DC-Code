using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public SaveHeader header;
    public SaveBody body;

    public SaveData()
    {
        header = new SaveHeader();
        body = new SaveBody();
    }
}

public class SaveHeader
{
    public string version;
    public DateTime regdate;
}

public class SaveBody
{
    public SaveChapterData chapterData;
    public SavePositionData positionData;
    public SaveQuestData questData;
}

public class SaveChapterData
{
    public int chapter;
    public int root;
    public int subChapter;
}

public class SavePositionData
{
    public string mapID;
    public Vector2 position;
}

public class SaveQuestData
{
    public int currentQuest;
    public List<int> completeQuests;
}