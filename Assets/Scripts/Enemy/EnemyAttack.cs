using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyData enemyData;
    public bool parryable;
    public ParticleSystem[] loadUpParticles;
    public Light[] flashes;
    public EnemyController controller;
    public void LoadUp()
    {
        foreach(ParticleSystem ps in loadUpParticles)
            ps.Play();
    }
    public void ParryOpen()
    {
        foreach(ParticleSystem ps in loadUpParticles)
            ps.Stop();
        foreach (Light flash in flashes)
            flash.enabled = true;
        controller.OpenParry(true);
    }
    public void ParryClose()
    {
        controller.OpenParry(false);
        foreach (Light flash in flashes)
            flash.enabled = false;
        CheckHit();
    }
    void CheckHit()
    {
        if(!Player.instance.playerBlock.parried)
            Player.instance.TakeDamage(controller.enemyData.attackDamage);
        else
            Player.instance.playerBlock.ResetParried();
    }
}
