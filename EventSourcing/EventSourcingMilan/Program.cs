//https://www.youtube.com/watch?v=gvW9uJSFujA
Console.WriteLine("Hello, World!");

var account = BankAccount.Open("John Doe", 1000);

account.Deposit(500, "Paycheck");
account.Withdraw(200, "Groceries");
account.Transfer(Guid.NewGuid(), 300, "Gift");
account.Withdraw(account.Balance, "Withdraw before closing");
account.Close("Not needed anymore");

Console.WriteLine($"Final balance {account.Balance}");

foreach (var item in account.Events)
{
    Console.WriteLine($"Events: {item.GetType().Name} at {item.Timestamp}");
}

var events = account.Events;

var theSameAccount = BankAccount.ReplayEvents(events);

try
{
    theSameAccount.Deposit(100, "Extra deposit");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}


public abstract record Event(Guid StreamId)
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public record AccountOpened(Guid AccountId, string AccountHolder,
    decimal InitialDeposit,
    string Currency = "USD") : Event(AccountId);

public record DepositMade(Guid AccountId, decimal Amount,
    string Description) : Event(AccountId);

public record WithdrawalMade(Guid AccountId, decimal Amount, string Description) : Event(AccountId);

public record MoneyTransfered(Guid FromAccountId, Guid ToAccountId,
    decimal Amount, string Description) : Event(FromAccountId);

public record AccountClosed(Guid AccountId, string Reason) : Event(AccountId);

public class BankAccount
{
    public Guid Id { get; set; }
    public string AccountHolder { get; private set; }
    public decimal Balance { get; private set; }
    public string Currency { get; private set; }
    public bool IsActive { get; private set; }

    public List<Event> Events { get; } = [];

    private BankAccount() { }

    public static BankAccount Open(string accountHolder, decimal initialDeposit, string currency = "USD")
    {
        if (string.IsNullOrEmpty(accountHolder))
            throw new InvalidOperationException("Account holder must be provided.");

        if (initialDeposit < 0)
            throw new InvalidOperationException("Initial deposit must be greater than zero.");

        var account = new BankAccount();
        account.Apply(new AccountOpened(Guid.NewGuid(), accountHolder, initialDeposit, currency));
        return account;
    }

    private void Apply(Event @event)
    {
        switch (@event)
        {
            case AccountOpened e:
                Id = e.AccountId;
                AccountHolder = e.AccountHolder;
                Balance = e.InitialDeposit;
                Currency = e.Currency;
                IsActive = true;
                break;

            case DepositMade e:
                Balance += e.Amount;
                break;
            case WithdrawalMade e:
                Balance -= e.Amount;
                break;
            case MoneyTransfered e:
                Balance -= e.Amount;
                break;
            case AccountClosed e:
                IsActive = false;
                break;
            default:
                break;
        }

        Events.Add(@event);
    }

    public static BankAccount ReplayEvents(IEnumerable<Event> events)
    {
        var account = new BankAccount();
        foreach (var @event in events)
        {
            account.Apply(@event);
        }
        return account;
    }

    private void EnsureActive()
    {
        if (!IsActive)
            throw new InvalidOperationException("Account is closed.");
    }


    public void Deposit(decimal amount, string description)
    {
        EnsureActive();
        if (amount <= 0)
            throw new InvalidOperationException("Deposit amount must be greater than zero.");

        Apply(new DepositMade(Id, amount, description));
    }

    public void Withdraw(decimal amount, string description)
    {
        EnsureActive();
        if (amount <= 0)
            throw new InvalidOperationException("Withdrawal amount must be greater than zero.");
        if (Balance - amount < 0)
            throw new InvalidOperationException("Insufficient funds.");
        Apply(new WithdrawalMade(Id, amount, description));
    }

    public void Transfer(Guid toAccountId, decimal amount, string description)
    {
        EnsureActive();

        if (amount <= 0)
            throw new InvalidOperationException("Transfer amount must be greater than zero.");

        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");

        Apply(new MoneyTransfered(Id, toAccountId, amount, description));
    }

    public void Close(string reason)
    {
        EnsureActive();

        if (Balance != 0)
            throw new InvalidOperationException("Account balance must be zero to close the account.");

        Apply(new AccountClosed(Id, reason));
    }
}
