/*####Interface de modificadores*/
using System.Collections.Generic;

public interface IAddModifier
{
    IEnumerable<float> GetAddMods(Stats stat);
    IEnumerable<float> GetPercentMods(Stats stats);

}
