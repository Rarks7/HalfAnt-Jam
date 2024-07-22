using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public enum StorySection
    {
        TrashBinRandomRune,

    }

    public static class StorySectionExtensions
    {
        public static string GetInkKnotString(this StorySection section)
        {
            return section switch
            {
                StorySection.TrashBinRandomRune => "trash_bin",
                _ => "origin",
            } ;
                
        }
    }

}

