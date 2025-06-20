using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;
using DG.Tweening;


public enum Alfabeto
{
    A, B, C, TODOS     
}

[System.Serializable]

public class HiddenObjectManager : MonoBehaviour
{
  
    public static HiddenObjectManager instance;


    Dictionary<string, LibreriaObjetos> diccionario = new Dictionary<string, LibreriaObjetos> ();
    public LibreriaObjetos todos;

    public Abecedario abecedario;
    public Alfabeto[] letrasCorrectas;


    public Transform[] slotTransforms;
    public Transform[] ghostTransforms;
    public Transform canvasTransform;
    public Transform layoutGroupParent;



    public int cantidadDeObjetosMaximo;
    public int cantidadObjetosCorrectos;

    public ExperienceManager experienceManager;
    public Contador contador;
    public PanelRespuesta panelRespuesta;
    public GameObject hiddenObjectPrefab;
    HiddenObject[] hiddenObjects;
    public Transform parentHiddenObjects;
    public GameObject ghostObjectPrefab;
    //public TextMeshProUGUI letraTextSeleccionada;
    public Transform contenedorLetras;
    public GameObject prefabLetra;
    public Animator animator;

    BalanceHiddenObject balance;
    public void IniciarJuego()
    {
        //local experience se resetea
        experienceManager.ResetLocalExp();
        experienceManager.SetTotal(cantidadObjetosCorrectos);

        // poblar el diccionario _abecedario
        for (int i = 0; i < abecedario.libreria.Length; i++)
        {
            diccionario.Add(abecedario.libreria[i].letra.ToString(), abecedario.libreria[i]);
        }


        //Instanciar objetos Vacios
        hiddenObjects = new HiddenObject[slotTransforms.Length];
        for ( int i = 0; i < cantidadDeObjetosMaximo; i++)
        {
            GameObject _go = Instantiate(hiddenObjectPrefab, parentHiddenObjects);
            hiddenObjects[i] = _go.GetComponent<HiddenObject>();
            hiddenObjects[i].rectTransform.localScale = Vector3.zero;
        }

        // shuflear slots
        RandomizeArray(slotTransforms);
        
        //asignar un slotTransform a cada HiddenObjets
        for(int i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].rectTransform.position = slotTransforms[i].position;
        }

        //poblar las respuestas correctas con las letras designadas

        int indexObjetosCorrectos = 0;
        for(int i = 0; i < letrasCorrectas.Length; i++)
        {
            // obtener la libreria de objetos del index la letra correcta
            LibreriaObjetos _libreriaTemp = new LibreriaObjetos();
            _libreriaTemp = diccionario[letrasCorrectas[i].ToString()];
            _libreriaTemp.ShuflearObjetos();
            for (int j = 0; j < cantidadObjetosCorrectos / letrasCorrectas.Length; j++)
            {
                // poblar los datos del objeto correcto con los datos del template
                hiddenObjects[indexObjetosCorrectos].AlimentarDatosHiddenObject(_libreriaTemp.objetos[j], escalaObjetos);
                indexObjetosCorrectos++;
            }
        }


        //poblar los Ghost de respuestas
        ghostTransforms = new RectTransform[cantidadObjetosCorrectos];
        for(int i = 0; i < cantidadObjetosCorrectos; i++)
        {
            GameObject _go = Instantiate(ghostObjectPrefab, layoutGroupParent.transform);
            _go.GetComponent<Image>().sprite = hiddenObjects[i].image.sprite;
            _go.GetComponent<Image>().SetNativeSize();
            
            ghostTransforms[i] = _go.GetComponent<RectTransform>();
            ghostTransforms[i].localScale = Vector3.one * 0.3f;
        }

        // asignar la posicion del ghost a los hiddenObjects correctos
        StartCoroutine(PoblarSlots());


        //poblar los objetos incorrectos
        // crear un listado de objetos que no son los correctos
        List<ObjetoTemplate> _objetosIncorrectos = new List<ObjetoTemplate>();

        for (int i = 0; i < todos.objetos.Length; i++)
        {
            if (!EsIgualALetra(todos.objetos[i].letra))
            {
                _objetosIncorrectos.Add(todos.objetos[i]);
            }
        }

        // shuflear la lista
        RandomizarLista(_objetosIncorrectos);

        // asignar los objetos a los hiddenObject incorrectos
        for(int i = indexObjetosCorrectos; i < cantidadDeObjetosMaximo; i++)
        {
            hiddenObjects[i].AlimentarDatosHiddenObject(_objetosIncorrectos[i], escalaObjetos);
        }


        // mostrar la letra elegida en el panel
        //letraTextSeleccionada.text = "";
        for (int i = 0; i < letrasCorrectas.Length; i++)
        {
            GameObject _prefabLetra = Instantiate(prefabLetra, contenedorLetras);
            _prefabLetra.GetComponentInChildren<TextMeshProUGUI>().text = letrasCorrectas[i].ToString();
        }

        //Animar objetos agrandandose
        AgrandarLosObjetos();

    }




    bool EsIgualALetra(string letra)
    {
        bool result = false;

        for(int i = 0; i < letrasCorrectas.Length; i++)
        {
            if(letra == letrasCorrectas[i].ToString())
            {
                result = true;
                break;
            }
        }


        return result;
    }


    IEnumerator PoblarSlots()
    {
        yield return new WaitForSeconds(1.5f);



        // asignar el ghostPosition a los hiddenObject Correctos
        for (int i = 0; i < cantidadObjetosCorrectos; i++)
        {
           //Vector3 ghostWorldPos = GetWorldPositionOfUIElement(ghostTransforms[i]);
            hiddenObjects[i].ghostPosition = ghostTransforms[i].position;
            //hiddenObjects[i].ghostPosition.y = adjustY;
        }


    }



    void AgrandarLosObjetos()
    {
        for(int i = 0; i < hiddenObjects.Length; i++)
        {
            float duracion = UnityEngine.Random.Range(0.7f, 0.9f);
            hiddenObjects[i].rectTransform.DOScale(Vector3.one * escalaObjetos, duracion).SetEase(Ease.OutExpo);
        }

    }
    public float escalaObjetos;

    Vector3 GetWorldPositionOfUIElement(RectTransform element)
    {
        // Obtener la posici�n en la pantalla del objeto de UI
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(null, element.position);

        // Convertir la posici�n de la pantalla a una posici�n en el mundo
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
        return worldPosition;
    }




    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);

        balance = GetComponent<BalanceHiddenObject>();
    }
    public void ComenzarMiniJuego()
    {
        IniciarJuego();
        contador.Comenzar();
        balance.lastObjectFoundTime = Time.time;

    }




    public void ChequearObjeto(HiddenObject _hiddenObject)
    {

        bool respuestaCorrecta = false;
        for (int i = 0; i < letrasCorrectas.Length; i++)
        {
            if (letrasCorrectas[i].ToString() == _hiddenObject.letra)
            {
                respuestaCorrecta = true;
                break;
            }
        }


        if (respuestaCorrecta)
            RespuestaCorrecta(_hiddenObject);
        else
            RespuestaIncorrecta(_hiddenObject);

        panelRespuesta.ActivarVentana(_hiddenObject, respuestaCorrecta);
    }

    void RespuestaCorrecta(HiddenObject _hiddenObject)
    {
        print("respuesta correcta");
        experienceManager.GainLocalExp(balance.CalcularPuntaje());
        contador.ToggleContador(false);
    }

    void RespuestaIncorrecta(HiddenObject _hiddenObject)
    {
        balance.DetenerRacha();
        print("respuesta incorrecta");
        contador.ToggleContador(false);
    }


    public static void RandomizeArray<T>(T[] array)
    {
        System.Random random = new System.Random(); // Crear una instancia de Random

        // Recorrer el arreglo desde el final hasta el principio
        for (int i = array.Length - 1; i > 0; i--)
        {
            // Seleccionar un �ndice aleatorio entre 0 e i (inclusive)
            int j = random.Next(i + 1);

            // Intercambiar los elementos en las posiciones i y j
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    public void RandomizarLista<T>(List<T> lista)
    {
        // Crear una instancia de Random
        System.Random random = new System.Random();

        // Recorrer la lista desde el final hasta el principio
        for (int i = lista.Count - 1; i > 0; i--)
        {
            // Seleccionar un �ndice aleatorio entre 0 e i (inclusive)
            int j = random.Next(i + 1);

            // Intercambiar los elementos en las posiciones i y j
            T temp = lista[i];
            lista[i] = lista[j];
            lista[j] = temp;
        }
    }

    
    public void TerminoMiniJuego()
    {
        // desactivar todos los botones

        for(int i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].puedeApretarse = false;
            hiddenObjects[i].rectTransform.DOScale(Vector3.zero, 0.8f).SetEase(Ease.InOutSine);
        }

        animator.Play("terminoMinijuego");
    }


    
}
