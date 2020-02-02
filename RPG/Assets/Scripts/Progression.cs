/*####Administra progreço de personagens*/
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Progression", menuName = "RPG Project/Stats/New Progression", order = 0)]
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;//classes e tipos 

    Dictionary<CharacterClass, Dictionary<Stats, float[]>> lookUpTable = null;//gera um dicionario vazia para tabela

    //retorna estatos
    public float GetStats(Stats stat, CharacterClass chterClass, int lvl)
    {
        BuildLookUp();//checa na dicionario

        float[] levels = lookUpTable[chterClass][stat];//recebe os valores da tabela

        if (levels.Length < lvl) return 0;//se nao existe na tabela

        return levels[lvl - 1];//caso nao retorna o valor da variavel da lista
    }

    //retorna novo lvl apos evoluir
    public int GetLevelUp(Stats stat, CharacterClass chterClass)
    {
        BuildLookUp();//checa dicionario

        float[] levels = lookUpTable[chterClass][stat];//recebe valores da lista
        return levels.Length;//retorna nivel
    }

    //checa a lista
    private void BuildLookUp()
    {
        if (lookUpTable != null) return;//se nao tem nada no dicionario

        lookUpTable = new Dictionary<CharacterClass, Dictionary<Stats, float[]>>();//gera um novo dicionario

        foreach (ProgressionCharacterClass progCht in characterClasses)
        {//para cada item no dicionario
            var statLookUp = new Dictionary<Stats, float[]>();//adiciona itens da lista

            foreach (ProgressionStat progSt in progCht.stats)
            {//para item na lista
                statLookUp[progSt.stats] = progSt.lvls;//adiciona o valor referente ao nivel
            }
            lookUpTable[progCht.characterClass] = statLookUp;//adiciona ao dicionario
        }
    }

    [System.Serializable]
    class ProgressionCharacterClass
    {
        public CharacterClass characterClass;//lista de classes
        public ProgressionStat[] stats;//lista de estatos
    }

    [System.Serializable]
    public class ProgressionStat
    {
        public Stats stats;//estatos
        public float[] lvls;//valores dos estatos
    }
}

