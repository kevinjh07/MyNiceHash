using MyNiceHash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNiceHash.Services {
    public interface IProviderWorkerService {
        Task<List<Worker>> GetProviderWorkers(AlgorithmType algorithmType);
    }
}
