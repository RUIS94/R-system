using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Infrastructure.Caching;
using Model.DomainModels;
using Model.DTO;

namespace DataAccess.Repositories
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        private readonly RedisHelper redis;

        public AddressRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {          
            string query = "SELECT * FROM addresses";
            DataTable dataTable = await ExecuteQueryAsync(query);
            List<Address> addresses = new List<Address>();
            foreach (DataRow row in dataTable.Rows)
            {
                Address address = MapToAddress(row);
                addresses.Add(address);
            }

            return addresses;
        }

        public async Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId)
        {
            var addresses = new List<Address>();
            string query = "SELECT * FROM addresses WHERE customer_id = @customer_id";

            var parameters = new Dictionary<string, object?>
            {
                { "customer_id", customerId }
            };
            DataTable dataTable = await ExecuteQueryAsync(query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                addresses.Add(MapToAddress(row));
            }

            return addresses;
        }
        public async Task<bool> AddAddressAsync(Address address)
        {
            string query = @"
                INSERT INTO addresses (customer_id, address_type, street_address, city, state, country, zip_code)
                VALUES (@CustomerID, @AddressType, @StreetAddress, @City, @State, @Country, @ZipCode)";

            var parameters = new Dictionary<string, object?>
            {
                { "CustomerID", address.CustomerID },
                { "AddressType", address.AddressType },
                { "StreetAddress", address.StreetAddress },
                { "City", address.City },
                { "State", address.State },
                { "Country", address.Country },
                { "ZipCode", address.ZipCode }
            };

            try
            {
                await ExecuteNonQueryAsync(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteAddressesByCustomerIdAsync(int customerId)
        {
            string query = "DELETE FROM addresses WHERE customer_id = @CustomerID";
            var parameters = new Dictionary<string, object?>
            {
                { "CustomerID", customerId }
            };

            try
            {
                await ExecuteNonQueryAsync(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAddressesByCustomerIdAsync(Address address)
        {
            string query = @"
                UPDATE addresses
                SET address_type = @AddressType,            
                street_address = @StreetAddress, 
                city = @City, 
                state = @State, 
                country = @Country, 
                zip_code = @ZipCode
                WHERE customer_id = @CustomerID AND address_type = @AddressType";
            var parameters = new Dictionary<string, object?>
            {
                { "CustomerID", address.CustomerID },
                { "AddressType", address.AddressType },
                { "StreetAddress", address.StreetAddress },
                { "City", address.City },
                { "State", address.State },
                { "Country", address.Country },
                { "ZipCode", address.ZipCode }
            };
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }
        private Address MapToAddress(DataRow row)
        {
            return new Address
            {
                ID = Convert.ToInt32(row["id"]),
                CustomerID = Convert.ToInt32(row["customer_id"]),
                AddressType = row["address_type"] as string,
                StreetAddress = row["street_address"] as string,
                City = row["city"] as string,
                State = row["state"] as string,
                Country = row["country"] as string,
                ZipCode = row["zip_code"] as string
            };
        }
    }
}
