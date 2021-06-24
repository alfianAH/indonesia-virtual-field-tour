using UnityEngine;

public class ArrayElementTitleAttribute: PropertyAttribute
{
    public readonly string VarName;
    public ArrayElementTitleAttribute(string elementTitleVar)
    {
        VarName = elementTitleVar;
    }
}
