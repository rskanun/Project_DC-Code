using UnityEngine;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TextScriptManager : ScriptableObject
{
    private const string FILE_DIRECTORY = "Assets/Resources/Scenario";
    private const string FILE_PATH = "Assets/Resources/Scenario/ScriptManager.asset";

    private static TextScriptManager _instance;
    public static TextScriptManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<TextScriptManager>("Scenario/ScriptManager");

#if UNITY_EDITOR
            if (_instance == null)
            {
                // 파일 경로가 없을 경우 폴더 생성
                if (!AssetDatabase.IsValidFolder(FILE_DIRECTORY))
                {
                    string[] folders = FILE_DIRECTORY.Split('/');
                    string currentPath = folders[0];

                    for (int i = 1; i < folders.Length; i++)
                    {
                        if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                        {
                            AssetDatabase.CreateFolder(currentPath, folders[i]);
                        }

                        currentPath += "/" + folders[i];
                    }
                }

                // Resource.Load가 실패했을 경우
                _instance = AssetDatabase.LoadAssetAtPath<TextScriptManager>(FILE_PATH);

                if (_instance == null)
                {
                    _instance = CreateInstance<TextScriptManager>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif

            return _instance;
        }
    }

    [SerializeField]
    [FolderPath(RequireExistingPath = true)]
    private string scriptsDirectory;

    private Stack<Select> selectStack = new Stack<Select>();

    private TextScript currentScript = new TextScript();

    public List<Line> GetNpcLines(int npcID)
    {
        int scenarioNum = GetScenarioNumByNpc(npcID);

        return currentScript.GetLines(scenarioNum);
    }

    public List<Line> GetQuestLines(int questID, QuestState state)
    {
        int scenarioNum = GetScenarioNumByQuest(questID, (int)state);

        return currentScript.GetLines(scenarioNum);
    }

    public void LoadScript(int chapter, int root, int subChapter)
    {
        // 해당 상황에 맞는 스크립트를 가져올 폴더 경로 구하기
        string path = GetFolderPath(chapter, root, subChapter);

        // 경로를 통한 CSV 파일 라인 구하기
        List<CsvFile> files = CsvReader.ReadFiles(path);
        CsvFile mergeFile = MergeCsvFiles(files);

        // CSV 파일의 정보를 토대로 텍스트 스크립트 구현
        currentScript = BuildTextScript(mergeFile);
    }

    private string GetFolderPath(int chapter, int root, int subChapter)
    {
        // 챕터번호 1자리 + 분기번호 1자리 + 서브챕터번호 2자리
        string folderName = chapter.ToString() + root.ToString()
            + ((subChapter < 10) ? "0" : "") + subChapter.ToString();

        string path = scriptsDirectory + "/" + folderName;

        return path;
    }

    private CsvFile MergeCsvFiles(List<CsvFile> files)
    {
        CsvFile result = new CsvFile();

        // 매개변수로 받은 모든 CsvFile 값을 하나의 CsvFile 값으로 합치기기
        foreach (CsvFile file in files)
        {
            result.MergeLines(file);
        }

        return result;
    }

    private TextScript BuildTextScript(CsvFile csvFile)
    {
        TextScript script = new TextScript();

        Dictionary<int, int> lineNum = new Dictionary<int, int>(); // ID 값에 해당하는 라인의 마지막 index값
        int id = 0; // id를 기억할 dummy int

        foreach (string[] cells in csvFile)
        {
            // 처음 부여한 ID일 경우
            int tmpID = 0;
            if (int.TryParse(cells[0], out tmpID) && !script.ContainsKey(tmpID))
            {
                // 처음 부여한 키일 때에만 id값 변경
                id = tmpID;

                // 새 스크립트 라인 생성
                script.SetLines(new List<Line>(), id);
                lineNum.Add(id, 0);
            }

            // 라인 객체 생성
            Line line = CreateLine(cells, lineNum[id]);

            // 스크립트에 추가
            script.GetLines(id).Add(line);

            // 다음 라인 넘버로 넘어감
            lineNum[id]++;
        }

        return script;
    }

    private Line CreateLine(string[] strs, int lineNum)
    {
        // 코드별로 분리
        LineType code = (LineType)Enum.Parse(typeof(LineType), strs[1]);
        Line line = LineFactory.Instance.CreateLine(code, strs);

        // Select 처리
        if (code == LineType.Select)
        {
            selectStack.Push((Select)line);
        }
        // Case 처리
        else if (code == LineType.Case)
        {
            Select select = selectStack.Peek();
            select.addOptionBookmark(((Case)line).Choice, lineNum);
        }
        // End 처리
        else if (code == LineType.End)
        {
            Select select = selectStack.Pop();
            select.EndLineNum = lineNum;
        }

        return line;
    }

    public bool HasNpcLines(int npcID)
    {
        int id = GetScenarioNumByNpc(npcID);

        return HasLines(id);
    }

    public bool HasQuestLines(int questID, QuestState state)
    {
        int id = GetScenarioNumByQuest(questID, (int)state);

        return HasLines(id);
    }

    private bool HasLines(int id)
    {
        if (id > 0)
            return currentScript.ContainsKey(id);

        // id값이 0보다 작으면 
        return false;
    }

    private int GetScenarioNumByNpc(int npcID)
    {
        // NPC의 일반적인 대사의 경우
        // NPC 판별 번호 1 + NPC 아이디 6자리를 합쳐 시나리오 번호로 지정
        // ex) NPC 판별 번호 1 + NPC 아이디 001000 => 시나리오 번호 1001000
        return int.Parse("1" + npcID.ToString("D6"));
    }

    private int GetScenarioNumByQuest(int questID, int stateNum)
    {
        // 퀘스트에 따른 NPC의 대사의 경우
        // 퀘스트 판별 번호 2 + 퀘스트 아이디 5자리 + 상태 번호 1자리를 합쳐 시나리오 번호로 지정
        // ex) 퀘스트 판별 번호 2 + 퀘스트 아이디 1 + 상태 번호 1 => 시나리오 번호 2000011
        return int.Parse("2" + questID.ToString("D5") + stateNum);
    }
}