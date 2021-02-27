namespace ConsoleAppEfAndDb
{
    // A person may have zero or many pets
    // A person has only one address
    class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }

    class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }

        public int OwnerId { get; set; }
        public Person Owner { get; set; }
    }

    class Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}