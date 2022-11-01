using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

/*
We are working on a security system for a badged-access room in our company's building.

We want to find employees who badged into our secured room unusually often. We have an unordered list of names and entry times over a single day. Access times are given as numbers up to four digits in length using 24-hour time, such as "800" or "2250".

Write a function that finds anyone who badged into the room three or more times in a one-hour period. Your function should return each of the employees who fit that criteria, plus the times that they badged in during the one-hour period. If there are multiple one-hour periods where this was true for an employee, just return the earliest one for that employee.

badge_times = [
  ["Paul",      "1355"], ["Jennifer",  "1910"], ["Jose",    "835"],
  ["Jose",       "830"], ["Paul",      "1315"], ["Chloe",     "0"],
  ["Chloe",     "1910"], ["Jose",      "1615"], ["Jose",   "1640"],
  ["Paul",      "1405"], ["Jose",       "855"], ["Jose",    "930"],
  ["Jose",       "915"], ["Jose",       "730"], ["Jose",    "940"],
  ["Jennifer",  "1335"], ["Jennifer",   "730"], ["Jose",   "1630"],
  ["Jennifer",     "5"], ["Chloe",     "1909"], ["Zhang",     "1"],
  ["Zhang",       "10"], ["Zhang",      "109"], ["Zhang",   "110"],
  ["Amos",         "1"], ["Amos",         "2"], ["Amos",    "400"],
  ["Amos",       "500"], ["Amos",       "503"], ["Amos",    "504"],
  ["Amos",       "601"], ["Amos",       "602"], ["Paul",   "1416"],
];

Expected output (in any order)
   Paul: 1315 1355 1405
   Jose: 830 835 855 915 930
   Zhang: 10 109 110
   Amos: 500 503 504

n: length of the badge records array
*/

class Solution {
    static void Main(String[] args) {
        string[][] badgeTimes = new string[][]{
        new string[]{"Paul", "1355"},
        new string[]{"Jennifer", "1910"},
        new string[]{"Jose", "835"},
        new string[]{"Jose", "830"},
        new string[]{"Paul", "1315"},
        new string[]{"Chloe", "0"},
        new string[]{"Chloe", "1910"},
        new string[]{"Jose", "1615"},
        new string[]{"Jose", "1640"},
        new string[]{"Paul", "1405"},
        new string[]{"Jose", "855"},
        new string[]{"Jose", "930"},
        new string[]{"Jose", "915"},
        new string[]{"Jose", "730"},
        new string[]{"Jose", "940"},
        new string[]{"Jennifer", "1335"},
        new string[]{"Jennifer", "730"},
        new string[]{"Jose", "1630"},
        new string[]{"Jennifer", "5"},
        new string[]{"Chloe", "1909"},
        new string[]{"Zhang", "1"},
        new string[]{"Zhang", "10"},
        new string[]{"Zhang", "109"},
        new string[]{"Zhang", "110"},
        new string[]{"Amos", "1"},
        new string[]{"Amos", "2"},
        new string[]{"Amos", "400"},
        new string[]{"Amos", "500"},
        new string[]{"Amos", "503"},
        new string[]{"Amos", "504"},
        new string[]{"Amos", "601"},
        new string[]{"Amos", "602"},
        new string[]{"Paul", "1416"},
        };
        
        // var invalidlogs = InvalidActions(records4);
        
        // string [] invalidEntry = invalidlogs[0];
        // string [] invalidExits = invalidlogs[1];
        //     Console.WriteLine("Invalid Entries");
        //     Array.ForEach(invalidEntry, s=> Console.WriteLine(s));
        //     Console.WriteLine("Invalid Exits");
        //     Array.ForEach(invalidExits, s=> Console.WriteLine(s));
        
        BarginAction(badgeTimes);
    }
    
    
    public static void BarginAction(string [][] logs)
    {
        Dictionary<string,SortedList<int,string>> dict = new Dictionary<string,SortedList<int,string>>();
        
         foreach(string[] lg in logs)
        {
            string emp = lg[0];
            string time = lg[1];
            string st = time.PadLeft(4,'0');
                int h = int.Parse(st.Substring(0,2));
                int min = int.Parse(st.Substring(2,2));
            
            //Console.Write("[{0},{1} {2} - {3}] ",hr,min,hr*60+min,time);            
            if(!dict.ContainsKey(emp))
            {
                SortedList<int,string> timeslist= new SortedList<int,string>();
                timeslist.Add(h*60+min,time);
                dict.Add(emp,timeslist);
            }
            else
            {
                var timeslist = dict[emp];
                timeslist.Add(h*60+min,time);
            }
        }
        
        foreach(var de in dict)
        {
            Console.Write("{0} - [",de.Key);
            var srtList = de.Value;
            foreach(var kv in  srtList)
            {
                Console.Write("({0}-{1}), ",kv.Key,kv.Value);
            }
            Console.Write("]");
            Console.WriteLine("");
        }
        
        foreach(KeyValuePair<string,SortedList<int,string>> kv in dict)
        {            
           SortedList<int,string> srttimes = kv.Value;
            if(srttimes.Count > 2)
            {
                int[] times = srttimes.Keys.ToArray();
                for(int i=0; i < times.Length ;++i)
                {
                    int ti = times[i];
                    List<string> bargtimes = new List<string>();
                    bargtimes.Add(srttimes[times[i]]);
                    
                    for(int j= i+1; j< times.Length; ++j)
                    {
                        int tj = times[j];
                        if(tj -  ti<= 60)
                           bargtimes.Add(srttimes[times[j]]); 
                        else
                            break;                        
                    }
                    if (bargtimes.Count >=3 )
                    {
                         Console.WriteLine("{0}:  {1}",kv.Key, string.Join(' ',bargtimes));
                         i = i+bargtimes.Count;
                    }
                }
            }
            else
                    break;
        }                                   
    }
}
    
    
 public static string[][] InvalidActions(string[][] logs)
    {
        List<string> invalidEntry = new List<string>();
        List<string> invalidExits = new List<string>();
        
        Hashtable ht = new Hashtable();
        
        foreach(string[] lg in logs)
        {
            string emp = lg[0];
            string action = lg[1];
            //Console.WriteLine("Emp {0} - Action {1}",emp,action);
                        
            if(!ht.Contains(emp))
            {
                if(action == "exit")
                {
                    if(!invalidExits.Contains(emp))
                        invalidExits.Add(emp);
                }
                else                     // Emp Entry
                     ht.Add(emp,action);
                     
            }
            else // Emp record 
            {
                if(action == "enter")
                {
                   if(!invalidEntry.Contains(emp))
                        invalidEntry.Add(emp);
                }
                else
                        // remove the entry
                    ht.Remove(emp);
            } 
            
             foreach(DictionaryEntry  de in ht)
            {
                Console.WriteLine("{0} - {1}",de.Key,de.Value);
            }
            Console.WriteLine("{0} {1}",emp,action); 
                                   
        }
        
            foreach(DictionaryEntry  de in ht)
            {
                if(!invalidEntry.Contains(de.Key))
                    invalidEntry.Add(de.Key.ToString());
            }
        
        return new string[][]{invalidEntry.ToArray(),invalidExits.ToArray()};
    }
} 
