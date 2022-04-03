
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

//Method e) Given an order number find customer and all items in order and total cost of order, display name, unit price, quanity, and subtotal of each item in the order
Console.WriteLine("Please enter an order number: ");
customerOrder(Console.ReadLine());

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

static void customerOrder(string orderNumber)
{
    using var db = new SmallBusiness();
    {
        //join order and order item then join that new table with product and then finally that table with customer
        var results = from order in db.Orders
            join orderitem in db.OrderItems
                on order.Id equals orderitem.OrderId
            join product in db.Products on orderitem.ProductId equals product.Id
            join customer in db.Customers on order.CustomerId equals customer.Id
            where order.OrderNumber == orderNumber
            select new {order, orderitem, product, customer};
        if (results.Count() == 0)
        {
            Console.WriteLine("Order does not exist");
            return;
        }
        //Display the Name Just once instead of every iteration
        foreach (var e in results)
        {
            Console.WriteLine(e.customer.FirstName + " " + e.customer.LastName +":");
            break;
        }
        //display name, unit price, and total
        foreach(var e in results)
            Console.WriteLine(e.product.ProductName + " Unit Price: " + Encoding.UTF8.GetString(e.orderitem.UnitPrice) + " Quantity: " + e.orderitem.Quantity + " Subtotal: " + double.Parse(Encoding.UTF8.GetString(e.orderitem.UnitPrice)) * Convert.ToDouble(e.orderitem.Quantity));
        //display total amount
        foreach (var e in results)
        {
            Console.WriteLine("Total Amount: "+ Encoding.UTF8.GetString(e.order.TotalAmount));
            break;
        }
        Console.WriteLine();
    }
}