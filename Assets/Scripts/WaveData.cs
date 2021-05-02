using System.Collections.Generic;
using UnityEngine;

public class WaveData : MonoBehaviour
{
    public IEnumerable<Wave> Waves = new List<Wave> {
        new Wave("First wave", FirstWave),
        new Wave("Second wave",SecondWave),
    };

    public static List<SpawningEnemy> FirstWave = new List<SpawningEnemy>
    {
        // new SpawningEnemy("Capsule", 2, 5, 0 ),
        // new SpawningEnemy("Sphere", 1, 2, 5 ),
        new SpawningEnemy("coronavirusEnemy", 1, 4, 1 ),
    };

    public static List<SpawningEnemy> SecondWave = new List<SpawningEnemy>
    {
        new SpawningEnemy("Sphere", 3, 10, 0 ),
        new SpawningEnemy("Capsule", 2, 20, 5 ),
        new SpawningEnemy("coronavirusEnemy", 6, 30, 0 ),
    };
}
