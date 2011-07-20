using FluentNHibernate.Mapping;
using Tests.TestDatabase;

namespace AutoMigrator.Tests {
    public class Mapping
    {
      public static void MapAddress(ComponentPart<Address> a)
        {
            a.Map(c => c.AddressLine, "Address");
            a.Map(c => c.City);
            a.Map(c => c.Country);
            a.Map(c => c.PostalCode);
            a.Map(c => c.Region);
            
        }   
    }
    
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("Customers");
            Id(x => x.Id, "CustomerID");
            Map(x => x.CompanyName, "Company Name");
            Map(x => x.ContactName, "Contact Name");
            Map(x => x.ContactTitle, "Contact Title");
            Map(x => x.Phone);
            Map(x => x.Fax);
            Component(x => x.Address,Mapping.MapAddress);
        }
    }
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employees");
            Id(x => x.Id, "EmployeeID");
            Map(x => x.FirstName, "First Name").Length(10);
            Map(x => x.LastName, "Last Name").Length(20);
            Map(x => x.Title).Length(30);
            Map(x => x.BirthDate);
            Map(x => x.HireDate);
            Map(x => x.HomePhone).Length(24);
            Map(x => x.Photo);
            References(x => x.ReportsTo);
            Component(x => x.Address, Mapping.MapAddress);
        }
    }
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("Orders");
            Id(x => x.Id, "OrderID");
            References(x => x.Customer,"CustomerID");
            References(x => x.SalesPerson,"EmployeeID");
            References(x => x.Shipper, "Ship Via");
            Component(x => x.ShipTo, Mapping.MapAddress);
            Map(x => x.OrderDate, "Order Date");
            Map(x => x.Freight);
            HasMany(x => x.OrderDetails);

        }
    }

    public class OrderDetailsMap : ClassMap<OrderDetails>
    {
        public OrderDetailsMap()
        {
            Table("Order Details");
            CompositeId()
                .KeyProperty(x=>x.OrderId, "OrderID")
                .KeyProperty(x=>x.ProductId, "Product ID");
            References(x => x.Order);       
            References(x => x.Product);       
            Map(x=>x.UnitPrice);
            Map(x=>x.Quantity);
            Map(x=>x.Discount);
         
        }
    }

    public class ShipperMap : ClassMap<Shipper>
    {
        public ShipperMap()
        {
            Table("Shippers");
            Id(x => x.Id, "ShipperId");
            Map(x => x.CompanyName, "Company Name");

        }
    }
    public class SupplierMap : ClassMap<Supplier>
    {
        public SupplierMap()
        {
            Table("Suppliers");
            Id(x => x.Id, "SupplierId");
            Map(x => x.Name, "Company Name");
            Map(x => x.Phone);
            Map(x => x.Fax);
            Component(x => x.Address, Mapping.MapAddress);
        }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Products");
            Id(x => x.Id, "ProductId");
            References(x => x.Supplier, "Supplier Id");
        }
    }
    
    


}
