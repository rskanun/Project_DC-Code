using UnityEngine;

public class GameData : ScriptableObject
{
    /************************************************************
    * [é�� ������]
    * 
    * ���� �÷��̾ ���� ���� é��(1~9), �б� ��ȣ, é�� ��
    * ������ ���� ���� é�� ��ȣ ������
    ************************************************************/
    [SerializeField]
    private Chapter _chapter;
    public Chapter Chapter
    {
        set { _chapter = value; }
        get
        {
            if (_chapter == null)
                _chapter = new Chapter(0, 0, 0);

            return _chapter;
        }
    }

    /************************************************************
    * [�� ������]
    * 
    * ���� �÷��̾ �ִ� �ʿ� ���� ������
    ************************************************************/
    [SerializeField]
    private MapData _currentMap;
    public MapData CurrentMap
    {
        set { _currentMap = value; }
        get
        {
            // ���� �� �ҷ����� ��� ��ó ���
            if (_currentMap == null)
                return null;

            return _currentMap;
        }
    }
}