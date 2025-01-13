using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

public class Connect_SQL
{
	private readonly string _connectionString; 

	public Connect_SQL(string connectionString)
	{
		_connectionString = connectionString;
	}

	// Phương thức private: tạo và mở kết nối tới cơ sở dữ liệu.
	private SqlConnection GetConnection()
	{
		var conn = new SqlConnection(_connectionString); 
		conn.Open(); 
		return conn; 
	}

	// Phương thức thực thi Stored Procedure không trả về dữ liệu (ExecuteNonQuery).
	public async Task<int> ExecuteNonQueryAsync(string storedProcedure, SqlParameter[] parameters)
	{
		using (var conn = GetConnection()) 
		{
			using (var cmd = new SqlCommand(storedProcedure, conn)) 
			{
				cmd.CommandType = CommandType.StoredProcedure; 
				cmd.Parameters.AddRange(parameters); 
				return await cmd.ExecuteNonQueryAsync(); 
			}
		}
	}

	// Phương thức thực thi Stored Procedure trả về dữ liệu dạng SqlDataReader.
	public async Task<SqlDataReader> ExecuteReaderAsync(string storedProcedure, SqlParameter[] parameters)
	{
		var conn = GetConnection(); 
		using (var cmd = new SqlCommand(storedProcedure, conn)) 
		{
			cmd.CommandType = CommandType.StoredProcedure; 
			cmd.Parameters.AddRange(parameters);
			var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
			return reader; 
		}
	}
}


