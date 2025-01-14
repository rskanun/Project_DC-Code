using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;

[Serializable]
public class CsvFile : IEnumerable<string[]>
{
    private List<string[]> cells;

    public CsvFile()
    {
        cells = new List<string[]>();
    }

    public string[] GetLineCells(int index)
    {
        return cells[index];
    }

    public void AddLineCells(string[] lineCells)
    {
        cells.Add(lineCells);
    }

    public CsvFile MergeLines(CsvFile mergeFile)
    {
        // ���� ���Ͽ� �Ű������� ���� ���� �� �� ��ġ��
        cells.AddRange(mergeFile.cells);

        // ���� ������ ��� ������ ����
        return this;
    }

    public IEnumerator<string[]> GetEnumerator()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            yield return cells[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class CsvReader
{
    public static List<CsvFile> ReadFiles(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            // ���� �� CSV ������ �о� ����
            List<CsvFile> files = new List<CsvFile>();
            string[] csvFiles = Directory.GetFiles(folderPath, "*.csv");

            foreach (string filePath in csvFiles)
            {
                TextAsset textAsset = GetTextAsset(filePath);
                CsvFile file = ReadFile(textAsset);

                files.Add(file);
            }

            return files;
        }

        return null;
    }

    public static CsvFile ReadFile(string path)
    {
        TextAsset textAsset = GetTextAsset(path);

        return ReadFile(textAsset);
    }

    public static CsvFile ReadFile(TextAsset csvFile)
    {
        CsvFile result = new CsvFile();
        string[] lines = csvFile.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] lineCells = SplitLine(line);

            result.AddLineCells(lineCells);
        }

        return result;
    }

    private static TextAsset GetTextAsset(string path)
    {
        // ���� ����� ��ȿ���� �˻��ϰ� ������ ����
        IsCorrectPath(path);

        // ���Ͽ��� �ؽ�Ʈ �б�
        string fileContent = File.ReadAllText(path);

        // TextAsset�� �������� ����
        TextAsset textAsset = new TextAsset(fileContent);

        return textAsset;
    }

    private static bool IsCorrectPath(string path)
    {
        // ������ ã�� �� ���� ���
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"������ ã�� �� �����ϴ�: {path}");
        }

        // ���� Ȯ���ڰ� .csv�� �ƴ� ���
        if (Path.GetExtension(path).ToLower() != ".csv")
        {
            throw new InvalidDataException($"�߸��� ���� �����Դϴ�. csv ������ �ʿ��մϴ�: {path}");
        }

        return true;
    }

    private static string[] SplitLine(string line)
    {
        // ���޹��� ������ ���� ����
        string result = RemoveComment(line);

        // CSV ������ �� ���� ������
        string[] cells = line.Split(',');

        return cells;
    }

    private static string RemoveComment(string str)
    {
        int commentIndex = str.IndexOf('#');

        // �ּ��� �����ϴ� ���
        if (commentIndex >= 0)
        {
            // �ּ� ����(#)�� ������ ���� ������ ����
            return str.Substring(0, commentIndex);
        }

        // �ּ��� ������ �׳� ����
        return str;
    }
}
