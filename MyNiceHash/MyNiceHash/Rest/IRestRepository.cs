using MyNiceHash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNiceHash.Rest {
    public interface IRestRepository {
        Task<string> GetAPIVersoin();
        Task<List<Worker>> GetWorkers(AlgorithmType algorithm);
        Task<List<ProviderStats>> GetProviderStats();
    }
}
