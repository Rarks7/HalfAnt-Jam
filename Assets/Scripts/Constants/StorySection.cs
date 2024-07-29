using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    //Make sure the enum entry matches the Knot name in Ink exactly.
    //IMPORTANT: Always add to the bottom of this enum list if you add a new story section, if you put something in the middle it 
    //will break all the currently selected options
    public enum StorySection
    {
        None,
        trash_bin,
        my_mail_box,
        not_my_mail_box,
        the_void_book,
        the_slime_bowl,
    }

    //public static class StorySectionExtensions
    //{
    //    public static string GetInkKnotString(this StorySection section)
    //    {
    //        return section switch
    //        {
    //            StorySection.TrashBinRandomRune => "trash_bin",
    //            StorySection.
    //            _ => "origin",
    //        } ;
                
    //    }
    //}

}

