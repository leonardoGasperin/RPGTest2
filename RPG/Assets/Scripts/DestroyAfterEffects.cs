/*####Administrador de efeitos, retira o efeito depois do termino*/
using UnityEngine;

public class DestroyAfterEffects : MonoBehaviour
{
    [SerializeField] GameObject targetDestroy = null;//referencia do que ser distruido

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<ParticleSystem>()) 
        {//se nao tem componente de particula
            if (!GetComponent<TrailRenderer>().isVisible)
            {//se nao for mais vizivel
                if (targetDestroy != null)
                    Destroy(targetDestroy);//se ouver algo para destruir destoi
                else
                    Destroy(gameObject);//caso contrario destroi o objeto principal
            }
        }
        else 
        {//caso tenha particulas
            if (!GetComponent<ParticleSystem>().IsAlive())
            {//se o efeito esta vivo
                if (targetDestroy != null)
                    Destroy(targetDestroy);//caso tenha algo para destuir destroi
                else
                    Destroy(gameObject);//caso contrario destroi o objeto principal
            }
        }
    }
}
