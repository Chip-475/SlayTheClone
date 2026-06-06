using System.Collections;

public interface IBattleEntity
{
    public IEnumerator BattleAction();
    public void StopActionBar();
    public void StartActionBar();
}
