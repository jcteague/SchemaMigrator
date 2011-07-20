using System;
using System.Collections.Generic;
using FubuCore;

namespace Tests.TestDatabase
{
    public abstract class EntityBase<PKType>
    {
        protected bool IsTransient()
        {

            return Convert.ToInt32(Id) == 0;
        }

        public virtual PKType Id { get; set; }
        public override bool Equals(object obj) {
            var other = obj as EntityBase<PKType>;

            if (other == null) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            if (IsTransient()) {
                return false;
            }
            return Id.Equals(other.Id);
        }
        public override int GetHashCode() {
            return IsTransient()
                       ? base.GetHashCode()
                       : Id.GetHashCode();
        }
    }
    public class Category : EntityBase<int>
    {
        public virtual string CategoryName { get; set; }
        public virtual string Description { get; set; }
       
    }
    public class Customer : EntityBase<string>
    {
        protected bool IsTransient()
        {
            return Id.IsEmpty();
        }
        public virtual string CompanyName { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string ContactTitle { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual Address Address { get; set; }
    }

    public class Employee : EntityBase<int>
    {
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual DateTime HireDate { get; set; }
        public virtual Address Address { get; set; }
        public virtual string HomePhone { get; set; }
        public virtual byte[] Photo { get; set; }
        public virtual Employee ReportsTo { get; set; }
    }

    public class Order : EntityBase<int>
    {
        public virtual Customer Customer { get; set; }
        public virtual Employee SalesPerson { get; set; }
        public virtual Shipper Shipper { get; set; }
        public virtual Address ShipTo { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual double Freight { get; set; }
        public virtual IList<OrderDetails> OrderDetails { get; set; }

    }
    public class OrderDetails : EntityBase<int>
    {
        public virtual int OrderId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual double UnitPrice { get; set; }
        public virtual double Quantity { get; set; }
        public virtual double Discount { get; set; }
    }

    public class Shipper : EntityBase<int>
    {
        public virtual string CompanyName { get; set; }
    }

    public class Product : EntityBase<int>
    {
        public virtual Supplier Supplier { get; set; }
    }

    public class Supplier : EntityBase<int> {
        public virtual string Name { get; set; }
        public virtual Address Address { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }    
    }
    public class Address
    {
        public virtual string AddressLine { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }
    }

}   