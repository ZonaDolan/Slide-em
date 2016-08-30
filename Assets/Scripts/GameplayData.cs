using UnityEngine;
using System.Collections;

public class GameplayData {
    // Please keep the distance even between lowerBound and upperBound
    private long lowerBound;
    private long upperBound;

    private long middle;

    private long score;

    private static readonly float INITIAL_SECOND = 60;
    private float secondLeft;


    public GameplayData(long lowerBound, long upperBound) {
        setBound(lowerBound, upperBound);

        this.secondLeft = INITIAL_SECOND;
        this.score = 0;
    }

    public long getMiddle() {
        return this.middle;
    }

    public void setBound(long lowerBound, long upperBound) {
        if (lowerBound >= upperBound)
            throw new UnityException("Lower bound must smaller than upper bound");
        if ((upperBound - lowerBound) % 2 > 0)
            throw new UnityException("Distance between lower bound and upper bound must be even");

        this.lowerBound = lowerBound;
        this.upperBound = upperBound;
        this.middle = (this.lowerBound + this.upperBound) / 2;
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
