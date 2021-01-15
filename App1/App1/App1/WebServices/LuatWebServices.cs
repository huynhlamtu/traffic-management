using App1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App1.WebServices
{
    class LuatWebServices
    {
        private string uri = "http://192.168.1.6/api/Luats";
        public LuatWebServices()
        {

        }
        public async Task<List<Luat>> GetLuatList()
        {
            List<Luat> lst = new List<Luat>();
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(uri);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Luat>>(content);
                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0} alo1234", ex.Message);
            }
            return lst;
        }

        public async Task<Luat> GetLuatById(int id)
        {
            Luat luat = new Luat();
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(uri + "/" + id);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Luat>(content);
                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0} alo1234", ex.Message);
            }
            return luat;
        }
    }
}
