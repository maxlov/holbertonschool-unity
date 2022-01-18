using System.Collections;
using System.Collections.Generic;

public abstract class State
{
    protected GameManager gameManager;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
