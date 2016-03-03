# gremlins.dataaccess
Gremlin Data-Access is lightweight library used to simplify interaction between SQL Server database and your .NET application.
## Quick Start
Follow next steps:

1. Add reference to gremlins.dataaccess
2. Create entities
  
  ``` 'C#'
  class Parent
  {
    public Guid ParentID { get; set; }
    public string Name { get; set; }
    public IList<Child> Childs { get; set; }
  }
  
  class Child
  {
    public Guid ChildID { get; set; }
    public Guid ParentID { get; set; }
    public string Name { get; set; }
  }
  ```
3. Create entity mappers to map datafrom database to entities

  ```
  class ParentMapper: EntityMapper<Parent>
  {
    protected override Parent Read(DataRecord record, string path)
    {
      return new Parent
      {
        ParentID = record.Guid(path, nameof(Parent.ParentID)),
        Name = record.String(path, nameof(Parent.Name)),
      }
    }
  }
  
  class ChildMapper: EntityMapper<Child>
  {
    protected override Child Read(DataRecord record, string path)
    {
      return new Child
      {
        ChildID = record.Guid(path, nameof(Child.ChildID)),
        ParentID = record.Guid(path, nameof(Child.ParentID)),
        Name = record.String(path, nameof(Child.Name)),
      }
    }
  }
  ```
4. Setup your entity mappers adding relations between entities

  ```
  class ParentMapper: EntityMapper<Parent>
  {
    ...
    
    protected override MappingCollection Setup()
    {
      return MappingCollection.Create()
        .List<Parent, Child>("Childs", (parent, childs) => parent.Childs = childs, (parent, child) => parent.ParentID == child.ParentID);
    }
  }
  ```
5. Write entity accessor to call SQL Server stored procedure. Don't forget to register mappers you wrote.
  
  ```
  class ParentAccessor: EntityAccessor<Parent>
  {
    public ParentAccessor(IDbConnection connection)
      :base(connection)
    {
      UsingMapper<Parent, ParentMapper>();
      UsingMapper<Child, ChildMapper>();
    }

    public Parent Get(Guid parentID)
    {
      return Execute("GetParent",
        InputCollection.Create()
          .Add("ParentID", parentID),
        OutputCollection.Create()
          .Add("Parents")
          .Add("Parents#Childs"))
        .FirstOrDefault();
    }        
  }
  ```
6. Run it

## Tutorials

You can run sample project to overview different use cases. 

1. Run sample project
2. Invoke **list** command to get a set of available samples
  
  ```
  list
  ```
3. Run sample you like typing **run** command
  
  ```
  run Basic
  ```
4. Debug it or investigate output.
