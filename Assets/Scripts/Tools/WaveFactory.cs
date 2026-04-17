using UnityEngine;

public static class WaveFactory {
    public static Wave CreateRandomWave()
    {
        int ran =Random.Range(0,2);
        switch (ran)
        {
            case 0: return new DefendWave();
            default: return new DefendWave(); //TODO : CHANGE THIS
        }
    }
}