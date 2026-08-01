using System.Collections;
using System.Threading.Tasks;

public interface IBattleEntity
{
    public int ID { get; set; }

    public IEnumerator Action();
    public int ApplyResistances(int amount);
    public void TakeDamage(int amount);
    public IEnumerator DeathSequence();
}
