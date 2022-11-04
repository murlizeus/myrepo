/*
We have a bin of robot parts in a factory. Each part goes to a robot with a specific, unique name. Each part will be described by a string, with the name of the robot and the part name separated by an underscore, like "Rocket_arm".

All robots are made of the same types of parts, and we have a string of all of the parts required to form a complete robot. Given a list of available parts, return the collection of robot names for which we can build at least one complete robot.

Sample Input:

all_parts = [
    "Rocket_claw",
    "Rocket_sensors", 
    "Dustie_case", 
    "Rust_sensors",
    "Bolt_sensors",
    "Rocket_case",
    "Rust_case",
    "Bolt_speaker",
    "Rocket_wheels",
    "Rocket_speaker",
    "Dustie_case",
    "Dustie_arms",
    "Rust_claw",
    "Dustie_case",
    "Dustie_speaker",
    "Bolt_case",
    "Bolt_wheels",
    "Rust_legs",
    "Bolt_sensors" ]

required_parts_1 = "sensors,case,speaker,wheels"
required_parts_2 = "sensors,case,speaker,wheels,claw"

Expected Output (output can be in any order):

get_robots(all_parts, required_parts_1) => ["Bolt", "Rocket"]
get_robots(all_parts, required_parts_2) => ["Rocket"]

N: Number of elements in `all_parts`
P: Number of elements in `required_parts`
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

class Solution {
    static void Main(String[] args) {
        string requiredParts1 = "sensors,case,speaker,wheels";
        string requiredParts2 = "sensors,case,speaker,wheels,claw";

        var allParts = new string[] { 
        "Rocket_claw",
        "Rocket_sensors", 
        "Dustie_case", 
        "Rust_sensors",
        "Bolt_sensors",
        "Rocket_case",
        "Rust_case",
        "Bolt_speaker",
        "Rocket_wheels",
        "Rocket_speaker",
        "Dustie_case",
        "Dustie_arms",
        "Rust_claw",
        "Dustie_case",
        "Dustie_speaker",
        "Bolt_case",
        "Bolt_wheels",
        "Rust_legs",
        "Bolt_sensors"
        };
        
        string[] result = get_robots(allParts,requiredParts1);
        Console.WriteLine("[{0}]",string.Join(',',result));
        result = get_robots(allParts,requiredParts2);
        Console.WriteLine("[{0}]",string.Join(',',result));
        
    }
    
    public static string[] get_robots(string [] allparts,string required_parts)
    {
        List<string> robots = new List<string>();
        
        Hashtable ht = new Hashtable();
        
         foreach(string part in allparts)
         {
             string[] ptarr = part.Split('_');
             string rbname = ptarr[0];
             string ptname = ptarr[1];
             if(!ht.Contains(rbname))
             {
                 HashSet<string> hs = new HashSet<string>();
                 hs.Add(ptname);
                 ht.Add(rbname,hs);
             }else
             {
                 var hs = (HashSet<string>)ht[rbname];
                 hs.Add(ptname);
             }
         }
         
         
        //  foreach(var kv in ht.Keys)
        //  {
        //      Console.WriteLine("{0} -  {1}",kv, string.Join(',', (HashSet<string>) ht[kv]));
        //  }
         
         string []reqparts = required_parts.Split(',');
         
         foreach(var kv in ht.Keys)
         {
             var robopartset = (HashSet<string>) ht[kv];
              bool allpartsflag = true;
             foreach(string rpart in reqparts)
             {
                 if(!robopartset.Contains(rpart))
                 { 
                     allpartsflag = false;
                     break;
                 }
             }
             if(allpartsflag)
             robots.Add((string)kv);
         }
         
         return robots.ToArray();
    }
}


