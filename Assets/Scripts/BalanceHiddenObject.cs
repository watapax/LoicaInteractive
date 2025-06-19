using UnityEngine;

public class BalanceHiddenObject : MonoBehaviour
{
    
    public BalanceSettings balanceSettings;
    bool isRacha;
    public float lastObjectFoundTime;

    

    public float CalcularPuntaje()
    {
        float puntaje = balanceSettings.puntajaBase;
        float tiempoDeBusqueda = Time.time - lastObjectFoundTime;

        if(tiempoDeBusqueda < balanceSettings.maxTimeForBonus && isRacha)
        {
            float porcentaje = tiempoDeBusqueda / balanceSettings.maxTimeForBonus;
            float valorBonus = balanceSettings.puntajeMaximo - (balanceSettings.puntajeMaximo * porcentaje);
            puntaje += valorBonus;
        }

        isRacha = true;


        return puntaje;
    }

    public void DetenerRacha()
    {
        isRacha = false;
    }

}
