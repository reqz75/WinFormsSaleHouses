using System;
using System.Data;
using System.Data.SqlClient;

public class PropertyRepository
{
    private DatabaseHelper dbHelper = new DatabaseHelper();

    public DataTable GetAllProperties()
    {
        string query = @"
            SELECT 
                Id,
                Address,
                Area,
                Condition,
                Price,
                IsAvailable,
                CreatedAt
            FROM Properties 
            ORDER BY CreatedAt DESC";

        return dbHelper.ExecuteQuery(query);
    }

    public int AddProperty(string address, decimal area, string condition, decimal price)
    {
        string query = @"
            INSERT INTO Properties (Address, Area, Condition, Price) 
            VALUES (@Address, @Area, @Condition, @Price)";

        SqlParameter[] parameters = {
            new SqlParameter("@Address", address),
            new SqlParameter("@Area", area),
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Price", price)
        };

        return dbHelper.ExecuteNonQuery(query, parameters);
    }
}