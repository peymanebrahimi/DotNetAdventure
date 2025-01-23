namespace ConsoleApp7BuilderPattern
{
    // aggregation vs composition
    public class Employee
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Employee(string name, Address add)
        {
            Name = name;
            Address = add;
        }
    }

    public class Address
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
    }



    public class CEO
    {
        //CEO implementation   
    }

    public class Organization
    {
        public List<Employee> Employees { get; set; }
        public CEO CEO { get; set; }

        public void AddEmployee(Employee emp)
        {
            if (emp != null) Employees.Add(emp);
        }

    }
}
