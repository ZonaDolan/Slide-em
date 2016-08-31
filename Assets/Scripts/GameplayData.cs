using UnityEngine;
using System.Collections.Generic;

public class GameplayData {
    private long lowerBound;
    private long upperBound;

    private long target;

    private long score;

    private static readonly float INITIAL_SECOND = 60;
    private float secondLeft;

    private List<long> primeList;

    private int level;

    // Generate prime list below 1000 (10 ^ 3)
    private void sieve() {
        primeList = new List<long>();

        bool[] visited = new bool[1010];
        for (int i = 0; i < 1010; i++) visited[i] = false;

        for (long i = 2; i <= 1000; i++) {
            if (visited[i])
                continue;
            primeList.Add(i);
            for (long j = i * i; j * j <= 1000; j += i) visited[j] = true;
        }
    }

    public GameplayData(long lowerBound, long upperBound) {
        Random.seed = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        sieve();
        setBound(lowerBound, upperBound);
        
        this.secondLeft = INITIAL_SECOND;
        this.score = 0;
        this.level = 0;
    }

    public long getTarget() {
        return this.target;
    }

    private void setBound(long lowerBound, long upperBound) {
        if (lowerBound >= upperBound)
            throw new UnityException("Lower bound must smaller than upper bound");

        this.lowerBound = lowerBound;
        this.upperBound = upperBound;
        this.target = (int)Random.Range(this.lowerBound + 1, this.upperBound - 1);
    }

    public void nextLevel() {
        this.level++;
        this.level = this.level % primeList.Count;
        setBound(this.lowerBound, this.upperBound + primeList[level]);
    }

    public void decreaseSecondLeft(float delta) {
        this.secondLeft -= delta;
        if (this.secondLeft < 0.0f)
            this.secondLeft = 0.0f;
    }

    public void addSecondLeft(float delta) {
        this.secondLeft += delta;
        if (this.secondLeft >= INITIAL_SECOND)
            this.secondLeft = INITIAL_SECOND;
    }

    public void addScore(long score) { this.score += score; }

    public long getLowerBound() { return this.lowerBound; }
    public long getUpperBound() { return this.upperBound; }
    public float getSecondLeft() { return this.secondLeft; }
    public long getScore() { return this.score; }
}
