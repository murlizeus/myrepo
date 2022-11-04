/*
You work in an automated robot factory. Once robots are assembled, they are sent to the shipping center via a series of autonomous delivery carts, each of which moves packages on a one-way route.

Given input that provides the (directed) steps that each cart takes as pairs, write a function that identifies all the start locations, and a collection of all of the possible ending locations for each start location.

In this diagram, starting locations are at the top and destinations are at the bottom - i.e. the graph is directed exclusively downward.

   A         E      J       Key:  [Origins]
  / \       / \      \             \
 B   C     F   L      M            [Destinations]
  \ /  \  /
   K     G
        / \
       H   I

paths = [ 
  ["A", "B"],
  ["A", "C"],
  ["B", "K"],
  ["C", "K"],
  ["E", "L"],
  ["F", "G"],
  ["J", "M"],
  ["E", "F"],
  ["G", "H"],
  ["G", "I"],
  ["C", "G"]
]

Expected output (unordered):  
[ "A": ["K", "H", "I"], 
  "E:" ["H", "L", "I"],
  "J": ["M"] ]

N: Number of pairs in the input.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

class Solution {
    static void Main(String[] args) {
        string[][] paths = {
            new []{"A", "B"},
            new []{"A", "C"},
            new []{"B", "K"},
            new []{"C", "K"},
            new []{"E", "L"},
            new []{"F", "G"},
            new []{"J", "M"},
            new []{"E", "F"},
            new []{"G", "H"},
            new []{"G", "I"},
            new []{"C", "G"}
        };
        var result = GetLeafNodes(paths);
        Console.WriteLine("+++RootNodes ++++");
        foreach(string[] strarr in result)
        {
            Console.WriteLine("{0} -> {1}",strarr[0], string.Join('>',strarr.Skip(1).Take(strarr.Length-1)));
        }
    }
    
    
    public static List<string[]> GetLeafNodes(string [][] paths)
    {
        List<string[]> rootleafnodes = new List<string[]>();
        Hashtable pht = new Hashtable();
        Hashtable cht = new Hashtable();
        for(int i = 0;i < paths.Length; ++i)
        {
            string par = paths[i][0];
            string ch = paths[i][1];
            
            if(!pht.Contains(par))
            {
                List<string> chlist = new List<string>();
                chlist.Add(ch);
                pht.Add(par,chlist);
            }
            else
            {
                var chlist = (List<string>) pht[par];
                chlist.Add(ch);
            }
            
            if(!cht.Contains(ch))
            {
                var prlist = new List<string>();
                prlist.Add(par);
                cht.Add(ch,prlist);
            }
            else
            {                
                var prlist = (List<string>) cht[ch];
                prlist.Add(par);
            }
        }
            /* Console.WriteLine("Parent Nodes");
            foreach(string prk in pht.Keys)
            {
                Console.WriteLine("{0}->{1}",prk,string.Join(',',(List<string>) pht[prk]));
            }
            
            Console.WriteLine("Child Nodes");
            foreach(string chk in cht.Keys)
            {
                Console.WriteLine("{0}<- {1}",chk,string.Join(',',(List<string>) cht[chk]));
            } */
            
            foreach(string node in pht.Keys)
            {
                if(!cht.Contains(node))
                {
                    // find the leaf nodes
                    string root = node;
                    List<string> leafnodes = new List<string>();
                    //Console.WriteLine("Root {0} ",node);
                    leafnodes.Add(root);
                    string tmpnode = root;
                    Stack<string> st = new Stack<string>();
                    while(pht.Contains(tmpnode) || st.Count() != 0)
                    {
                        if(!pht.Contains(tmpnode))
                        {
                        if(!leafnodes.Contains(tmpnode))
                            leafnodes.Add(tmpnode);
                            tmpnode = st.Pop();                          

                        }
                        else
                        {
                            //Console.WriteLine("{0}",string.Join(',',(List<string>) pht[tmpnode]));
                            var childnodes = (List<string>) pht[tmpnode];
                            foreach(string chn in childnodes)
                            {
                                st.Push(chn);
                            }  
                            tmpnode = st.Pop();                                                  
                        } 
                        //Console.WriteLine("{0}",tmpnode);                      
                    }
                    if(!leafnodes.Contains(tmpnode))
                        leafnodes.Add(tmpnode);
                    rootleafnodes.Add(leafnodes.ToArray());
                }
            }
        
        return rootleafnodes;
    }
