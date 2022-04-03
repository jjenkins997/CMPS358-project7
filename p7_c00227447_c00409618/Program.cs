﻿
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Design;
using p7_c00227447_c00409618;

//Method a) all discontinued products
listDiscontinued();

//Method b) given a country give all names and numbers of customers in that country
Console.WriteLine("Please Enter a Country: ");
countryCustomers(Console.ReadLine());

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

static void countryCustomers(string country)
{
    using var db = new SmallBusiness();
    {
        var results = from n in db.Customers where n.Country == country select n;
        if (results.Count() == 0)
        {
            Console.WriteLine($"No Customers in this country");
            return;
        }
        Console.WriteLine("Cusomters in " + country + ":");
        foreach (var e in results)
            Console.WriteLine(e.FirstName + " " + e.LastName + " " + e.Phone);
        Console.WriteLine();
    }
}