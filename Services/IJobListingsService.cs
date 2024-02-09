using CareerSharp.Models;

namespace CareerSharp.Services
{
    public interface IJobListingsService
    {
        Task<IEnumerable<JobListing>> SearchJobs(string query);
    }
}