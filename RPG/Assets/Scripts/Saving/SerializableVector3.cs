/* #### serializa um vetor, recebendo em 3 floats "x, y, z", os valores respectivo do vetor recebido 
 * ####podendo assim, serializar os valores do vetor como tambem retornar o valor serializado como um vetor */
using UnityEngine;

/*namespace RPG.Saving
{
    // criar pasta Saving caso nao tenha feito ainda
}*/

[System.Serializable]//função do sistema para serializar a classe
public class SerializableVector3
{
    float x, y, z;//variaveis para armazenar o vetor

    //serializa vetor recebido
    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    //retorna valor serializado como um vetor
    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }
}