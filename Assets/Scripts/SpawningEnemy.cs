public class SpawningEnemy
{
    public SpawningEnemy(string enemy, int debounce, int amount, float delay)
    {
        Enemy = enemy;
        Debounce = debounce;
        Amount = amount;
        Delay = delay;
    }
    public string Enemy { get; set; }
    public int Debounce { get; set; }
    public int Amount { get; set; }
    public float Delay { get; set; }

}