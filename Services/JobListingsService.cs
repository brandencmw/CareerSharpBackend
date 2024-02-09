using CareerSharp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using static System.Net.WebRequestMethods;

namespace CareerSharp.Services
{
    public class JobListingsService : IJobListingsService
    {

        private readonly HttpClient _httpClient;
        public JobListingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        async public Task<IEnumerable<JobListing>> SearchJobs(string query)
        {
            List<JobListing> jobs = new List<JobListing>();
            string reed_endpoint = "https://jobicy.com/api/v2/remote-jobs?count=10&tag=" + query;
            this._httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpResponseMessage response = await this._httpClient.GetAsync(reed_endpoint);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                dynamic? responseObject = JsonConvert.DeserializeObject(responseData);
                int jobCount = responseObject?.jobCount ?? 0;
                if (jobCount > 0)
                {
                    foreach (var job in responseObject!.jobs)
                    {
                        long iD = Convert.ToInt64(job.id);
                        string title = job.jobTitle.ToString();
                        string description = job.jobDescription.ToString();
                        string location = job.jobGeo.ToString();
                        string url = job.url.ToString();
                        ulong salary = Convert.ToUInt64(job.annualSalaryMin);
                        jobs.Add(new JobListing(iD, title, location, url, salary, description));
                    }
                }

            }
            return jobs;
        }
    }
}
