using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


/*
Prompt: Given an array of arrays, where each array contains two numbers - the first number being a parent and the second 
number being a child of a tree - return an array of arrays where the 0ith element is an array of all nodes with no parents (roots), 
and the 1st element is an array of all nodes with 3 parents.
signature getRootAndNodesWithThreeParents(arrayOfRelationships)

Technical Question 2
Prompt: Given an array of arrays, where each array contains two numbers - the first number being a parent and the second number 
being a child of a tree -, and two nodes - node1 and node2 - return an array of all common anscestors.

input_data = [[1, 3], [2, 3], [3, 6], [5, 6],[5, 7], [4, 5], [4, 8], [4, 9], [9, 11]]
output_date = [[],[]]
*/

class Solution {
    static void Main(String[] args) {
        
        int[,] input_data = {{2, 3}, {3, 6}, {5, 6},{5, 7}, {4, 5}, {4, 8}, {4, 9}, {9, 11}};
        
        int[][] ret = FindParents(input_data);
        Console.WriteLine("0 Preants {0}",string.Join(',',ret[0]));
        Console.WriteLine("3 Preants {0}",string.Join(',',ret[1]));
        
        int[] commonans = CommonAncestor(input_data,8,6);
    }
    
    public static int[][] FindParents(int[,] inputarr)
    {
        List<int> root = new List<int>();
        List<int> child3 = new List<int>();
        
        Hashtable ht = new Hashtable();
        for(int i =0; i< inputarr.GetLength(0); ++i)
        {
            //Console.WriteLine("[{0} {1}]",inputarr[i,0],inputarr[i,1]);
             if(!ht.Contains(inputarr[i,0]))
             {
                 var chlist = new List<int>();
                 chlist.Add(inputarr[i,1]);
               ht.Add(inputarr[i,0],chlist);
             }
             else
              ((List<int>) ht[inputarr[i,0]]).Add(inputarr[i,1]);             
        }
        
        /* foreach(int key in ht.Keys)
        {
            Console.WriteLine("{0} - {1}",key,string.Join(',',(List<int>)ht[key]));
        } */
        foreach(int key in ht.Keys)
        {
            bool isroot = true;
            foreach(int skey in ht.Keys)
            {
                var childList = (List<int>)ht[skey];
                if(childList.Contains(key))   
                {
                     isroot = false;
                     break;  
                } 
            }
            if(isroot)
            root.Add(key);
         }
        
        foreach(int key in ht.Keys)
        {
            int count3 = 0;
            foreach(int skey in ht.Keys)
            {
                var childList = (List<int>)ht[key];
                if(childList.Contains(key))   
                     count3++;
            }
            if(count3 == 3)
            child3.Add(key);
        }
        
        int [][] retarr = new int[2][];
         retarr[0] = root.ToArray();
         retarr[1] =  child3.ToArray();
     return retarr;
     }
     
     
     public static int[] CommonAncestor(int[,] inputarr, int node1, int node2)
    {
        HashSet<int> ans1 = new HashSet<int>();
        HashSet<int> ans2 = new HashSet<int>();
        
        Hashtable ht = new Hashtable();
        for(int i =0; i< inputarr.GetLength(0); ++i)
        {
            //Console.WriteLine("[{0} {1}]",inputarr[i,0],inputarr[i,1]);
             if(!ht.Contains(inputarr[i,0]))
             {
                 var chlist = new List<int>();
                 chlist.Add(inputarr[i,1]);
               ht.Add(inputarr[i,0],chlist);
             }
             else
              ((List<int>) ht[inputarr[i,0]]).Add(inputarr[i,1]);             
        }
        
        ans1 = Ancestor(ht,node1);
        ans2 = Ancestor(ht,node2);
        
        Console.WriteLine("Anscetor of {0} is {1}",node1,string.Join(',',ans1));
        Console.WriteLine("Anscetor of {0} is {1}",node2,string.Join(',',ans2));
        ans1.IntersectWith(ans2);
        Console.WriteLine("Common anscetor of {0} {1} is {2}",node1,node2,string.Join(',',ans1));
        return ans1.ToList().ToArray();
}

        public static HashSet<int> Ancestor(Hashtable ht, int node)
        {
            HashSet<int> anslist= new HashSet<int>();        
            int curnode = node;
            int parent = curnode;
            while(true)
            {
                bool isroot = true;
                foreach(int key in ht.Keys)
                {
                    var childnodes = (List<int>)ht[key];
                    if(childnodes.Contains(curnode))
                    {
                        anslist.Add(key);
                        parent = curnode;
                        curnode = key;
                        isroot = false;
                        break;
                    }                    
                }
                Console.WriteLine("{0}->{1} ",curnode,parent);
               if(isroot)
                break;
            }
            return anslist;
        }
}
        

