namespace DataStorage

open SQLite
open System

[<Table("user_accounts"); CLIMutable>]
type UserAccount =
    {
        [<Column "id"; PrimaryKey; Collation "nocase">]
        Id: string
        [<Column("full_name")>]
        FullName: string
        [<Column("email")>]
        Email: string
        [<Column("password")>]
        Password: string
    }
           
module UserRegistry =
        
    let getConnection (database: string) =
        let conn = new SQLiteConnection(database)
        conn.CreateTable<UserAccount>() |> ignore
        conn

    let create database (userId : string) (pwd : string) (fullname: string) (email: string) =
        use conn = getConnection database
        conn.Insert 
            ({ Id = userId
               FullName = fullname
               Email = email
               Password= pwd} : UserAccount)

    let get database (userId : string) =
        use conn = getConnection database
        let user = conn.Find<UserAccount>(userId)
        if not <| Object.ReferenceEquals(user, Unchecked.defaultof<UserAccount>) 
        then
            Some ({ Id = user.Id
                    Email = user.Email
                    FullName = user.FullName
                    Password = user.Password})
        else
            None
