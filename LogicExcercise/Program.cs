void fooBar (int x)
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

fooBar(21);