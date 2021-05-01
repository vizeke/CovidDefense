using System.Collections.Generic;

public class Wave
{
    public string Name;
    public List<SpawningEnemy> Spawners;
    private bool started = false;
    private bool finished = false;
    private float clearTime = 0;

    public Wave(string name, List<SpawningEnemy> spawners)
    {
        this.Spawners = spawners;
        Name = name;
    }

    public bool Started { get => started; }
    public bool Finished { get => finished; }

    public void Start()
    {
        started = true;
    }

    public void Clear(float time)
    {
        finished = true;
        clearTime = time;
    }
}
