# MapData
Simple implementation to tranform data from datareader into an object or list
Methods:
  - Mapper.MapObject
  - Mapper.MapList

### Considerations
- The field name must match with property name of class

<br />

## Usage
This example uses a mysql database

Data stored in table
| character_id | name | genre | serie | 
| ------ | ------ | ------ | ------ |
| 1 | Maki Nishikino | F | Love Live! | 
| 2 | Mash Kyrielight | F | Fate Grand Order | 
| 3 | Shinobu Oshino | F | Monogatari Series |

### Character class

```csharp
public class Character
{
    public string CharacterId { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Serie { get; set; }
}
```

### Map Object

```csharp
public Character GetCharacter(int id)
{
    Character character = null;
    MySqlConnection connection = null;
    string query = @"select 
                      character_id as CharacterId
                        , name as Name
                        , genre as Genre
                        , serie as Serie
                        from tbl_character
                      where character_id = @id";

    try
    {
        using (connection = new MySqlConnection(_appSettings.Connection_DBTest))
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    character = Mapper.MapObject<Character>(reader);
                }
                connection.Close();
            }
        }
    }
    catch (Exception e) {
        throw;
    }
    finally {
        if (connection != null) {
            if (connection.State != System.Data.ConnectionState.Closed) {
                connection.Close();
            }
        }
    }

    return character;
}
```

### Map List

```csharp
public List<Character> GetCharacters()
{
    List<Character> characters = null;
    MySqlConnection connection = null;
    string query = @"select 
                      character_id as CharacterId
                        , name as Name
                        , genre as Genre
                        , serie as Serie
                        from tbl_character";

    try
    {
        using (connection = new MySqlConnection(_appSettings.Connection_DBTest))
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.CommandTimeout = 600;
                connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    characters = Mapper.MapList<Character>(reader);
                }
                connection.Close();
            }
        }
    }
    catch (Exception e) {
        throw;
    }
    finally {
        if (connection != null) {
            if (connection.State != System.Data.ConnectionState.Closed) {
                connection.Close();
            }
        }
    }

    return characters;
}
```
