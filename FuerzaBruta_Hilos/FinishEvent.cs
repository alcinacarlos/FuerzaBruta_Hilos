namespace Fuerza_bruta;

public class FinishEvent
{
    public Action FinishAction;

    public FinishEvent()
    {
        FinishAction = () => { };
    }
}