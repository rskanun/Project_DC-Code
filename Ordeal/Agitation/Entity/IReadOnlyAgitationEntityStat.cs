public interface IReadOnlyAgitationEntityStat
{
    public int MaxHP { get; }
    public int HP { get; }
    public int MaxAgitationLevel { get; }
    public int AgitationLevel { get; }
    public int RoundDamage { get; }
}