namespace ConsoleApp7BuilderPattern;

// required steps
// host is required
internal interface IUrlBuilder
{
    IUrlBuilder WithProtocol(string protocol);
    IUrlBuilder WithPort(string port);
    string Build();
}

internal interface IHostUrlBuilder
{
    IUrlBuilder WithHost(string host);
}

class MyUrlBuilder2 : IUrlBuilder, IHostUrlBuilder
{
    private string _host = "localhost";
    private string _port = "";
    private string _protocol = "http";

    private MyUrlBuilder2() { }

    public static IHostUrlBuilder Create()
    {
        return new MyUrlBuilder2();
    }
    public string Build()
    {
        if (string.IsNullOrEmpty(_port))
        {
            return $"{_protocol}://{_host}";
        }
        else
        {
            return $"{_protocol}://{_host}:{_port}";
        }
    }

    public IUrlBuilder WithHost(string host)
    {
        _host = host;
        return this;
    }

    public IUrlBuilder WithPort(string port)
    {
        _port = port;
        return this;
    }

    public IUrlBuilder WithProtocol(string protocol)
    {
        _protocol = protocol;
        return this;
    }
}
