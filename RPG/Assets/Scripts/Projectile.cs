/*####Administra projetei de armas*/
using UnityEngine;
using UnityEngine.Events;

/*namespace RPG.Combat
{
    //criar pasta Combat caso nao tenha
}*/

public class Projectile : MonoBehaviour
{
    //referencias e variaveis
    [SerializeField] GameObject hitEffect = null;//efeito do projetil
    [SerializeField] UnityEvent onHit = null;//evento de hit
    [SerializeField] float speed = 1;//velocidade
    [SerializeField] float timer = 10;//tempo de vida
    [SerializeField] bool isHoming = false;//checa se persegue alvo

    Health target = null;//vida do alvo
    GameObject instigator = null;//alvo
    float damage = 0;//dano do projetil
    bool isDmg = true;//checa se pode dar dano

    private void Start()
    {
        //inicializa mira no alvo
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        //checa se tem alvo
        if (target == null) return;
        if (isDmg)
        {//caso tenha se pode contar dano
            if (isHoming && !target.Died())//se o alvo estiver vivo e projetil segue
                transform.LookAt(GetAimLocation());//encontre o alvo

            transform.Translate(Vector3.forward * speed * Time.deltaTime);//movimento do projetil
        }
        else
            transform.position = GetAimLocation();//caso nao possa dar dano continue indo
    }

    //mostra alvo
    public void SetTarget(Health t, GameObject instigator, float dmg)
    {
        this.target = t;//vida do alvo
        this.damage = dmg;//dano causado
        this.instigator = instigator;//alvo

        Destroy(gameObject, timer);//destroi projetil apos tempo de vida
    }

    //posisao do alvo
    private Vector3 GetAimLocation()
    {
        //referencia do colisor do alvo
        CapsuleCollider targetCaps;
        targetCaps = target.GetComponent<CapsuleCollider>();

        //caso nao bateu no alvo retorna posisao do alvo
        if (targetCaps == null) return target.transform.position;
        
        //caminho do projetil
        return target.transform.position + Vector3.up * (targetCaps.height / 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;//se nao eh o alvo retorna

        isDmg = false;//nao causa mais dano
        if (hitEffect)
        {//se tem efeito instancia
            Instantiate(hitEffect, this.transform.position, this.transform.rotation);
        }

        target.Damge(instigator, damage);//causa dano do alvo
        onHit.Invoke();

        //se tem um trail desativa-o
        if (GetComponentInChildren<TrailRenderer>())
            GetComponentInChildren<TrailRenderer>().enabled = false;
        
        Destroy(gameObject, 0.5f);//destroi objeto
        
        if (!other.GetComponent<Health>().Died()) return;//se o alvo esta morto retorna
        
        Destroy(gameObject);//caso nao destroi projetil
    }
}
