using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeUtil {

	public static sbyte SerializeBool(bool val)
    {
        var result = val ? 1 : 0;
        return (sbyte)result;
    }

    public static bool DeserializeBool(sbyte val)
    {
        return val == 1 ? true : false;
    }
}
