using System.Collections.Generic;

public class TextScript
{
    private Dictionary<int, List<Line>> _scenarios;

    public TextScript()
    {
        _scenarios = new Dictionary<int, List<Line>>();
    }

    public List<Line> GetLines(int scenarioNum)
    {
        // 해당 번호의 시나리오가 없거나 로드되지 않았다면
        if (ContainsKey(scenarioNum) == false)
        {
            // 빈 값 리턴
            return null;
        }

        return _scenarios[scenarioNum];
    }

    public void SetLines(List<Line> scenario, int scenarioNum)
    {
        _scenarios[scenarioNum] = scenario;
    }

    public bool ContainsKey(int id)
    {
        return _scenarios.ContainsKey(id);
    }
}