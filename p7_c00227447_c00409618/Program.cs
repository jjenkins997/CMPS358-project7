
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Design;
using p7_c00227447_c00409618;

//Method a) all discontinued products
listDiscontinued();

static void listDiscontinued()
{
    using var db = new SmallBusiness();
    {
        var results = from n in db.Products where n.IsDiscontinued.ToString() == "1" select n;
        if (results.Count() == 0)
        {
            Console.WriteLine($"No discontinued products");
            return;
        }
        Console.WriteLine("Discontinued products: ");
        foreach(var e in results)
            Console.WriteLine(e.ProductName);
        Console.WriteLine();
    }
}