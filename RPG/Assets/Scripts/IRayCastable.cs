/*####Interface de interagiveis com cursor*/

public interface IRayCastable
{ 

    CursorType GetCursorType();

    bool HandleRaycast(PlayerController callingControl);
}