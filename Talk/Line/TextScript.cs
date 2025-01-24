using System.Collections.Generic;

public class TextScript
{
    private Dictionary<int, List<Line>> scenarios;

    public TextScript()
    {
        scenarios = new Dictionary<int, List<Line>>();
    }

    public List<Line> GetLines(int scenarioNum)
    {
        // 해당 번호의 시나리오가 없거나 로드되지 않았다면
        if (ContainsKey(scenarioNum) == false)
        {
            // 빈 값 리턴
            return null;
        }

        return scenarios[scenarioNum];
    }

    public void SetLines(List<Line> scenario, int scenarioNum)
    {
        scenarios[scenarioNum] = scenario;
    }

    public bool ContainsKey(int id)
    {
        return scenarios.ContainsKey(id);
    }
}