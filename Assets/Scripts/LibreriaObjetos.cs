using UnityEngine;

[CreateAssetMenu(fileName = "LibreriaObjetos", menuName = "Scriptable Objects/LibreriaObjetos")]
public class LibreriaObjetos : ScriptableObject
{
    public Alfabeto letra;
    public ObjetoTemplate[] objetos;

    public void ShuflearObjetos()
    {
        RandomizeArray(objetos);

    }

    public static void RandomizeArray<T>(T[] array)
    {
        System.Random random = new System.Random(); // Crear una instancia de Random

        // Recorrer el arreglo desde el final hasta el principio
        for (int i = array.Length - 1; i > 0; i--)
        {
            // Seleccionar un índice aleatorio entre 0 e i (inclusive)
            int j = random.Next(i + 1);

            // Intercambiar los elementos en las posiciones i y j
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
