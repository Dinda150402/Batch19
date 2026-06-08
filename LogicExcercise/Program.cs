void fooBar (int x)
{
    for(int i = 1; i <= x; i++)
    {
        string result = "";
        if (i % 3 == 0) result += "foo";
        if (i % 5 == 0) result += "bar";
        //LogicExcercise 2, dimana jika x habis dibagi 7 akan print "jazz"
        if (i % 7 == 0) result += "jazz";

        Console.Write(result == "" ? i.ToString() : result);

        if (i < x) Console.Write(", ");
    }
}

fooBar(21);