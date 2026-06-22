using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;

/*void fooBar (int x)
{
    for(int i = 1; i <= x; i++)
    {
        string result = "";
        if (i % 3 == 0) result += "foo";
        //LogicExcercise 3, dimana jika x habis dibagi 4 akan print "baz"
        if (i % 4 == 0) result += "baz";
        if (i % 5 == 0) result += "bar";
        //LogicExcercise 2, dimana jika x habis dibagi 7 akan print "jazz"
        if (i % 7 == 0) result += "jazz";
        //LogicExcercise 3, dimana jika x habis dibagi 9 akan print "huzz"
        if (i % 9 == 0) result += "huzz";

        Console.Write(result == "" ? i.ToString() : result);

        if (i < x) Console.Write(", ");
    }
}

fooBar(21);*/

// Final LogicExcercise
var generator = new FooBarGenerator();

generator.AddRule(3, "foo");
generator.AddRule(4, "baz");
generator.AddRule(5, "bar");
generator.AddRule(7, "jazz");
generator.AddRule(9, "huzz");

generator.Print(21);

public class FooBarGenerator
{
    private readonly List<(int divisor, string text)> rules = new();

    public void AddRule(int divisor, string text)
    {
        rules.Add((divisor, text));
    }

    public void Print (int n)
    {
        for(int i = 1; i <= n; i++)
        {
            var sb = new StringBuilder();
            foreach(var rule in rules)
                if(i % rule.divisor == 0) sb.Append(rule.text);
                    
            Console.Write(sb.Length == 0 ? i.ToString() : sb.ToString());
            
            if (i < n) Console.Write(", ");
            
        }
    }

}