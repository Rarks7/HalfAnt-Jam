VAR searched_other_peoples_mail = false
VAR didnt_search_other_peoples_mail = false


->origin

===origin===
#end
*       [A Choice No One Will Ever Make]#end


->END



===trash_bin===
{!It stinks...| It continues to stink...}

+       [Move On] ->origin
*       [Search] ->search_bin

= search_bin
Gross... There's something in here...
#wait 1
#give rune_random
A Rune!
->origin

===my_mail_box===
It's the mail! It never fails.
* [Check mail] -> check_mail
+ [Move On] -> origin

= check_mail
It's a letter from my cousin! He lives in a very strange country on the other side of the world... What! According to this a strange old scientist guy has convinced him to leave home and try and become a master of some sort of animal fighting ring... he's 12! Looks like he got a cute pet though.
-> origin


===not_my_mail_box===
My mail box isn't on this side. {searched_other_peoples_mail: It looks looted | No one's around though}

+ [Search] -> search
+ [Move On] -> origin

= search
{searched_other_peoples_mail: There's nothing left to search you monster|Check other peoples mail... are you sure? Isn't that a crime?}

* [Do It] -> do_it
+ {!searched_other_peoples_mail} [Move On] -> relief
+ {searched_other_peoples_mail} [Move On] -> origin


= do_it
    ~ searched_other_peoples_mail = true
Okay fine *shuffle* it's a bunch of bills and cards for someone's birthday. No money. I hope you feel good about yourself.
-> origin

= relief
Wow close one, really wouldn't to make such a moral dip this early.
-> origin


