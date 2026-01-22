public abstract class Controller
{
    private bool isEnabled;

    public virtual void Enable() => isEnabled = true;

    public virtual void Disable() => isEnabled = false;
    
    public void Update(float deltaTime)
    {
        if (isEnabled == false)
            return;

        UpdateLogic(deltaTime);
    }

    protected abstract void UpdateLogic(float deltaTime);
}
