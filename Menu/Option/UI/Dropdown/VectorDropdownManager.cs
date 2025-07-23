using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class VectorDropdownManager : OptionDropdownManager<Vector2>
{
    [SerializeField]
    [TableList, PropertyOrder(1)]
    protected List<DropOption> dropdownOptions;

    private void OnValidate()
    {
        dropOptions = dropdownOptions;

    }
}