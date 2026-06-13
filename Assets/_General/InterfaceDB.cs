using System.Collections;

public interface IBattleEntity
{
    public int GetId();
    public void StopActionBar();
    public void StartActionBar();

    public IEnumerator BattleAction();
    public void TakeDamage(int amount);
    public void CalcDmg(int amount);
}
