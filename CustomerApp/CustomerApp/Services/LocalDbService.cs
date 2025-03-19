using CustomerApp.Exceptions;
using CustomerApp.Models;
using SQLite;

namespace CustomerApp.Services
{
    public class LocalDbService
    {
        private const string DB_NAME = "customers.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Customer>();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            try
            {
                return await _connection.Table<Customer>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error!", ex.Message);
                throw new CustomException("Error occured while retrieving customers from the database!");
            }
        }

        public async Task<Customer> GetById(int id)
        {
            try
            {
                return await _connection.Table<Customer>().Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex) 
            {
                Console.Error.WriteLine("Error!", ex.Message);
                throw new CustomException("Error occured while retrieving selected customer from the database!");
            }
        }

        public async Task Create(Customer customer)
        {
            try
            {
                await _connection.InsertAsync(customer);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error!", ex.Message);
                throw new CustomException("Error occured while adding customer in the database!");
            }
        }

        public async Task Update(Customer customer)
        {
            try
            {
                await _connection.UpdateAsync(customer);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error!", ex.Message);
                throw new CustomException("Error occured while retrieving customers from the database!");
            }
        }

        public async Task Delete(Customer customer)
        {
            try
            {
                await _connection.DeleteAsync(customer);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error!", ex.Message);
                throw new CustomException("Error occured while retrieving customers from the database!");
            }
        }
    }
}
