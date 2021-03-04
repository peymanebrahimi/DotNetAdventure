using System;

namespace DesignPattern.ConsoleApp1
{
    public class Address2
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
    public class Customer
    {
        public string Name { get; set; }
        public bool IsVip { get; set; }
        public Address2 Address { get; set; }
    }

    public class Account
    {
        public DateTime DueDate { get; set; }
        public Customer Customer { get; set; }
        public double Balance { get; set; }

    }
    //////////////////////////////////////////////////

    public class AccountBuilder
    {
        private Account _account;

        public AccountBuilder()
        {
            _account = new Account()
            {
                Balance = 10000,
                DueDate = DateTime.Now.AddDays(1),
                Customer = new Customer()
                {
                    IsVip = false,
                    Name = "Scott",
                    Address = new Address2()
                    {
                        City = "LA",
                        Country = "USA"
                    }
                }
            };
        }

        public static AccountBuilder DefaultAccount()
        {
            return new AccountBuilder();
        }

        public AccountBuilder WithLatePaymentStatus()
        {
            _account.DueDate = DateTime.Now.AddDays(-1);
            return this;
        }

        public AccountBuilder WithVipCustomer()
        {
            _account.Customer.IsVip = true;
            return this;
        }

        public Account Build()
        {
            return _account;
        }
    }
}
