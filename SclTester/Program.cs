using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SclTester
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleConversationLanguage.SclParser p = new SimpleConversationLanguage.SclParser();
            p.a(@"Hello.
Hi and again.
/command {$arg}
menu
    Option 1
        I do something
        And something else
    Option 2
        And something quite different
end of menu
Now bye.");

            /* p.a(@"
 I would like to live with you, {you}.
 Don't take it the wrong way!... I mean, I'm still going to be in virtual space, just... 
 I want to be in the same country at least.
 Would that be alright?
     -Of course, {name}.
     -I don't trust you that much yet.
         I understand. But I still *need* to know where I'm going to live.
         How about you just invent a place and we're going to pretend you live there?
             -That would work.
 Awesome!
 
 
 Please type the full name of the country where you live!
 /label country
 /input country
 You live in {$country}, really?
     -Yes.
         /special:SetCountry {$country}
         Understood.
         Now let me check the timezone, please.
         /goto timezone
     -No.
         Okay. Then please type the full name of the country where you live.
         /goto country
 /label timezone
 What's the time where you live right now? 
 Use the HH:MM format, please, using 24-hour clock.
 /input time
 /special:SetTimezone {$time}
 Thanks! 
 I'm really looking forward to living in my new home!
 Hopefully we'll make many nice memories together!");*/
        }
    }
}
