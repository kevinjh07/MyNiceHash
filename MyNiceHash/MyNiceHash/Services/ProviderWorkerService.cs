using System.Collections.Generic;
using System.Threading.Tasks;
using MyNiceHash.Models;
using MyNiceHash.Rest;

namespace MyNiceHash.Services {
    public class ProviderWorkerService : IProviderWorkerService {
        private readonly IRestRepository _restRepository;

        public ProviderWorkerService(IRestRepository restRepository) {
            _restRepository = restRepository;
        }

        public async Task<List<Worker>> GetProviderWorkers(AlgorithmType algorithmType) {
            return await _restRepository.GetWorkers(algorithmType);
        }
    }
}
