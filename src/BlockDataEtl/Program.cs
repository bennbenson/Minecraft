using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MadReflection.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlockDataEtl
{
	internal static class Program
	{
		private static async Task Main(string[] args)
		{
			IConfiguration configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			string connectionString = configuration.GetConnectionString("Default");

			//await ImportFromJsonToTable("je_blocks.json", connectionString);
		}



		private static async Task ImportFromJsonToTable(string jsonFilePath, string connectionString)
		{
			System.Diagnostics.Debug.Assert(false, "CAREFUL!  This undoes all the manual work you've done.");

			string jsonText = System.IO.File.ReadAllText(jsonFilePath);
			JArray blocks = JsonConvert.DeserializeObject<JArray>(jsonText);

			using (SqlConnection connection = await SqlUtility.GetOpenConnectionAsync(connectionString))
			using (SqlCommand command = connection.CreateCommand())
			{
				command.CommandType = CommandType.Text;
				command.CommandText = "TRUNCATE TABLE [dbo].[MinecraftBlock];";
				await command.ExecuteNonQueryAsync();

				command.CommandText = @"INSERT INTO [dbo].[MinecraftBlock] ([je_id], [je_nsid], [be_id], [be_nsid], [be_dv], [display_name]) VALUES (@je_id, @je_nsid, @be_id, @be_nsid, @be_dv, @display_name);";

				int? nullInt = null;
				string nullString = null;

				var jeIdParameter = command.AddInputParameter("@je_id", nullInt);
				var jeNsidParameter = command.AddInputParameter("@je_nsid", nullString, 256);
				var beIdParameter = command.AddInputParameter("@be_id", nullInt);
				var beNsidParameter = command.AddInputParameter("@be_nsid", nullString, 256);
				var beDvParameter = command.AddInputParameter("@be_dv", nullInt);
				var displayNameParameter = command.AddInputParameter("@display_name", nullString, 256);



				foreach (JToken block in blocks)
				{
					jeIdParameter.Value = (int)block["id"];
					jeNsidParameter.Value = (string)block["name"];
					displayNameParameter.Value = (string)block["displayName"];

					await command.ExecuteNonQueryAsync();
				}


			}
		}

		private static void ExportFromTableToJson(string connectionString, string jsonFilePath)
		{
		}
	}
}
