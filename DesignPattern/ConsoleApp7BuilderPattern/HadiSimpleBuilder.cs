namespace ConsoleApp7BuilderPattern;

internal class MyUrlBuilder
{
    private string _host = "localhost";
    private string _port = "";
    private string _protocol = "http";

    public MyUrlBuilder WithHost(string host)
    {
        _host = host;
        return this;
    }

    public MyUrlBuilder WithPort(string port)
    {
        _port = port; 
        return this;
    }

    public MyUrlBuilder WithProtocol(string protocol)
    {
        _protocol = protocol;
        return this;
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
}
