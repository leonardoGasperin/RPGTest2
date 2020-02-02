/* #### interface para fazer o tipo(script) ser salvavel, carrega com sigo as header das funções de salvar data e restalrar data
   #### salvaa recebendo um objeto serializavel e restalra esse objeto*/
/*namespace RPG.Saving
{
    // criar pasta Saving caso nao tenha feito ainda
}*/

public interface ISaveable
{
    object CaptureState();//salva data
    void RestoreState(object state);//restalra data
}