// Component for a selectable dialogue option

using UnityEngine;
using Ink.Runtime;

public class Selectable : MonoBehaviour
{
    public Choice element;

    //Decides
    public void Decide() { FindObjectOfType<Dialogger>().SetDecision(element); }
}
