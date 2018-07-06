## Commander

Commander is a small library combines command pattern, with DDD practices. I think it is best explained by an example.

Download here [DotNetCommander](https://www.nuget.org/packages/DotNetCommander/)

```
PM> Install-Package DotNetCommander -Version 1.1.0
```


### Example

#### `RequestProductInformation.cs` represents the request for the commander.
```csharp
class RequestProductInformation
{
    public RequestProductInformation(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
}
```

#### `Product.cs` represents the response from the commander. The model must be marked as `AggregateRoot`
```csharp
class Product : AggregateRoot
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
}
```

### Command fetches the product information. 
#### You can think of the Command as `Command<Input, Output>`

```csharp
class BasicCommand : Command<RequestProductInformation, Product>
{
    private readonly Repo _repo;
    public static BasicCommand Instance(Repo repo) => new BasicCommand(repo);

    public BasicCommand(Repo repo)
    {
        _repo = repo;
    }
        
    public override Product Handle()
    {
        return _repo.GetProductByName(Request.Name);
    }
}
```

### Execute the command
#### You can have multiple commands executed by the Commander.
```csharp
var request = new RequestProductInformation("Test");

var commander = new Commander<RequestProductInformation, Product>(request);
var product = commander.Execute(BasicCommand.Instance(Repo.Instance()));
```