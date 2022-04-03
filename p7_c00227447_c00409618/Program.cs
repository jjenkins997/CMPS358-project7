
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using p7_c00227447_c00409618;

//Method a) all discontinued products
listDiscontinued();

//Method b) given a country give all names and numbers of customers in that country
Console.WriteLine("Please Enter a Country for customer information: ");
countryCustomers(Console.ReadLine());

//Method c) given a country list id, name, phone number, fax number and city of all suppliers in country
Console.WriteLine("Please Enter a Country for supplier information: ");
countrySuppliers(Console.ReadLine());

//Method d) given a supplier find all products supplied not discontinued, display supplier name, package name, unit price and package info
Console.WriteLine("Please enter a supplier for product information: ");
supplierProducts(Console.ReadLine());

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

static void countrySuppliers(string country)
{
    using var db = new SmallBusiness();
    {
        var results = from n in db.Suppliers where n.Country == country select n;
        if (results.Count() == 0)
        {
            Console.WriteLine($"No Suppliers in this country");
            return;
        }

        foreach (var e in results)
        {
            Console.Write(e.Id + " " + e.CompanyName + " " + e.Phone + " ");
            Console.Write(String.IsNullOrEmpty(e.Fax) ? "No Fax Machine " : e.Fax + " ");
            Console.WriteLine(e.City);
        }
        Console.WriteLine();
    }
}

static void supplierProducts(string supplier)
{
    using var db = new SmallBusiness();
    {
        //join the tables set supplier id equal to supplierId in products where the product isnt discontinued
        var results = from s in db.Suppliers join p in db.Products 
            on s.Id equals p.SupplierId where s.CompanyName == supplier where p.IsDiscontinued.ToString() == "0" select p;
        if (results.Count() == 0)
        {
            Console.WriteLine("Supplier does not exist");
            return;
        }
        foreach(var e in results)
            Console.WriteLine(supplier+ " " + e.ProductName + " " + "Unit Price: " +Encoding.UTF8.GetString(e.UnitPrice) + " " + e.Package);
        Console.WriteLine();

    }
}