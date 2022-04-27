using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MediaSoupExtension
{
    static IntPtr invalidPtr = new IntPtr(-1); 
    public static bool IsInvalidPtr(this IntPtr ptr)
    {
        if (ptr == invalidPtr)
            return true;

        return false;
    }
}