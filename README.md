# MapData
Simple implementation to convert data reader from a database in an object or list

- The field name of table in database must match with the property name of class

## Simple Usage
This example used a mysql database

Data stored in database
| id_user | name | last_name | username | password | creation_date |
| ------ | ------ | ------ | ------ | ------ | ------ |
| 1 | Name1 | LastName1 | Username1 | Password1 | 2021-06-26 22:59:13 |
| 2 | Name2 | LastName2 | Username2 | Password2 | 2021-06-26 22:59:13 |
| 3 | Name3 | LastName3 | Username3 | Password3 | 2021-06-26 22:59:13 |

### User class

```csharp
  public class User
  {
      public int Id { get; set; }
      public string  Name { get; set; }
      public string  LastName { get; set; }
      public string  Username { get; set; }
      public string  Password { get; set; }
      public DateTime  CreationDate { get; set; }
  }
```

### Map to object

```csharp
User user = null;

string query = @"select 
                  id_user as Id
                  , name as Name
                  , last_name as LastName
                  , username as Username
                  , password as Password
                  , creation_date as CreationDate
                  from tbl_users u
                  where u.id_user = 1";
// Code omitted

using (MySqlDataReader reader = cmd.ExecuteReader()) {
    user = MapData.MapObject.Map<User>(reader);
}

```

### Map to list

```csharp
List<User> users = nul;

string query = @"select 
                  id_user as Id
                  , name as Name
                  , last_name as LastName
                  , username as Username
                  , password as Password
                  , creation_date as CreationDate
                  from tbl_users";
                                
// Code omitted

using (MySqlDataReader reader = cmd.ExecuteReader()) {
    users = MapData.MapList.Map<User>(reader);
}

```
