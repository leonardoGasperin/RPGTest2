/*####Arquivo de administraçao de armas*/
using UnityEngine;

/*namespace RPG.Combat
{
    //criar pasta Combat caso nao tenha
}*/

[CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Weapon", order = 0)]
public class WeaponConfig : ScriptableObject
{
    //referencias e variaveis
    [SerializeField] AnimatorOverrideController waeponOverD = null;//animacao da arma padrao
    [SerializeField] Weapon equipPrb = null;//equip prefab
    [SerializeField] Projectile project = null;//projetei
    [SerializeField] float damage = 5;//quantia de dano
    [SerializeField] float weaponBonus = 0;//quantia de bonus em %
    [SerializeField] float weaponsR = 5f;//range da arma
    [SerializeField] bool isRightHand = true;//checa mao do equip

    const string wName = "Unarmed";//referencia do nome do item

    public Weapon Spawn(Transform r_handT, Transform l_handT, Animator anim)
    {//coloca o item no lugar ccerto no persanagem
        DestroyOldWeapon(r_handT, l_handT);//destroy arma equipada anteriomente

        Weapon weapon;

        if (equipPrb == null) { Debug.Log("NULL!"); return null; }//checa se ter item a ser equipado

        weapon = Instantiate(equipPrb, GetTransform(r_handT, l_handT));//estancia item
        weapon.gameObject.name = wName;//pega nome do item
        
        var overriderController = anim.runtimeAnimatorController as AnimatorOverrideController;//referencia do overrider da animacao

        if (anim != null)//se nao tiver override coloca animador padrao
            anim.runtimeAnimatorController = waeponOverD;
        else if (overriderController != null)
        {//caso contrario coloca animacao da arma
            anim.runtimeAnimatorController = overriderController.runtimeAnimatorController;
        }

        return weapon;
    }

    //destroi arma equiada anteriormente
    private void DestroyOldWeapon(Transform r_handT, Transform l_handT)
    {
        Transform oldW = r_handT.Find(wName);//referencia da arma
        if(oldW == null)
        {//caso nao tenha arma nao faz anda
            oldW = l_handT.Find(wName);
            
            if (oldW == null) return;
        }

        oldW.name = "DESTROYED";//destroi o nome da arma
        Destroy(oldW.gameObject);//destroi a arma
        Debug.Log(oldW);
    }

    //pega posicao de onde sera equipado a arma
    private Transform GetTransform(Transform r_handT, Transform l_handT)
    {
        if (isRightHand)//mao direita
            return r_handT;
        else//mao esquerda
            return l_handT;
    }

    //checa se tem projeteis
    public bool HasProjectile()
    {
        return project != null;
    }

    //lança projeteis
    public void LauchProjectile(Transform r_handT, Transform l_handT, Health t, GameObject instigator, float calcDmg)
    {
        Projectile projectIns = Instantiate(project, GetTransform(r_handT, l_handT).position, Quaternion.identity);//estancia projeteis
        projectIns.SetTarget(t, instigator, calcDmg);//seta alvo
    }

    //retorna dano da arma
    public float GetDamage()
    {
        return damage;
    }

    //retorna bonus de porcentagem
    public float GetPercentBonus()
    {
        return weaponBonus;
    }

    //retorna alcance da arma
    public float GetRange()
    {
        return weaponsR;
    }
}
