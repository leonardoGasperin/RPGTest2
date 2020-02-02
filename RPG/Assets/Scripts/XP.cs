/*####Administrador do XP do personagem jogador*/
using UnityEngine;
using System;

public class XP : MonoBehaviour, ISaveable
{
    [SerializeField] float xpPoint = 0;//ponstos de xp

    public event Action OnXpGain;//eventos

    //retorna xp atual
    public float _xp
    {
        get { return xpPoint; }
    }

    //ganho de xp
    public void GainXP(float exp)
    {
        xpPoint += exp;//recebe xp
        OnXpGain();//checa eventos do progresso do jogador
    }

    //salva experiancia
    public object CaptureState()
    {
        return xpPoint;
    }

    //restoura experiencia
    public void RestoreState(object state)
    {
        xpPoint = (float)state;
    }
}
