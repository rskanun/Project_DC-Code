using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public SaveChapterData chapterData;
    public SavePositionData positionData;
    public SaveQuestData questData;
    public DateTime saveTime;
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